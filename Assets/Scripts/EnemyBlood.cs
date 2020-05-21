using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlood : MonoBehaviour
{
    [SerializeField]
    private int blood = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            blood--;
            if (blood < 0) Destroy(this.gameObject);
        }
    }
}
