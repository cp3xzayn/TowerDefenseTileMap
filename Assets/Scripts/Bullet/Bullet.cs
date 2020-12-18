using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int bulletDamage;
    float bulletRange;

    public float BulletRange { get { return bulletRange; } }

    /// <summary>
    /// 弾のステータスをセットする関数
    /// </summary>
    /// <param name="bDamage"></param>
    /// <param name="bRange"></param>
    public void SetBullet(int bDamage, float bRange)
    {
        bulletDamage = bDamage;
        bulletRange = bRange;
    }
}
