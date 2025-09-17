using UnityEngine;

public class PlayerCarControler : MonoBehaviour
{
    [Header("Wheel Collider")]
    public WheelCollider FrontLeftWheelCollider;
    public WheelCollider FrontRightWheelCollider;
    public WheelCollider RearLeftWheelCollider;
    public WheelCollider RearRightWheelCollider;

    [Header ("Wheel Transform")]
    public Transform FrontLeftWheelTransform;
    public Transform FrontRightWheelTransform;
    public Transform RearLeftWheelTransform;
    public Transform RearRightWheelTransform;


    [Header("Car Engine")]
    public float accelerationForce = 1000f;
    public float CurrentAcceleration = 0f;
    public float breakingForce = 3000f;
    public float _currentBreakingForce = 0f;

    [Header("Car Steering")]
    public float WheelTorque = 30f;
    private float currentSteerAngle = 0f;

    private void Update()
    {
        MoveCar();
        SteerCar();
        ApplyBreak();

    }//Update

    private void MoveCar()
    {
        CurrentAcceleration = accelerationForce * Input.GetAxis("Vertical");

        FrontLeftWheelCollider.motorTorque = CurrentAcceleration;
        FrontRightWheelCollider.motorTorque = CurrentAcceleration;
        RearLeftWheelCollider.motorTorque = CurrentAcceleration;
        RearRightWheelCollider.motorTorque = CurrentAcceleration;

    }//MoveCar

    private void SteerCar()
    {
        currentSteerAngle = WheelTorque * Input.GetAxis("Horizontal");

        FrontLeftWheelCollider.steerAngle = currentSteerAngle;
        FrontRightWheelCollider.steerAngle = currentSteerAngle;

        // Call SteerWheel Method
        SteerWheel(FrontRightWheelCollider,FrontRightWheelTransform);
        SteerWheel(FrontLeftWheelCollider,FrontLeftWheelTransform);
        SteerWheel(RearRightWheelCollider,RearRightWheelTransform);
        SteerWheel(RearLeftWheelCollider,RearLeftWheelTransform);

    }//SteerCar

    private void SteerWheel(WheelCollider wheelColliders, Transform wheelTransforms)
    {
        // Get Wheel colider posion and rotation apply it to Visual wheels

        Vector3 _positon;
        Quaternion _rotaton;
        
        wheelColliders.GetWorldPose(out _positon, out _rotaton);

        wheelTransforms.position = _positon;
        wheelTransforms.rotation = _rotaton;

    }//SterWheel

    private void ApplyBreak()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _currentBreakingForce = breakingForce;
        }
        else
        {
            _currentBreakingForce = 0f;
        }

        FrontLeftWheelCollider.brakeTorque = _currentBreakingForce;
        FrontRightWheelCollider.brakeTorque = _currentBreakingForce;
        RearLeftWheelCollider.brakeTorque = _currentBreakingForce;
        RearRightWheelCollider.brakeTorque = _currentBreakingForce;

    }//ApplyBreak

}
 