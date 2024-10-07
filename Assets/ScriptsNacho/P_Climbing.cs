using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_Climbing : MonoBehaviour
{
    [Header("Climbing")]
    public LayerMask whatIsStair;
    public LayerMask whatIsGround;
    private float horizontalInput;
    private float verticalInput;

    [Header("References")]
    public Transform orientation;
    private P_Movement pm;
    private Rigidbody rb;

    [Header("Detection")]
    public float stairCheckDistance;
    public float minJumpHeight;
    private RaycastHit frontStairHit;
    private bool stairFront;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<P_Movement>();
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void CheckForStairs()
    {
        stairFront = Physics.Raycast(transform.position, orientation.forward, out frontStairHit, stairCheckDistance, whatIsStair);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForStairs();
        StateMachine();
    }

    void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if (stairFront && verticalInput > 0 && AboveGround())
        {
            pm.climbing = true;
            if (AboveGround())
            {
                rb.velocity = new Vector3(rb.velocity.x * horizontalInput, Vector3.up.y * verticalInput * pm.climbSpeed, 0);
            }
        }
        else pm.climbing = false;

        void StartClimbing()
        {
            pm.climbing = true;
            rb.velocity = new Vector3(rb.velocity.x * horizontalInput, Vector3.up.y * verticalInput, 0);
        }
    }
}
