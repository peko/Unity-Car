using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

 

	public WheelCollider frontWheel1;
	public WheelCollider frontWheel2;
	public WheelCollider rearWheel1;
	public WheelCollider rearWheel2;
	
	public Transform R_L_R;
	public Transform R_R_R;
	public Transform F_L_R;
	public Transform F_R_R;
	public Transform F_L_S;
	public Transform F_R_S;
	
		
	public float steerMax = 20f;
	public float motorMax = 10f;
	public float brakeMax = 100f;
	private float steer = 0f;
	private float motor = 0f;
	private float brake = 0f;
	private Vector3 ra = new Vector3(1,0,0);

	void Start ()
	{

		//rigidbody.centerOfMass += new Vector3(0f, 0f, -1f);

	}

	void FixedUpdate (){

		steer = Mathf.Clamp (Input.GetAxis ("Horizontal"), -1, 1);
		motor = Mathf.Clamp (Input.GetAxis ("Vertical"), 0, 1);
		brake = -1 * Mathf.Clamp (Input.GetAxis ("Vertical"), -1, 0);
		
		float torque = motorMax * motor;
		rearWheel1.motorTorque = torque;
		rearWheel2.motorTorque = torque;
		rearWheel1.brakeTorque = brakeMax * brake;
		rearWheel2.brakeTorque = brakeMax * brake;
		frontWheel1.steerAngle = steerMax * steer;
		frontWheel2.steerAngle = steerMax * steer;
		
		R_R_R.Rotate(ra, torque);
		R_L_R.Rotate(ra, torque);
		F_R_R.Rotate(ra, torque);
		F_L_R.Rotate(ra, torque);
		F_R_S.localEulerAngles = new Vector3(0, frontWheel1.steerAngle);
		F_L_S.localEulerAngles = new Vector3(0, frontWheel2.steerAngle);

	
	}

 

}