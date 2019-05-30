using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Music_player_Script : MonoBehaviour
{
	public AudioSource source;
	public Slider volume;
	public Slider Song_Slider;
	public List<AudioClip> songs=new List<AudioClip>();
	bool shuffle=false;
	int songs_index = 0;
	bool pause_va = false;
	bool mute = false;
	bool Play_ = false;
	public Text timer;
	public Text title;
	public Text length;
	public int fullLength;
	public int seconds;
	public int minutes;

	void Start()
	{

		source = GetComponent<AudioSource>();

	}

	void Update()
	{
		showFullLengthOfSong ();
		showPlayTime ();
		if (Play_)
		{
			if (!source.isPlaying)
			{
				if (!pause_va)
				{
					Next();
				}
			}
		}
	}
	public void PLay()
	{
		Play_ = true;
		source.clip = songs[songs_index];
		Song_Slider.maxValue = source.clip.length;
		//playTime = (int)source.time;
		//showPlayTime();
		source.Play();
		songTitle();

	}
	public void Pause()
	{
		pause_va = !pause_va;
		if (pause_va)
		{
			source.Pause();
		}
		else if (!pause_va)
		{
			source.UnPause();
		}


	}
	public void ValueChangeCheck()
	{
		source.volume = volume.value;
	}
	public void Next()
	{
		if (songs_index < songs.Count && !shuffle)
		{
			songs_index++;
			songs_index = songs_index % songs.Count;
		}
		else
		{
			System.Random rand = new System.Random();
			songs_index=rand.Next(0,songs.Count);
			Debug.Log ("Random"+songs_index);
		}
		PLay();
		songTitle();
	}
	public void Previous()
	{
		if (songs_index >= 0 && !shuffle)
		{
			if (songs_index == 0) {
				songs_index = songs.Count - 1;
			} else {
				songs_index--;
			}
		}
		else
		{
			System.Random rand = new System.Random();
			songs_index=rand.Next(0,songs.Count);
			Debug.Log ("Random"+songs_index);
		}
		PLay();
		songTitle();
	}
	public void Mute()
	{
		mute = !mute;
		if (mute)
		{
			source.mute = true;
			volume.value=0;
			source.volume=0;
		}
		else if (!mute)
		{
			source.mute = false;
			volume.value=1;
			source.volume=1;
		}
	}

	public void stop() {
		Play_ = false;		
		source.Stop();
	}
	public void Shuffle(){
		shuffle = !shuffle;
	}
	void songTitle(){
		title.text = source.clip.name;
		fullLength = (int)source.clip.length;
	}
	void showFullLengthOfSong(){
		length.text = ((fullLength /60) %60) + ":" + (fullLength%60).ToString("D2");
	}
	void showPlayTime(){
		double d = Math.Round(source.time,0);
		Song_Slider.value = (float) d;
		timer.text = minutes + ":" + seconds.ToString();
		int sec = (int)d;
		TimeSpan time = TimeSpan.FromSeconds (sec);
		timer.text = time.ToString();
	}
}
