using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    protected Rigidbody rb;
    protected GameObject cam;
    protected Collider col;
    [SerializeField] protected float throwForce = 10.0f;

    [SerializeField] protected bool isHeld;
    public bool canBeHeld;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera");
        col = gameObject.GetComponent<Collider>();
        isHeld = transform.parent != GameObject.Find("Tool").transform ? false : true;
        if(isHeld){
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld && Input.GetMouseButtonDown(0)){
            ActionOne();
        }

        if(isHeld && Input.GetMouseButtonDown(1)){
            ActionTwo();
        }

        if(!isHeld && canBeHeld){
            PickUp();
        }
    }

    protected abstract void ActionOne();

    protected virtual void ActionTwo(){
        Release();
        rb.AddForce((cam.transform.forward + new Vector3(0, 0.5f, 0)) * throwForce, ForceMode.Impulse);
    }

    protected void PickUp(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tool = GameObject.Find("Tool");
            DropItems(tool);
            transform.SetParent(tool.transform);

            isHeld = true;
            rb.isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;            
        }
    }

    protected void Release(){
        isHeld = false;
        transform.parent = null;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    protected void DropItems(GameObject tool){
        foreach (Transform child in tool.transform)
        {
            Tool tempTool;
            child.gameObject.TryGetComponent<Tool>(out tempTool);
            if(tempTool != null){
                tempTool.Release();
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject == GameObject.Find("Player")){
            Debug.Log("other is player");
            canBeHeld = true;
        }
        else if(other.gameObject == GameObject.Find("Ground")){
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject == GameObject.Find("Player")){
            canBeHeld = false;
        }
    }
}
