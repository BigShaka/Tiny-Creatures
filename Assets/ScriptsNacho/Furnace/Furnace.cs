using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    bool _ON = false;

    public float timeON;
    public float timeOFF;
    float _timer = 0;

    private void Start()
    {
        _timer = timeOFF;
    }
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_ON)
            {
                _ON = false;
                _timer = timeOFF;
            }
            else
            {
                _ON = true;
                _timer = timeON;
            }

            print(_ON);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        P_Movement player = collision.gameObject.GetComponent<P_Movement>();

        if (player != null)
        {
            if (_ON)
            {
                player.Death();
            }
            else
            {
                return;
            }
        }
    }
}
