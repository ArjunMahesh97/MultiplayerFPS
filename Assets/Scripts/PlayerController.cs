using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float thrusterForce = 1000f;

    [Header("Spring Settings")]
    //[SerializeField] private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField] private float jointSpring = 20f;
    [SerializeField] private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
	}
	
	// Update is called once per frame
	void Update () {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 vel = (movHorizontal + movVertical).normalized * speed;
        motor.Move(vel);

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rot = new Vector3(0f, yRot, 0f) * lookSensitivity;
        motor.Rotate(rot);

        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cam_rot = new Vector3(xRot, 0f, 0f) * lookSensitivity;
        motor.RotateCamera(cam_rot);

        Vector3 thrusterForceVector = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            thrusterForceVector = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        motor.ApplyThruster(thrusterForceVector);
    }

    private void SetJointSettings(float jointspring)
    {
        joint.yDrive = new JointDrive {
            /*mode = jointMode, */
            positionSpring = jointspring,
            maximumForce = jointMaxForce
        };
    }
}
