using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe_Wrench : MovableObj
{
    [SerializeField]
    private BoxCollider mainCollider, handleCollider;

    [SerializeField]
    private Button tightAnimBtn;

    private Animator animator;

    [SerializeField]
    private Animation anim;

    private Transform[] childObjs;

    private bool animNot, started = false;

    [SerializeField]
    private GameObject capGO;

    // Start is called before the first frame update
    void Start()
    {
        mainCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        mainCollider.enabled = true;
        handleCollider.enabled = false;
        animator.enabled = false;
        childObjs = GetComponentsInChildren<Transform>();
        tightAnimBtn.gameObject.SetActive(false);
        tightAnimBtn.onClick.AddListener(TightenAnimRun);
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        animNot = !anim.isPlaying;
        HandlesFunction();
        if (animNot && started)
        {
            movable = true;
            mainCollider.enabled = true;
        }

    }

    private void HandlesFunction()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Ray");

            if (hit.collider == handleCollider && Input.GetMouseButtonDown(0))
            {
                Debug.Log("hitCol");

                handleCollider.transform.localPosition = new Vector3(0.27f, 10.33f, -0.6f);      //tightening grip for plier
                tightAnimBtn.gameObject.SetActive(true);
                capGO.transform.parent = this.transform;
            }

        }
    }

    private void TightenAnimRun()
    {
        animator.enabled = true;
        animator.SetTrigger("Tighten");
        started = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pipes" && canRemoveCap)
        {
            for (int i = 1; i < childObjs.Length; i++)
            {
                childObjs[i].localRotation = Quaternion.Euler(0, 90, 90);

            }

            mainCollider.center = new Vector3(mainCollider.center.x, 10.34f, mainCollider.center.z);
            transform.localPosition = new Vector3(0.122f, 0.012f, -0.075f);
            movable = false;
            mainCollider.enabled = false;
            handleCollider.enabled = true;
        }
    }

}
