using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;
    private float camrotationX = 0f;
    private float currentCamRotX = 0f;

    [SerializeField] private Camera cam;

    [SerializeField] float cameraRotationLimit = 85f;

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
            currentCamRotX -= camrotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
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

    public void RotateCamera(float cam_rotX)
    {
        camrotationX = cam_rotX;
    }

    public void ApplyThruster(Vector3 thrusterForceVector)
    {
        thrusterForce=thrusterForceVector;
    }
}
