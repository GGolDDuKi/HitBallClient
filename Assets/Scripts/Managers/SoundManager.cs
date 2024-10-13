using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    /// <summary>
    /// ���������� ���带 �߻���ų ��� ���
    /// </summary>
    /// <param name="path">�����Ű�� ���� AudioClip �̸�</param>
    /// <param name="type">Bgm or Effect</param>
    /// <param name="pitch">���� ������ ����</param>
    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource = null;

        if (type == Sound.Bgm)
        {
            audioSource = _audioSources[(int)Sound.Bgm];
        }
        else
        {
            audioSource = _audioSources[(int)Sound.Effect];
        }

        Play(audioSource, audioClip, type, pitch);
    }

    /// <summary>
    /// Ư�� AudioSource���� ���带 �߻���Ű�� ���� ��� ���
    /// </summary>
    /// <param name="audioSource">���带 �߻���Ű�� ���� AudioSource</param>
    /// <param name="path">�����Ű�� ���� AudioClip �̸�</param>
    /// <param name="type">Bgm or Effect</param>
    /// <param name="pitch">���� ������ ����</param>
    public void Play(AudioSource audioSource, string path, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        Play(audioSource, audioClip, type, pitch);
    }

    public void Play(AudioSource audioSource, AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            //audioSource.volume = Managers.Setting.BGMVol;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            //audioSource.volume = Managers.Setting.SFXVol;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void SetVolume(Sound type)
    {
        //if (type == Sound.Effect)
        //    _audioSources[(int)Sound.Effect].volume = Managers.Setting.SFXVol;
        //else if (type == Sound.Bgm)
        //    _audioSources[(int)Sound.Bgm].volume = Managers.Setting.BGMVol;
        //else
        //    Debug.Log($"There is no type of sound : {type}");
    }

    AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
            if (audioClip == null)
                Debug.Log($"AudioClip Missing : {path}");
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing : {path}");
        }

        return audioClip;
    }
}

public enum Sound
{
    Bgm,
    Effect,
    MaxCount
}