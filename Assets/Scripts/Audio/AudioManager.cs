public class AudioManager
{
    //引数なしのデリゲート型を宣言する
    public delegate void DelegateAudio();

    public static DelegateAudio OnSetVolume;
    public static DelegateAudio OnReturnVolume;

    public static void SetVolume()
    {
        OnSetVolume?.Invoke();
    }

    public static void ReturnVolume()
    {
        OnReturnVolume?.Invoke();
    }
}
