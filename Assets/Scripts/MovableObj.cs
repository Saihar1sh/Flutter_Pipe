using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObj : MonoBehaviour
{
    [SerializeField]
    protected bool movable;

    protected static bool canRemoveCap;

    private Vector3 mouseOffset;
    private float mouseZ, mouseY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousePos);

    }

    //Mouse Events
    private void OnMouseDown()
    {
        mouseZ = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseY = transform.position.y;
        mouseOffset = transform.position - GetMousePos();                               // offset = gameObj position - mouse hit point
    }
    private void OnMouseDrag()
    {
        if (!movable)                                                                   //if not movable the mouse position is not tracked and the obj won't move
            return;
        Debug.Log("mouseDr");

        Vector3 pos = GetMousePos() + mouseOffset;
        pos.y = mouseY;                                                                 //height should not change
        transform.position = pos;
    }
}

public enum Tools
{
    None,
    Pipe_Wrench,
    Plier,

}