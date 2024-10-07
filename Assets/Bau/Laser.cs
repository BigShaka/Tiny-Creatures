using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform cameraPosition; // Referencia a la posición de la cámara
    public Transform cameraOrientation; // Referencia a la orientación de la cámara
    public GameObject laserPointer; // El objeto invisible que seguirá la punta del láser
    public float laserRange = 100f;
    public KeyCode laserKey = KeyCode.Mouse0;
    public bool isLaserUnlocked = false;
    private LineRenderer laserLine;
    private bool isLaserActive;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserPointer.SetActive(false);
    }

    private void Update()
    {
        if(isLaserUnlocked == true)
        {
            Debug.Log(laserKey);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Funciono");
                ActivateLaser();
            }

            if (Input.GetMouseButtonUp(0))
            {
                DeactivateLaser();
            }

            if (isLaserActive)
            {
                UpdateLaser();
            }
        }    
    }

    private void ActivateLaser()
    {
        Debug.Log("FuncionoA");
        isLaserActive = true;
        laserLine.enabled = true;
        laserPointer.SetActive(true);
    }

    private void DeactivateLaser()
    {
        isLaserActive = false;
        laserLine.enabled = false;
        laserPointer.SetActive(false);
    }

    private void UpdateLaser()
    {
        Ray ray = new Ray(cameraPosition.position, cameraOrientation.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserRange))
        {
            laserLine.SetPosition(0, cameraPosition.position);
            laserLine.SetPosition(1, hit.point);
            laserPointer.transform.position = hit.point;
        }
        else
        {
            laserLine.SetPosition(0, cameraPosition.position);
            laserLine.SetPosition(1, cameraPosition.position + cameraOrientation.forward * laserRange);
            laserPointer.transform.position = cameraPosition.position + cameraOrientation.forward * laserRange;
        }
    }
        public void UnlockLaser()
    {
        isLaserUnlocked = true;
    }
}
