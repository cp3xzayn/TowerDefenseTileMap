using UnityEngine;
using System;

[Serializable]
public class InputJsonWeaponData
{
    public float[] m_weaponShootData;
    public int[] m_bulDamage;
    public float[] m_limitRange;
    public float[] m_weapon1ShootData;
    public int[] m_bul1Damage;
    public float[] m_limitRange1;
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
    int m_weaponIndex = 0;

    /// <summary> 兵器の発射間隔 </summary>
    float m_shootingTime;
    /// <summary> 兵器1の発射間隔 </summary>
    float m_shooting1Time;
    /// <summary>
    /// Jsonファイルから兵器の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadWeaponData(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_shootingTime = inputJsonWeaponData.m_weaponShootData[m_weaponIndex];
        return m_shootingTime;
    }
    /// <summary>
    /// Jsonファイルから兵器1の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadWeapon1Data(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_shooting1Time = inputJsonWeaponData.m_weapon1ShootData[m_weaponIndex];
        return m_shooting1Time;
    }

    /// <summary> 兵器の弾のダメージ </summary>
    int m_bulDamage;
    /// <summary> 兵器1の弾のダメージ </summary>
    int m_bul1Damage;
    /// <summary>
    /// Jsonファイルから兵器の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadBulletDamage(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_bulDamage = inputJsonWeaponData.m_bulDamage[m_weaponIndex];
        return m_bulDamage;
    }
    /// <summary>
    /// Jsonファイルから兵器1の発射間隔を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int LoadBullet1Damage(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_bul1Damage = inputJsonWeaponData.m_bul1Damage[m_weaponIndex];
        return m_bul1Damage;
    }

    /// <summary> 兵器の射程範囲 </summary>
    float m_bulRange;
    /// <summary> 兵器1の射程範囲 </summary>
    float m_bul1Range;
    /// <summary>
    /// Jsonファイルから兵器の射程範囲を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadBulletRange(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_bulRange = inputJsonWeaponData.m_limitRange[m_weaponIndex];
        return m_bulRange;
    }
    /// <summary>
    /// Jsonファイルから兵器の射程範囲を取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float LoadBullet1Range(int index)
    {
        m_weaponIndex = index;
        string inputString = Resources.Load<TextAsset>("Json/WeaponData").ToString();
        InputJsonWeaponData inputJsonWeaponData = JsonUtility.FromJson<InputJsonWeaponData>(inputString);
        m_bul1Range = inputJsonWeaponData.m_limitRange1[m_weaponIndex];
        return m_bul1Range;
    }

    /// <summary>
    /// 兵器の情報をセットする関数
    /// </summary>
    /// <param name="cTime"></param>
    public void SetWeaponData(float cTime)
    {
        coolTime = cTime;
    }

    AudioSource audioSource;
    
    void Start()
    {
        m_bullet = Resources.Load<GameObject>("Bullet");
        m_bullet1 = Resources.Load<GameObject>("Bullet1");
        audioSource = GetComponent<AudioSource>();
        // 発射間隔を初期化する
        if (this.gameObject.name == "Weapon(Clone)")
        {
            m_shootingTime = LoadWeaponData(m_weaponIndex);
            SetWeaponData(m_shootingTime);
        }
        else if (this.gameObject.name == "Weapon1(Clone)")
        {
            m_shooting1Time = LoadWeapon1Data(m_weaponIndex);
            SetWeaponData(m_shooting1Time);
        }
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
                GameObject go = Instantiate(m_bullet, this.transform.position, Quaternion.identity);
                Bullet b = go.GetComponent<Bullet>();
                b.Damage = LoadBulletDamage(m_weaponIndex);
                b.Range = LoadBulletRange(m_weaponIndex);
            }
            if (this.name == "Weapon1(Clone)")
            {
                //弾を生成する
                Debug.Log("弾1生成");
                GameObject go = Instantiate(m_bullet1, this.transform.position, Quaternion.identity);
                Bullet b = go.GetComponent<Bullet>();
                b.Damage = LoadBullet1Damage(m_weaponIndex);
                b.Range = LoadBullet1Range(m_weaponIndex);
            }
            m_time = 0;
        }
    }

    /// <summary> 兵器強化に必要なコスト </summary>
    int m_needCost = 10;
    public int NeedCost { get { return m_needCost; } }
    /// <summary> 兵器が強化されたときのSound </summary>
    [SerializeField] AudioClip m_weaponStren;
    SpriteRenderer m_spriteRenderer;
    /// <summary> 兵器が強化されたときのSprite </summary>
    [SerializeField] Sprite[] m_weaponSprite;

    /// <summary>
    /// 兵器が強化されたときの処理
    /// </summary>
    public void OnClickWeapon()
    {
        Debug.Log("兵器強化");
        audioSource.PlayOneShot(m_weaponStren);
        //兵器の強化が最大になるまでの処理を追加で書く
        //Spriteを入れ替える
        m_weaponIndex++;
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_weaponSprite[m_weaponIndex];
        //兵器の弾生成間隔の強化
        m_shootingTime = LoadWeaponData(m_weaponIndex);
        SetWeaponData(m_shootingTime);
    }

    /// <summary>
    /// 兵器1が強化されたときの処理
    /// </summary>
    public void OnClickWeapon1()
    {
        Debug.Log("兵器強化");
        audioSource.PlayOneShot(m_weaponStren);
        //兵器の強化が最大になるまでの処理を追加で書く
        //Spriteを入れ替える
        m_weaponIndex++;
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_weaponSprite[m_weaponIndex];
        //兵器の弾生成間隔の強化
        m_shooting1Time = LoadWeaponData(m_weaponIndex);
        SetWeaponData(m_shooting1Time);
    }
}
 