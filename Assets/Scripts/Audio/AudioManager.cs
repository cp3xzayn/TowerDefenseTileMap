public class AudioManager
{
    /// <summary> 現在のBGMの音量 </summary>
    static float m_bGMVolume;
    static float m_sEVolume;

    /// <summary>
    /// BGMの音量
    /// </summary>
    public static float BGMVolume
    {
        set { m_bGMVolume = value; }
        get { return m_bGMVolume; }
    }

    public static float SEVolume
    {
        set { m_sEVolume = value; }
        get { return m_sEVolume; }
    }
}
