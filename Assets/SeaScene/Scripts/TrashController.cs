using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashController : MonoBehaviour
{
    private XRGrabInteractable interactor = null;
    public bool IsGrabbing;
    private float grabbing_time;
    public float destroyDepth = -50.0f;

    private void Awake()
    {

        interactor = GetComponent<XRGrabInteractable>();
        IsGrabbing = false;

    }

    private void Update()
    {
        if (transform.position.y < destroyDepth)
            Destroy(transform.gameObject);
        if (interactor.isSelected) 
        {
            if (IsGrabbing)
            {
                grabbing_time += Time.deltaTime;
                if (grabbing_time > 2f) Destroy(gameObject);
            }
            else
            {
                IsGrabbing = true;
                grabbing_time = 0f;
            }
        }
        else
        {
            IsGrabbing = false;
        }
    }
}
