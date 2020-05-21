using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy"|| collision.tag == "Weapon")
        {
            GameManager.Instance.blood--;
            if (GameManager.Instance.blood < 0)
            {
                this.transform.position = new Vector3(0, 0, 1.36f);
                GameManager.Instance.blood = 5;
            }
        }
    }
}
