using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHpManager : MonoBehaviour
{
    /// <summary> 拠点の耐久値 </summary>
    int m_baseHP = 20;
 
    public void DecreaseHP(int eneAttack)
    {
        m_baseHP -= eneAttack;
        Debug.Log("拠点の耐久値" + m_baseHP);
    }
}
