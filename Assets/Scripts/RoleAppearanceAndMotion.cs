using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAppearanceAndMotion : MonoBehaviour
{
    public Camera camera;
    public List<Sprite> allDirectionImage = new List<Sprite>();

    private SpriteRenderer sp;
    [SerializeField]
    private float MoveSpeed = 0.04f;
    [SerializeField]
    private float AnimSpeed = 10.0f;
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isOnMoving)
        {
            DoMove(GameManager.Instance.CurDirection, MoveSpeed);
            DoPlayImage(GameManager.Instance.CurDirection);
        }
    }

    void DoMove(Vector2 direction,float speed)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);
        this.transform.position += new Vector3(direction.x, direction.y, 0) * speed;
    }

    void DoPlayImage(Vector2 direction)
    {
        if (allDirectionImage.Count >= 6)
        {
            if (direction.y > 0)
            {
                if (Mathf.Sin(Time.time * AnimSpeed) > 0) sp.sprite = allDirectionImage[0];
                else sp.sprite = allDirectionImage[1];
            }
            else if (direction.x < 0 && direction.y < 0)
            {
                if (Mathf.Sin(Time.time * AnimSpeed) > 0) sp.sprite = allDirectionImage[2];
                else sp.sprite = allDirectionImage[3];
            }
            else
            {
                if (Mathf.Sin(Time.time * AnimSpeed) > 0) sp.sprite = allDirectionImage[4];
                else sp.sprite = allDirectionImage[5];
            }
        }
    }

    
}
