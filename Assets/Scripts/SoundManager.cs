using UnityEngine;
using UnityEngine.UI;

public class SoundManager : AudioEventSubscriber
{
    [SerializeField, Range(0f, 1f), Tooltip("BGMの音量")] float BGMVolume = 1;
    [SerializeField, Range(0f, 1f), Tooltip("効果音の音量")] float SeVolume = 1;

    AudioSource titleBGM;
    AudioSource stageBGM;

    Slider bgmSlider;


    void Start()
    {
        titleBGM = GameObject.Find("TitleBGM").GetComponent<AudioSource>();
        bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        AudioManager.SetVolume();
    }

    void Update()
    {
        TitleBGMVolumeSet();
    }

    /// <summary>
    /// Title画面でのBGMの音量をセットする関数
    /// </summary>
    void TitleBGMVolumeSet()
    { 
        titleBGM.volume = bgmSlider.value;
    }

    public override void OnSetVolume()
    {
        Debug.Log("1");
    }

    //void StageBGMVolumeSet()
    //{
    //    stageBGM = GameObject.Find("StageBGM").GetComponent<AudioSource>();
    //    stageBGM.volume = 1f;
    //}
}
