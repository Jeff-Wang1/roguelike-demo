using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    public GameObject player;
    public GameObject enemy;


    public Vector2 CurDirection;
    public bool isOnMoving = false;

    public int blood = 5;
    public int enemyAmount = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        for(int i = 0; i < enemyAmount; ++i)
        {
            GameObject.Instantiate(enemy, new Vector3(Random.Range(-10.0f, 8.0f), Random.Range(-4.0f, 4.0f), 1.36f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
