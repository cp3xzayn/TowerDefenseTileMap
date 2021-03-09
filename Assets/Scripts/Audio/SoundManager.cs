using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f), Tooltip("BGMの音量")] float BGMVolume = 1;
    [SerializeField, Range(0f, 1f), Tooltip("効果音の音量")] float SeVolume = 1;

    AudioSource titleBGM;
    AudioSource stageBGM;

    Slider bgmSlider;
    Slider seSlider;

    // Updateで一度だけ呼び出すためのbool
    bool isTitleOneTime = true;
    bool isStageOneTime = true;

    void Start()
    {
        titleBGM = GameObject.Find("TitleBGM").GetComponent<AudioSource>();
        bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        seSlider = GameObject.Find("SESlider").GetComponent<Slider>();
        AudioManager.BGMVolume = 1;
        AudioManager.SEVolume = 1;
    }

    void Update()
    {
        SetBGMVolume();
        SetSEVolume();
        GetBGMVolume();
    }

    /// <summary>
    /// 設定した音量を入れる関数
    /// </summary>
    void GetBGMVolume()
    {
        // Scene名がTitleの時
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if (isTitleOneTime)
            {
                isStageOneTime = true;
                titleBGM = GameObject.Find("TitleBGM").GetComponent<AudioSource>();
                isTitleOneTime = false;
            }
            titleBGM.volume = AudioManager.BGMVolume;
        }
        // Scene名がStageの時
        else if (SceneManager.GetActiveScene().name == "Stage")
        {
            if (isStageOneTime)
            {
                isTitleOneTime = true;
                stageBGM = GameObject.Find("StageBGM").GetComponent<AudioSource>();
                isStageOneTime = false;
            }
            stageBGM.volume = AudioManager.BGMVolume;
        }
    }

    /// <summary>
    /// BGMの音量をセットする関数
    /// </summary>
    void SetBGMVolume()
    {
        AudioManager.BGMVolume = bgmSlider.value;
    }

    /// <summary>
    /// SEの音量をセットする関数
    /// </summary>
    void SetSEVolume()
    {
        AudioManager.SEVolume = seSlider.value;
    }
}
