using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private enum EnemyState { StandState,WanderState,AlertState,AttackState,ChaseState}
    private EnemyState currentEnemyState;

    public float wanderRadius;
    public float alertRadius;
    public float attackRadius;
    public float chaseRadius;

    public float moveSpeed;
    public float chaseSpeed;

    private float distanceFromPlayer;
    private float distanceFromInitial;
    private Vector3 targetRotation;

    private GameObject curPlayer;
    private Vector3 initialPosition;

    private float lastTime;
    public float changeStateTime;

    private bool inAlertState = false;

    public GameObject enemyWeapon;
    private float lastAttackTime;
    public float AttackIntervalTime;

    public GameObject Alert;
    private GameObject alert;

    private void Start()
    {
        alert = GameObject.Instantiate(Alert, transform.position, Quaternion.identity);
        alert.SetActive(false);
        currentEnemyState = EnemyState.StandState;
        lastTime = Time.time;
        lastAttackTime = Time.time;
        curPlayer = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        RandomAction();
    }

    void RandomAction()
    {
        lastTime = Time.time;
        float num = Random.Range(0.0f, 1.0f);
        if (num < 0.5) currentEnemyState = EnemyState.StandState;
        else
        {
            currentEnemyState = EnemyState.WanderState;
            targetRotation = new Vector3(0, 0, Random.Range(0.0f, 4.0f) * 90);
        }
    }
    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.StandState:
                if (Time.time - lastTime > changeStateTime)
                {
                    RandomAction();
                }
                EnemyDistanceCheck();
                break;
            case EnemyState.WanderState:
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
                transform.rotation = Quaternion.Euler(targetRotation);
                if (Time.time - lastTime > changeStateTime)
                {
                    RandomAction();
                }
                WanderRadiusCheck();
                break;
            case EnemyState.AlertState:
                alert.SetActive(true);
                alert.transform.position = transform.position + new Vector3(0, 1.5f, 0);
                targetRotation = new Vector3(0, 0, Vector3Angle(new Vector3(1, 0, 0), new Vector3(curPlayer.transform.position.x - transform.position.x, curPlayer.transform.position.y - transform.position.y, 0)));
                transform.rotation = Quaternion.Euler(targetRotation);
                if (!inAlertState)
                {
                    inAlertState = true;
                
                }
                AlertRadiusCheck();
                break;
            case EnemyState.AttackState:
                targetRotation = new Vector3(0, 0, Vector3Angle(new Vector3(1, 0, 0), new Vector3(curPlayer.transform.position.x - transform.position.x, curPlayer.transform.position.y - transform.position.y, 0)));
                transform.rotation = Quaternion.Euler(targetRotation);
                AttackRadiusCheck();
                break;
            case EnemyState.ChaseState:
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * chaseSpeed);
                transform.rotation = Quaternion.Euler(targetRotation);
                ChaseRadiusCheck();
                break;
        }
    }

    private void EnemyDistanceCheck()
    {
        distanceFromPlayer = Vector3.Distance(curPlayer.transform.position, transform.position);
        if(distanceFromPlayer < chaseRadius)
        {
            currentEnemyState = EnemyState.ChaseState;
        }
        else if(distanceFromPlayer < attackRadius)
        {
            currentEnemyState = EnemyState.AttackState;
        }
        else if (distanceFromPlayer < alertRadius)
        {
            currentEnemyState = EnemyState.AlertState;
        }
    }

    private void WanderRadiusCheck()
    {
        distanceFromInitial = Vector3.Distance(initialPosition, transform.position);
        distanceFromPlayer = Vector3.Distance(transform.position, curPlayer.transform.position);

        if (distanceFromPlayer < chaseRadius)
        {
            currentEnemyState = EnemyState.ChaseState;
        }
        else if (distanceFromPlayer < attackRadius)
        {
            currentEnemyState = EnemyState.AttackState;
        }
        else if (distanceFromPlayer < alertRadius)
        {
            currentEnemyState = EnemyState.AlertState;
        }

        if(distanceFromInitial > wanderRadius)
        {
            targetRotation = new Vector3(0, 0, Vector3Angle(new Vector3(1, 0, 0), new Vector3(initialPosition.x - transform.position.x, initialPosition.y - transform.position.y, 0)));
        }
    }

    private void AlertRadiusCheck()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, curPlayer.transform.position);
        if (distanceFromPlayer < chaseRadius)
        {
            inAlertState = false;
            currentEnemyState = EnemyState.ChaseState;
            alert.SetActive(false);
        }
        if(distanceFromPlayer > chaseRadius && distanceFromPlayer < attackRadius)
        {
            inAlertState = false;
            currentEnemyState = EnemyState.AttackState;
            alert.SetActive(false);
        }
        if (distanceFromPlayer > alertRadius)
        {
            inAlertState = false;
            alert.SetActive(false);
            RandomAction();
        }
    }

    private void AttackRadiusCheck()
    {
        if (Time.time - lastAttackTime > AttackIntervalTime)
        {
            GameObject.Instantiate(enemyWeapon, transform.position, Quaternion.identity);
            lastAttackTime = Time.time;
        }
        distanceFromPlayer = Vector3.Distance(transform.position, curPlayer.transform.position);
        if (distanceFromPlayer < chaseRadius)
        {
            currentEnemyState = EnemyState.ChaseState;
        }
        if(distanceFromPlayer > attackRadius)
        {
            currentEnemyState = EnemyState.AlertState;
        }
    }

    private void ChaseRadiusCheck()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, curPlayer.transform.position);
        // 追逐时不断随着人物改变方向
        targetRotation = new Vector3(0, 0, Vector3Angle(new Vector3(1, 0, 0), 
            new Vector3(curPlayer.transform.position.x - transform.position.x, curPlayer.transform.position.y - transform.position.y, 0)));
        
        if (distanceFromPlayer > chaseRadius)
        {
            currentEnemyState = EnemyState.AttackState;
        }
    }

    public float Vector3Angle(Vector3 fromVector,Vector3 toVector)
    {
        float angle = Vector3.Angle(fromVector, toVector); //求出两向量之间的夹角
        Vector3 normal = Vector3.Cross(fromVector, toVector);//叉乘求出法线向量
        angle *= Mathf.Sign(Vector3.Dot(normal, new Vector3(0, 0, 1))); //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向
        return angle;
    }
}
