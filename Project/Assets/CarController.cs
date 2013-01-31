using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public WheelCollider[] WColForward;
	public WheelCollider[] WColBack;
	
	public Transform[] wheelsF; //1
    public Transform[] wheelsB; //1

	public float maxSteer = 30; //1
    public float maxAccel = 25; //2
    public float maxBrake = 50; //3
    
    public float wheelOffset = 0.1f; //2
    public float wheelRadius = 0.13f; //2
    
	public class WheelData{ //3
        public Transform wheelTransform; //4
        public WheelCollider col; //5
        public Vector3 wheelStartPos; //6 
        public float rotation = 0.0f;  //7
        public Vector3 startLocalRotation;
    }
    
    protected WheelData[] wheels;
    
	
    
	// Use this for initialization
	void Start () {
		
		
        wheels = new WheelData[WColForward.Length+WColBack.Length]; //8
        
        for (int i = 0; i<WColForward.Length; i++){ //9
            wheels[i] = SetupWheels(wheelsF[i],WColForward[i]); //9
        }
        
        for (int i = 0; i<WColBack.Length; i++){ //9
            wheels[i+WColForward.Length] = SetupWheels(wheelsB[i],WColBack[i]); //9
        }
		
	}
	
	private WheelData SetupWheels(Transform wheel, WheelCollider col){ //10
        WheelData result = new WheelData(); 
        
        result.wheelTransform = wheel; //10
        result.col = col; //10
        result.wheelStartPos = wheel.transform.localPosition; //10
        result.startLocalRotation.x = wheel.localRotation.x;
		result.startLocalRotation.y = wheel.localRotation.y;
		result.startLocalRotation.z = wheel.localRotation.z;
        
        return result; //10
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
        
        float accel = 0;
        float steer = 0;
                
        accel = Input.GetAxis("Vertical");  //4
        steer = Input.GetAxis("Horizontal");	 //4	
        
		Debug.Log(accel);
        
        CarMove(accel,steer); //5
        UpdateWheels();
        
    }
    
    private void UpdateWheels(){ //11
        float delta = Time.fixedDeltaTime; //12
        
        
        foreach (WheelData w in wheels){ //13
            WheelHit hit; //14
                                
            Vector3 lp = w.wheelTransform.localPosition; //15
            if(w.col.GetGroundHit(out hit)){ //16
                lp.y -= Vector3.Dot(w.wheelTransform.position - hit.point, transform.up) - wheelRadius; //17
            }else{ //18
                
                lp.y = w.wheelStartPos.y - wheelOffset; //18
            }
            w.wheelTransform.localPosition = lp; //19
            
            
            w.rotation = Mathf.Repeat(w.rotation + delta * w.col.rpm * 360.0f / 60.0f, 360.0f); //20
//            w.wheelTransform.localRotation = Quaternion.Euler(-w.rotation, w.col.steerAngle, 90.0f); //21
            //w.wheelTransform.localRotation = Quaternion.Euler(w.col.steerAngle, -w.rotation, 90.0f); //21
            w.wheelTransform.localRotation = Quaternion.Euler(w.rotation, w.col.steerAngle, 0); //21
        }	
        
    }
    
    private void CarMove(float accel,float steer){ //5
        
        foreach(WheelCollider col in WColForward){ //6
            col.steerAngle = steer*maxSteer; //6
        }
        
        if(accel == 0){ //7
            foreach(WheelCollider col in WColBack){  //7
                col.brakeTorque = maxBrake; //7
            }	
            
        }else{ //8
                                
            foreach(WheelCollider col in WColBack){ //8
                col.brakeTorque = 0; //8
                col.motorTorque = accel*maxAccel; //8
            }	
            
        }
        
                
        
    }
}
