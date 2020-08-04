using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;

[System.Serializable]
public class AudioProps
{
	public AudioProps()
	{ }

	public AudioProps(AudioClip c)
	{
		clip = c;
		pitch = 1.0f;
		vol = 1.0f;
	}

	public string name;
	public AudioClip clip;
	public float pitch;
	public float vol;
};

public class SFXManager : MonoBehaviour
{
	public Dictionary<string, List<AudioClip>> soundLib;
	public bool isMute;
	public SFXDatabase db;

	private List<AudioSource> audioSources;
	private float trimThreshold = 0.25f;

	public void Init()
	{
		isMute = false;
		audioSources = new List<AudioSource>();
		soundLib = new Dictionary<string, List<AudioClip>>();
		db = Resources.Load<SFXDatabase>("Data/SFXDatabase");
		InitializeSounds();
	}

	public bool ToggleMute()
	{
		isMute = !isMute;

		if (isMute)
		{
			foreach (AudioSource src in audioSources)
			{
				src.Stop();
			}
		}

		return isMute;
	}

	public void PlayLoop(string name, float vol = 1.0f)
	{
		PlaySound(name, vol);
		AudioClip clip = GetAudio(name).clip;
		GetSource(clip).loop = true;
	}

	public void PlaySound(string name, float vol = 1.0f, float min = 1.0f, float max = 1.0f)
	{
		if (soundLib.ContainsKey(name) == false)
		{
			Debug.Log("Sound library does not contain " + name + ".");
		}

		AudioProps props = GetAudio(name);
		props.pitch = Random.Range(min, max);
		PlaySound(props, vol);
	}

	public void PlaySound(AudioClip clip, float vol = 1.0f, float minPitch = 1.0f, float maxPitch = 1.0f)
	{
		AudioProps prop = new AudioProps(clip);
		prop.pitch = Random.Range(minPitch, maxPitch);
		PlaySound(prop, vol);
	}

	public void PlaySound(AudioProps props, float vol = 1.0f)
	{
		if (isMute) return;

		props.vol = vol;

		// Look for a free audio source
		foreach (AudioSource source in audioSources)
		{
			if (source.isPlaying) continue;
			ApplyProperties(source, props);
			source.volume = props.vol;
			source.Play();
			return;
		}

		// No free audio sources; create and use a new one
		AudioSource src = gameObject.AddComponent<AudioSource>();
		ApplyProperties(src, props);
		src.volume = props.vol;
		src.Play();
		audioSources.Add(src);
	}

	public void PitchShift(string name, float pitch)
	{
		AudioClip clip = GetAudio(name).clip;
		AudioSource source = GetSource(clip);
		if (source != null)
		{
			source.pitch = pitch;
		}
	}

	public void VolumeShift(string name, float vol)
	{
		AudioClip clip = GetAudio(name).clip;
		AudioSource source = GetSource(clip);
		if (source != null)
		{
			source.volume = vol;
		}
	}

	public void StopSound(string name)
	{
		var clip = GetAudio(name);

		// Note: Might cause bug for multiple sources playing the same clip
		foreach (AudioSource source in audioSources)
		{
			if (source.clip == clip.clip)
			{
				source.Stop();
				return;
			}
		}
	}

	AudioSource GetSource(AudioClip clip)
	{
		// Note: Might cause bug for multiple sources playing the same clip
		foreach (AudioSource source in audioSources)
		{
			if (source.clip.name == clip.name)
			{
				return source;
			}
		}

		return null;
	}

	// Private 
	void InitializeSounds()
	{
		foreach (AudioData data in db.sfxData)
		{
			if (data.clip == null) continue;
			AddSound(data.name, TrimStartSilence(data.clip));
		}
		foreach (AudioBatchData batch in db.sfxBatchData)
		{
			if (batch.clips.Count == 0) continue;
			AddSoundBatch(batch.name, batch.clips);
		}
	}

	void AddSound(string name, AudioClip clip)
	{
		if (!soundLib.ContainsKey(name))
		{
			soundLib.Add(name, new List<AudioClip>());
		}

		soundLib[name].Add(clip);
	}

	void AddSoundBatch(string name, List<AudioClip> clips)
	{
		for (int i = 0; i < clips.Count; ++i)
		{
			clips[i] = TrimStartSilence(clips[i]);
		}
		soundLib.Add(name, clips);
	}

	AudioClip TrimStartSilence(AudioClip clip)
	{
		AudioClip trimmedClip = clip;
		float[] samples = new float[clip.samples * clip.channels];
		clip.GetData(samples, 0);

		for (int i = 0; i < samples.Length; ++i)
		{
			if (Mathf.Abs(samples[i]) <= trimThreshold) continue;
			float[] newSample = new float[samples.Length - i];
			System.Array.Copy(samples, i, newSample, 0, newSample.Length);
			trimmedClip.SetData(newSample, 0);
			break;
		}

		return trimmedClip;
	}
	
	void ApplyProperties(AudioSource src, AudioProps props)
	{
		src.clip = props.clip;
		src.pitch = props.pitch;
	}

	AudioProps GetAudio(string name)
	{
		if (soundLib.ContainsKey(name) == false)
		{
			Debug.Log("Sound library does not contain " + name + ".");
			return new AudioProps();
		}

		List<AudioClip> clips = soundLib[name];
		AudioClip currentClip = clips[Random.Range(0, clips.Count)];

		return new AudioProps(currentClip);
	}

}
