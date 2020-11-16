using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //スタートと終わりの目印
    [SerializeField] Transform m_startMarker;
    [SerializeField] Transform m_endMarker;
    // スピード
    [SerializeField]float m_speed = 1.0f;
    /// <summary>二点間の距離/summary>
    private float m_distance;

    // Start is called before the first frame update
    void Start()
    {
        //シリアライズに入れたものの座標を取ってきているので、prefabの座標同士で計算してしまっている。
        //生成されたものの座標を取ってきて計算しなければならない。
        //二点間の距離を代入
        m_distance = Vector2.Distance(m_startMarker.position, m_endMarker.position);
        Debug.Log(m_startMarker);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の位置
        float nowLocation = (Time.time * m_speed) / m_distance;
        //オブジェクトの移動
        this.transform.position = Vector2.Lerp(m_startMarker.position, m_endMarker.position, nowLocation);
    }
}
