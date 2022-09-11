using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.Resource;

[System]
public class AudioSystem : AbstractSystem
{
    public BindableProperty<bool> BackgroundMusicMute = new BindableProperty<bool>();
    public BindableProperty<bool> EffectMusicMute = new BindableProperty<bool>();
    private AudioSource mBackGroundSource;
    private AudioSource mEffectSource;
    protected override void OnInit()
    {
        mBackGroundSource = new GameObject("BG").AddComponent<AudioSource>();
        mEffectSource = new GameObject("Effect").AddComponent<AudioSource>();
        mBackGroundSource.transform.SetParent(transform);
        mBackGroundSource.loop = true;
        mEffectSource.transform.SetParent(transform);

        BackgroundMusicMute.Value = true;
        BackgroundMusicMute.OnValueChanged = isMute => mBackGroundSource.mute = isMute;

        EffectMusicMute.Value = true;
        EffectMusicMute.OnValueChanged = isMute => mEffectSource.mute = isMute;
    }

    public void PlayBackGroundMusic(string path)
    {
        AudioClip clip = GetUtility<ResourcesManager>().Load<AudioClip>(path);
        if (!mBackGroundSource.isPlaying || mBackGroundSource.clip != clip)
        {
            mBackGroundSource.clip = clip;
            mBackGroundSource.Play();
        }
    }

    public void PlayEffectMusic(string path)
    {
        AudioClip clip = GetUtility<ResourcesManager>().Load<AudioClip>(path);
        mEffectSource.PlayOneShot(clip);
    }
}
