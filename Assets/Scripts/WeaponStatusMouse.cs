using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatusMouse : MonoBehaviour
{
    /// <summary> Weaponのステータスを表示するUI </summary>
    [SerializeField] GameObject m_weaponStatus;

    private void Update()
    {
        GameObjectRay();
    }

    void GameObjectRay()
    {
        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Rayの長さ
        float maxDistance = 10;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
        //なにかと衝突した時だけそのオブジェクトの名前をログに出す
        if (hit)
        {
            Debug.Log(hit);
            if (hit.collider.gameObject.tag == "WeaponButton")
            {
                Debug.Log("1234");
            }
        }
    }
}