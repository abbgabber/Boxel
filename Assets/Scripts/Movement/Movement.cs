using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour{

    public CharacterController controller;
    public Transform cam;
    public CinemachineFreeLook vcam;

    public float speed = 30f;
    public float turnTime = 0.1f;
    private float turnSpeed;
    public float fov = 70;

    void Start(){
    vcam.m_CommonLens = true;
    }
    void Update(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float scroll = Input.mouseScrollDelta.y;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if((scroll > 0) && (fov < 110f)){
            fov += 2.5f;
        }else if((scroll < 0) && (fov > 20f)){
            fov -= 2.5f;
        }
        vcam.m_Lens.FieldOfView = fov;
        
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}