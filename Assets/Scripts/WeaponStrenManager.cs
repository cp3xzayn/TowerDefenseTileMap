using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStrenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Rayの長さ
            float maxDistance = 10;

            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);

            //なにかと衝突した時だけそのオブジェクトの名前をログに出す
            if (hit.collider.gameObject.name == "Weapon(Clone)")
            {
                hit.collider.gameObject.GetComponent<WeaponManager>().OnClickWeapon();
            }
        }
    }
}
