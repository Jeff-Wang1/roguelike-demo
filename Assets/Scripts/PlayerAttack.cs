using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public void attack()
    {
        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }
}
