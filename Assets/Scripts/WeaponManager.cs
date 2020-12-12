using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject m_bullet;

    void Start()
    {
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
 