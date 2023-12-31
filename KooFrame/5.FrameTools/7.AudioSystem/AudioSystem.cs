using System;
using System.Collections.Generic;
using UnityEngine;

namespace KooFrame
{
    public static class AudioSystem
    {
        private static AudioModule audioModule;
        private static Dictionary<int, AudioStack> m_AudioDic;
        public static void Init()
        {
            audioModule = FrameRoot.RootTransform.GetComponentInChildren<AudioModule>();
            audioModule.Init();
            InitAudioDic();
        }
        private static void InitAudioDic()
        {
            m_AudioDic = new Dictionary<int, AudioStack>();
        }
        public static float MasterVolume { get => audioModule.MasterlVolume; set { audioModule.MasterlVolume = value; } }
        public static float BGMVolume { get => audioModule.BGMVolume; set { audioModule.BGMVolume = value; } }
        public static float SFXVolume { get => audioModule.SFXVolume; set { audioModule.SFXVolume = value; } }
        public static bool IsMute { get => audioModule.IsMute; set { audioModule.IsMute = value; } }
        public static bool IsLoop { get => audioModule.IsLoop; set { audioModule.IsLoop = value; } }
        public static bool IsPause { get => audioModule.IsPause; set { audioModule.IsPause = value; } }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="clip">音乐片段</param>
        /// <param name="loop">是否循环</param>
        /// <param name="volume">音量，-1代表不设置，采用当前音量</param>
        /// <param name="fadeOutTime">渐出音量花费的时间</param>
        /// <param name="fadeInTime">渐入音量花费的时间</param>
        public static void PlayBGMAudio(AudioClip clip, bool loop = true, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
            => audioModule.PlayBGMAudio(clip, loop, volume, fadeOutTime, fadeInTime);

        /// <summary>
        /// 使用音效数组播放背景音乐，自动循环
        /// </summary>
        /// <param name="fadeOutTime">渐出音量花费的时间</param>
        /// <param name="fadeInTime">渐入音量花费的时间</param>
        public static void PlayBGMAudioWithClips(AudioClip[] clips, float volume = -1, float fadeOutTime = 0, float fadeInTime = 0)
            => audioModule.PlayBGMAudioWithClips(clips, volume, fadeOutTime, fadeInTime);

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public static void StopBGMAudio() => audioModule.StopBGMAudio();

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public static void PauseBGMAudio() => audioModule.PauseBGMAudio();

        /// <summary>
        /// 继续播放背景音乐
        /// </summary>
        public static void UnPauseBGMAudio() => audioModule.UnPauseBGMAudio();

        /*
        /// <summary>
        /// 播放一次特效音乐,并且绑定在某个游戏物体身上
        /// 但是不用担心，游戏物体销毁时，会瞬间解除绑定，回收音效播放器
        /// </summary>
        /// <param name="clip">音效片段</param>
        /// <param name="autoReleaseClip">播放完毕时候自动回收audioClip</param>
        /// <param name="component">挂载组件</param>
        /// <param name="volumeScale">音量 0-1</param>
        /// <param name="is3d">是否3D</param>
        /// <param name="callBack">回调函数-在音乐播放完成后执行</param>
        public static void PlayOnShot(AudioClip clip, Component component = null, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
            => audioModule.PlayAudio(clip, component, autoReleaseClip, volumeScale, is3d, callBack);
        /// <summary>
        /// 播放一次特效音乐
        /// </summary>
        /// <param name="clip">音效片段</param>
        /// <param name="position">播放的位置</param>
        /// <param name="autoReleaseClip">播放完毕时候自动回收audioClip</param>
        /// <param name="volumeScale">音量 0-1</param>
        /// <param name="is3d">是否3D</param>
        /// <param name="callBack">回调函数-在音乐播放完成后执行</param>
        public static void PlayOnShot(AudioClip clip, Vector3 position, bool autoReleaseClip = false, float volumeScale = 1, bool is3d = true, Action callBack = null)
            => audioModule.PlayOnShot(clip, position, autoReleaseClip, volumeScale, is3d, callBack);
        */

        /// <summary>
        /// 播放一次特效音乐
        /// </summary>
        /// <param name="audioStack">音效的Stack</param>
        /// <param name="pos">位置</param>
        /// <param name="parent">父级</param>
        /// <param name="callBack">回调函数（播放完后触发）</param>
        public static void PlayAudio(AudioStack audioStack, Vector2 pos,Transform parent = null,Action callBack = null)
            => audioModule.PlayAudio(audioStack, pos, parent,callBack);
    }
}
