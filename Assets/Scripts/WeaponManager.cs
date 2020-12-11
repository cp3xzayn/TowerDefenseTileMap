using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject m_bullet;
    /// <summary>弾の発射間隔</summary>
    [SerializeField] float m_shootTime = 2.0f;
    private float m_time = 0f;

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
 