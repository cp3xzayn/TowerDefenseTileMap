using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    GameObject m_bullet;
    GameObject m_bullet1;

    void Start()
    {
        m_bullet = Resources.Load<GameObject>("Bullet");
        m_bullet1 = Resources.Load<GameObject>("Bullet1");
    }

    void Update()
    {
    }

    public void OnShot()
    {
        //弾を生成する
        Debug.Log("弾生成");
        Instantiate(m_bullet, this.transform.position, Quaternion.identity);
    }
}
 