using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    private float speed = 10.0f;
    private Vector2 direction;

    private float lives = 3;
    private float firstTime;

    void Start()
    {
        firstTime = Time.time;
        direction = GameManager.Instance.CurDirection;
        if (direction.x < 0.01 && direction.y < 0.01) direction.x = -1;
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if (Time.time -firstTime > lives) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag!="Player")
            Destroy(this.gameObject);
    }

}
