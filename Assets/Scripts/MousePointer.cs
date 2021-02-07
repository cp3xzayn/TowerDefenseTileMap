using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    /// <summary> MouseカーソルのＸ座標 </summary>
    float m_mouseX;
    /// <summary> MouseカーソルのＸ座標 </summary>
    float m_mouseY;

    [SerializeField] GameObject m_weapon1Status;
    [SerializeField] GameObject m_weapon2Status;

    void Update()
    {
        WeaponStatusVisible();
    }

    void WeaponStatusVisible()
    {
        m_mouseX = Input.mousePosition.x;
        m_mouseY = Input.mousePosition.y;

        if (m_mouseX > 30 && m_mouseX < 65 && m_mouseY > 360 && m_mouseY < 405)
        {
            m_weapon1Status.SetActive(true);
        }
        else
        {
            m_weapon1Status.SetActive(false);
        }

        if (m_mouseX > 30 && m_mouseX < 65 && m_mouseY > 232 && m_mouseY < 270)
        {
            m_weapon2Status.SetActive(true);
        }
        else
        {
            m_weapon2Status.SetActive(false);
        }
    }
}
