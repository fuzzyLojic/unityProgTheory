using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float lookSpeed = 2.0f;

    private float yRotation = 0.0f;
    private float xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    private void Move(){
        float verticalInput = Input.GetAxis("Vertical"); 
        float horizontalInput = Input.GetAxis("Horizontal");        

        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private void Look(){
        float horizontalLook = Input.GetAxis("Mouse X"); 
        float verticalLook = Input.GetAxis("Mouse Y");

        xRotation += verticalLook * lookSpeed;
        if(xRotation > 90.0f) xRotation = 90.0f;
        if(xRotation < -90.0f) xRotation = -90.0f;
        
        yRotation += horizontalLook * lookSpeed;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
