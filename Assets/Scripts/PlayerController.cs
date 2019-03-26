using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float thrusterForce = 1000f;
    [SerializeField] private float FuelBurnSpeed =0.3f;
    [SerializeField] private float FuelRegenSpeed = 0.1f;
    [SerializeField] private float FuelAmount = 1f;

    public float GetFuelAmount()
    {
        return FuelAmount;
    }

    [Header("Spring Settings")]
    //[SerializeField] private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField] private float jointSpring = 20f;
    [SerializeField] private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSettings(jointSpring);
	}
	
	// Update is called once per frame
	void Update () {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 vel = (movHorizontal + movVertical) * speed;
        motor.Move(vel);

        animator.SetFloat("ForwardVelocity", zMov);

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rot = new Vector3(0f, yRot, 0f) * lookSensitivity;
        motor.Rotate(rot);

        float xRot = Input.GetAxisRaw("Mouse Y");
        float cam_rot = xRot * lookSensitivity;
        motor.RotateCamera(cam_rot);

        Vector3 thrusterForceVector = Vector3.zero;
        if (Input.GetButton("Jump")&&FuelAmount>0f)
        {
            FuelAmount -= FuelBurnSpeed * Time.deltaTime;

            if (FuelAmount >= 0.01f)
            {
                thrusterForceVector = Vector3.up * thrusterForce;
                SetJointSettings(0f);
            }
        }
        else
        {
            FuelAmount += FuelRegenSpeed * Time.deltaTime;
            SetJointSettings(jointSpring);
        }
        FuelAmount = Mathf.Clamp(FuelAmount, 0f, 1f);

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
