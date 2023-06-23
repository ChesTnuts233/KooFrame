using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : Singleton<MusicMgr>
{
    private AudioSource bgMusic = null;
    private GameObject soundObj = null;
    private List<AudioSource> soundsList = new List<AudioSource>(); //音效列表

    private float bgValue = 1f;   //背景音乐音量
    private float soundValue = 1f;   //效果音乐音量

    private void Update()
    {
        for (int i = soundsList.Count - 1; i > 0; --i)
        {
            try
            {
                if (!soundsList[i].isPlaying)
                {
                    GameObject.Destroy(soundsList[i]);
                    soundsList.RemoveAt(i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGMUsic(string name)
    {
        if (bgMusic == null)
        {
            GameObject obj = new GameObject(name);
            obj.name = "bgMusic";
            GameObject.DontDestroyOnLoad(obj);
            bgMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐 加载完成后播放
        ResMgr.Instance.LoadAsync<AudioClip>("Music/BG/" + name, (clip) =>
        {
            bgMusic.clip = clip;
            bgMusic.loop = true;
            bgMusic.volume = bgValue;
            bgMusic.Play();
        });
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGMusic()
    {
        if (bgMusic == null)
            return;
        bgMusic.Pause();
    }
    /// <summary>
    /// 改变背景音乐音量
    /// </summary>
    public void ChangeBUMusicValue(float v)
    {
        bgValue = v;
        if (bgMusic == null)
        {
            return;
        }
        bgMusic.volume = bgValue;
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">音效名</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="callBack">回调函数</param>
    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callBack = null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        //异步加载音效 加载结束后再添加音效
        ResMgr.Instance.LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundsList.Add(source);
            if (callBack != null)
            {
                callBack(source);
            }

        });
    }
    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundsList.Contains(source))
        {
            soundsList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// 改变音效音量
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundsList.Count; i++)
        {
            soundsList[i].volume = soundValue;
        }
    }
    public MusicMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

}
