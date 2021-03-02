using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBreaker : MonoBehaviour
{
    //敵死亡時の効果音
    [SerializeField] AudioClip m_death;
    AudioSource audioSource;
    void Start()
    {
        Invoke("BreakEffect", 2f);

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(m_death);
    }

    void BreakEffect()
    {
        Destroy(this.gameObject);
    }
}
