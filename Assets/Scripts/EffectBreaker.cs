using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBreaker : MonoBehaviour
{
    void Start()
    {
        Invoke("BreakEffect", 2f);
    }

    void BreakEffect()
    {
        Destroy(this.gameObject);
    }
}
