using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    GameObject m_bullet;

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
    }
    

    /// <summary>
    /// 兵器の弾を生成する関数
    /// </summary>
    public void OnShot()
    {
        m_time += Time.deltaTime;
        if (m_time > coolTime)
        {
            Debug.Log("弾生成(兵器)");
            Instantiate(m_bullet, this.transform.position, Quaternion.identity);
            m_time = 0;
        }
    }
}
 