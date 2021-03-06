using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    /// <summary>攻撃力 </summary>
    private int attack;
    /// <summary>HP </summary>
    private int hp;
    /// <summary>拠点を攻撃する頻度 </summary>
    private float coolTime;
    float m_time = 0;
    /// <summary>弾のダメージ </summary>
    int bulletDamage;
    /// <summary>　敵死亡時のエフェクト </summary>
    GameObject m_effect;

    GameObject m_costMana;
    GameObject m_baseHPObj;
    BaseHpManager m_base;

    [SerializeField] AudioClip m_enemyAttack;
    AudioSource audioSource;

    [SerializeField] GameObject m_enemyHPObject;
    Slider m_enemyHPSlider;

    void Start()
    {
        m_baseHPObj = GameObject.Find("BaseHPManager");
        m_base = m_baseHPObj.GetComponent<BaseHpManager>();
        m_effect = Resources.Load<GameObject>("EnemyDeath");
        m_costMana = GameObject.Find("CostManager");
        audioSource = GetComponent<AudioSource>();
        m_enemyHPSlider = m_enemyHPObject.GetComponent<Slider>();
    }

    /// <summary>
    /// 敵のデータをセットする関数
    /// </summary>
    /// <param name="attack"></param>
    /// <param name="hp"></param>
    /// <param name="coolTime"></param>
    public void SetEnemyData(int attack, ref int eHP, float coolTime)
    {
        this.attack = attack;
        this.coolTime = coolTime;
        hp = eHP;
    }


    /// <summary> 拠点を攻撃する </summary>
    public void Attack()
    {
        //CoolTime経過したら敵が攻撃する
        m_time += Time.deltaTime;
        if (m_time > coolTime)
        {
            Debug.Log("攻撃" + attack);
            EneAttackSound();
            m_base.DecreaseHP(attack);
            m_time = 0f;
        }
    }

    /// <summary>
    /// 弾のダメージをセットし、敵のHPを減らす関数
    /// </summary>
    /// <param name="bDamage"></param>
    public void SetBulletDamage(int bDamage)
    {
        bulletDamage = bDamage;
        hp -= bulletDamage;
        Debug.Log($"現在のHPは{hp}");
        m_enemyHPSlider.value = hp;
        //敵のHPが0以下になったら
        if (hp <= 0)
        {
            //敵が倒されたときにエフェクトを発生させる
            DeathEffectGenerate();
            //Costを増やす
            CostManager c = m_costMana.GetComponent<CostManager>();
            c.UpCost();
            //敵を破壊する
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// 敵が攻撃したときに効果音を鳴らす
    /// </summary>
    void EneAttackSound()
    {
        audioSource.PlayOneShot(m_enemyAttack);
    }

    /// <summary>
    /// 敵死亡時にエフェクトを生成する
    /// </summary>
    void DeathEffectGenerate()
    {
        //敵が倒されたときにエフェクトを発生させる
        Instantiate(m_effect, this.transform.position, Quaternion.identity);
    }
}
