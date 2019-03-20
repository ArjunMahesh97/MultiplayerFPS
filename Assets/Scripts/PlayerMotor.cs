using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camrotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;

    public Camera cam;


    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector3 vel)
    {
        velocity = vel;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformRotation()
    {
        rigidBody.MoveRotation(transform.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            cam.transform.Rotate(-camrotation);
        }
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(transform.position + velocity*Time.fixedDeltaTime);
        }

        if (thrusterForce != Vector3.zero)
        {
            rigidBody.AddForce(thrusterForce * Time.fixedDeltaTime,ForceMode.Acceleration);
        }
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    public void RotateCamera(Vector3 cam_rot)
    {
        camrotation = cam_rot;
    }

    public void ApplyThruster(Vector3 thrusterForceVector)
    {
        thrusterForce=thrusterForceVector;
    }
}
