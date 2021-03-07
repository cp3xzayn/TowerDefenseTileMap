using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventSubscriber : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.OnSetVolume += OnSetVolume;
        AudioManager.OnReturnVolume += OnReturnVolume;
    }

    public virtual void OnSetVolume()
    {
        Debug.Log("a");
    }
    
    public virtual void OnReturnVolume()
    {
        Debug.Log("b");
    }
}
