using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float up = 1.0f;
    /// <summary>移動速度</summary>
    [SerializeField] float m_walkSpeed = 1f;
    SpriteRenderer m_sprite;
    Animator m_anim;
    Rigidbody2D m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        //Mapの情報を取得する
        Test t = GetComponent<Test>();
        int[,] m = t.Map;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HowTOMove()
    {

    }
}
