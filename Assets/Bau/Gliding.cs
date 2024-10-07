using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    [Header("Gliding")]
    public KeyCode glideKey = KeyCode.LeftAlt;
    public float glideSpeed = 5f;
    public float glideDrag = 1f;
    public float glideGravityScale = 0.5f;
    public bool isGlidingUnlocked = false;

    private Rigidbody rb;
    private bool isGliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isGlidingUnlocked == true)
        {
            if (Input.GetKeyDown(glideKey))
            {
                StartGliding();
            }

            if (Input.GetKeyUp(glideKey))
            {
                StopGliding();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isGliding)
        {
            Glide();
        }
    }

    private void StartGliding()
    {
        isGliding = true;
        rb.drag = glideDrag;
        rb.useGravity = false;
    }

    private void StopGliding()
    {
        isGliding = false;
        rb.drag = 0;
        rb.useGravity = true;
    }

    private void Glide()
    {
        Vector3 glideDirection = transform.forward * glideSpeed;
        rb.AddForce(glideDirection, ForceMode.Acceleration);

        // Simulacion de planecion
        rb.AddForce(Vector3.down * glideGravityScale, ForceMode.Acceleration);
    }
        public void UnlockGliding()
    {
        isGlidingUnlocked = true;
    }

}
