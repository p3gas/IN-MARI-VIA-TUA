using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class SwimmingController : MonoBehaviour
{
    [SerializeField] private float swimmingForce;
    [SerializeField] private float resistanceForce;
    [SerializeField] private float deadZone;
    [SerializeField] private float interval;
    [SerializeField] private Transform trackingSpace;

    private InputDevice _device_leftController;
    private InputDevice _device_rightController;
    private Vector3 _inputVelocity_leftController;
    private Vector3 _inputVelocity_rightController;

    private float currentWaitTime;
    private new Rigidbody rigidbody;
    private Vector3 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _device_leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        _device_rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        bool leftButtonPressed;
        bool rightButtonPressed;
        _device_leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out leftButtonPressed);
        _device_rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out rightButtonPressed);
        currentWaitTime += Time.deltaTime;
        if (leftButtonPressed && rightButtonPressed)
        {
            _device_leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out _inputVelocity_leftController);
            _device_rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out _inputVelocity_rightController);
            Vector3 localVelocity = -_inputVelocity_leftController + (-_inputVelocity_rightController);
            localVelocity *= 1f;
            if(localVelocity.sqrMagnitude > deadZone * deadZone && currentWaitTime > interval)
            {
                AddSwimmingForce(localVelocity);
                currentWaitTime = 0;
            }
        }
        if (rigidbody.velocity.sqrMagnitude > 0.01f && currentDirection != Vector3.zero)
        {
            rigidbody.AddForce(-rigidbody.velocity * resistanceForce, ForceMode.Acceleration);
        }
        else
        {
            currentDirection = Vector3.zero;
        }
        
    }

    private void AddSwimmingForce(Vector3 localVelocity)
    {
        Vector3 worldSpaceVelocity = trackingSpace.TransformDirection(localVelocity);
        rigidbody.AddForce(worldSpaceVelocity * swimmingForce, ForceMode.Impulse);
        currentDirection = worldSpaceVelocity.normalized;
    }
}
