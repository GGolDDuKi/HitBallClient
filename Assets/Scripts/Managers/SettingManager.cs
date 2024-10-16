using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager
{
    #region Sound

    float bgmVol;
    public float BGMVol
    {
        get
        {
            return bgmVol;
        }
        set
        {
            bgmVol = value;
            Managers.Sound.SetVolume(Sound.Bgm);
        }
    }

    float sfxVol;
    public float SFXVol
    {
        get
        {
            return sfxVol;
        }
        set
        {
            sfxVol = value;
            Managers.Sound.SetVolume(Sound.Effect);
        }
    }

    #endregion

    public void Init()
    {
        BGMVol = 1f;
        SFXVol = 1f;
    }
}
