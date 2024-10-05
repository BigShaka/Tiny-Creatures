using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV_CAM : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float _xAxis;
    float _yAxis;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yAxis += mouseX;
        _xAxis -= mouseY;
        _xAxis = Mathf.Clamp(_xAxis, -90, 90);

        transform.rotation = Quaternion.Euler(_xAxis, _yAxis, 0);
        orientation.rotation = Quaternion.Euler(0, _yAxis, 0);

    }
}
