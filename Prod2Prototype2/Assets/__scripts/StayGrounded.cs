using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayGrounded : MonoBehaviour
{
    private Vector3 _homePosition;

    private float _homewardForce = .1f;
    
    private void Awake()
    {
        _homePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // find out if the ball is at its home position
        if (gameObject.transform.position != _homePosition)
        {
            Vector3 direction = (_homePosition - gameObject.transform.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * _homewardForce);
        }
    }

    public Vector3 GetHomePosition()
    {
        return _homePosition;
    }
}
