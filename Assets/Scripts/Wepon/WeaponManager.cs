﻿using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //弾のオブジェクト
    GameObject m_bullet;
    GameObject m_bullet1;

    /// <summary>兵器の弾生成の間隔</summary>
    float coolTime;
    float m_time = 0;

    /// <summary>
    /// 兵器の情報をセットする関数
    /// </summary>
    /// <param name="cTime"></param>
    public void SetWeaponData(float cTime)
    {
        coolTime = cTime;
    }

    void Start()
    {
        m_bullet = Resources.Load<GameObject>("Bullet");
        m_bullet1 = Resources.Load<GameObject>("Bullet1");
    }
    

    /// <summary>
    /// 兵器の弾を生成する関数
    /// </summary>
    public void OnShot()
    {
        m_time += Time.deltaTime;
        if (m_time > coolTime)
        {
            //オブジェクトの名前で判断
            if (this.name == "Weapon(Clone)")
            {
                //弾を生成する
                Debug.Log("弾生成");
                Instantiate(m_bullet, this.transform.position, Quaternion.identity);
            }
            if (this.name == "Weapon1(Clone)")
            {
                //弾を生成する
                Debug.Log("弾1生成");
                Instantiate(m_bullet1, this.transform.position, Quaternion.identity);
            }
            m_time = 0;
        }
    }



    /// <summary> 兵器強化に必要なコスト </summary>
    int m_needCost = 10;
    public int NeedCost { get { return m_needCost; } }

    SpriteRenderer m_spriteRenderer;
    /// <summary> 兵器が強化されたときのSprite </summary>
    [SerializeField] Sprite[] m_weaponSprite;
    int m_spriteIndex = 0;

    public void OnClickWeapon()
    {
        Debug.Log("兵器強化");
        //Spriteを入れ替える
        m_spriteIndex++;
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_weaponSprite[m_spriteIndex];
        //Statusを上げる
    }

}
 