using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Direction_Haddle : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public GameObject button_Img, button_Point;
    public Vector3 buttonInitialPos;
    public Camera camera;
    [SerializeField]
    float radius = 1.5f;



    //滑动中
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 buttonScreenPos = camera.WorldToScreenPoint(button_Img.transform.position);
        Vector2 curDif = new Vector2(eventData.position.x - buttonScreenPos.x, eventData.position.y - buttonScreenPos.y);
        curDif = curDif / 50.0f;
        if (curDif.magnitude > radius) curDif = (curDif) / curDif.magnitude * radius;
        button_Point.transform.position = new Vector3(curDif.x, curDif.y, 0) + buttonInitialPos;
        GameManager.Instance.CurDirection = curDif / curDif.magnitude;
    }

    //触摸开始
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.isOnMoving = true;

        Vector3 buttonScreenPos = camera.WorldToScreenPoint(button_Img.transform.position);
        Vector2 curDif = new Vector2(eventData.position.x - buttonScreenPos.x, eventData.position.y - buttonScreenPos.y);
        curDif = curDif / 50.0f;
        if (curDif.magnitude > radius) curDif = (curDif) / curDif.magnitude * radius;
        button_Point.transform.position = new Vector3(curDif.x, curDif.y, 0) + buttonInitialPos;
        GameManager.Instance.CurDirection = curDif / curDif.magnitude;
    }

    //触摸结束
    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.isOnMoving = false;
        button_Point.transform.position = buttonInitialPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonInitialPos = button_Point.transform.position;
    }
}
