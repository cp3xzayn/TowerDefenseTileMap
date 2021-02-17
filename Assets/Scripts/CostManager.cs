using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    [SerializeField] int m_cost= 4;

    /// <summary>
    /// プロパティ
    /// </summary>
    public int Cost { get { return m_cost; } }

    /// <summary> コストのTextオブジェクト</summary>
    [SerializeField] GameObject m_costObject;
    /// <summary> コストを表示するテキスト</summary>
    Text m_costText;

    void Start()
    {
        m_costText = m_costObject.GetComponent<Text>();
        m_costText.text = "コスト : " + m_cost;
    }

    void Update()
    {
        m_costText.text = "コスト : " + m_cost;

        //Rayを飛ばし、当たったものが兵器だった場合強化する
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Rayの長さ
            float maxDistance = 10;

            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);

            //Hitしたオブジェクトが兵器の時
            if (hit.collider.gameObject.name == "Weapon(Clone)")
            {
                hit.collider.gameObject.GetComponent<WeaponManager>().OnClickWeapon();
                int needCost = hit.collider.gameObject.GetComponent<WeaponManager>().NeedCost;
                m_cost -= needCost;
            }
            else if (hit.collider.gameObject.name == "Weapon1(Clone)")
            {
                hit.collider.gameObject.GetComponent<WeaponManager>().OnClickWeapon1();
                int needCost = hit.collider.gameObject.GetComponent<WeaponManager>().NeedCost;
                m_cost -= needCost;
            }
        }
    }

    /// <summary>
    /// コストが増えたときの処理
    /// </summary>
    public void UpCost()
    {
        m_cost++;
    }

    /// <summary>
    /// コストが減らしたときの処理
    /// </summary>
    public void DecreaseCost()
    {
        m_cost--;
    }
}
