using UnityEngine;
using System;

[Serializable]
public class InputJsonWeaponData
{
    public float[] m_weaponShootData;
    public int[] m_bulDamage;
    public float[] m_limitRange;
}

public class WeaponManager : MonoBehaviour
{
    //弾のオブジェクト
    GameObject m_bullet;
    GameObject m_bullet1;

    /// <summary>兵器の弾生成の間隔</summary>
    float coolTime;
    float m_time = 0;

    /// <summary> 配列の要素数 </summary>
    int m_shootTimeIndex = 0;
    /// <summary> 発射間隔 </summary>
    float m_shootingTime;

    /// <summary>
    /// Jsonファイルから兵器の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadWeaponData(int index)
    {
        m_shootTimeIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_shootingTime = inputJsonWeaponData.m_weaponShootData[m_shootTimeIndex];
        return m_shootingTime;
    }

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

        // 発射間隔を初期化する
        m_shootingTime = LoadWeaponData(m_shootTimeIndex);
        SetWeaponData(m_shootingTime);
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

    /// <summary>
    /// 兵器が強化されたときの処理
    /// </summary>
    public void OnClickWeapon()
    {
        Debug.Log("兵器強化");
        //兵器の強化が最大になるまでの処理を追加で書く
        //Spriteを入れ替える
        m_spriteIndex++;
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_weaponSprite[m_spriteIndex];
        //Statusを上げる
        //兵器の弾生成間隔の強化
        m_shootTimeIndex++;
        m_shootingTime = LoadWeaponData(m_shootTimeIndex);
        SetWeaponData(m_shootingTime);
        Debug.Log("STime" + m_shootingTime);
    }
}
 