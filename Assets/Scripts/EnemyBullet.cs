using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector2 direction;

    private GameObject player;

    private float lives = 3;
    private float firstTime;

    void Start()
    {
        firstTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        if (direction.x < 0.01 && direction.y < 0.01) direction.x = -1;
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if (Time.time - firstTime > lives) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(this.gameObject);
    }
}
