using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour{

    public CharacterController controller;
    public Transform cam;
    public CinemachineFreeLook vcam;
    public Animator animator;
    public Vector3 jumpAxis;
    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField]
    private float fov = 70f;

    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float gravity = -9.82f; 
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private bool isGrounded;
    
    [SerializeField]
    private float turnTime = 0.1f;
    private float turnSpeed;
    
    void Update(){

        // Jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && jumpAxis.y < 0){
            jumpAxis.y = -2f;
        }
        if(Input.GetButtonDown("Jump") && isGrounded){
            jumpAxis.y = jumpHeight;
        }
        jumpAxis.y += gravity * Time.deltaTime;
        controller.Move(jumpAxis * Time.deltaTime);

        // Scrolling for FOV
        float scroll = Input.mouseScrollDelta.y;
        vcam.m_Lens.FieldOfView = fov;
        if((scroll < 0) && (fov < 110f)){
            fov += 2.5f;
        }else if((scroll > 0) && (fov > 20f)){
            fov -= 2.5f;
        }

        //Movement in X and Z, moves the camera with the mouse and makes the direction the mouse is looking forward
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        animator.SetFloat("Speed", direction.sqrMagnitude);
        
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}