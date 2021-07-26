using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pliers : MovableObj
{
    [SerializeField]
    private BoxCollider mainCollider, handleCollider;

    // Start is called before the first frame update
    void Start()
    {
        mainCollider = GetComponent<BoxCollider>();
        mainCollider.enabled = true;
        handleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandlesFunction();
    }

    private void HandlesFunction()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == handleCollider && Input.GetMouseButtonDown(0))
            {
                handleCollider.transform.localRotation = Quaternion.Euler(130f, -1.5f, -1.5f);         //tightening grip for plier
                canRemoveCap = true;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggerd");
        if (other.gameObject.tag == "Pipes")
        {
            Debug.Log("collision : " + other.gameObject.name);
            transform.localRotation = Quaternion.Euler(45, 90, 0);
            transform.localPosition = new Vector3(-0.231f, 0.069f, -0.22f);
            movable = false;
            mainCollider.enabled = false;
            handleCollider.enabled = true;
        }
    }
}
