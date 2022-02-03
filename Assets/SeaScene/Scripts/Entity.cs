using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    private static readonly float mRadiusSquaredDistance = 5.0f;
    private static readonly float mMaxVelocity = 1.0f;
    private static readonly float mMaxCubeExtent = 10.0f;
    private static readonly float mMaxCubeExtentX = 20.0f;
    public float separationWeight = 0.8f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.7f;
    private Vector3 mVelocity = new Vector3();
    void Start()
    {
        mVelocity = transform.forward;
        mVelocity = Vector3.ClampMagnitude(mVelocity, mMaxVelocity);
    }
    void Update()
    {
        mVelocity += FlockingBehaviour();
        mVelocity = Vector3.ClampMagnitude(mVelocity, mMaxVelocity);
        transform.position += mVelocity * Time.deltaTime;
        transform.forward = mVelocity.normalized;
        Reposition();
    }
    private void Reposition()
    {
        Vector3 position = transform.position;
        if (position.x >= mMaxCubeExtentX)
        {
            position.x = mMaxCubeExtentX - 0.2f;
            mVelocity.x *= -1;
        }
        else if (position.x <= -mMaxCubeExtentX)
        {
            position.x = -mMaxCubeExtentX + 0.2f;
            mVelocity.x *= -1;
        }
        if (position.y >= mMaxCubeExtent)
        {
            position.y = mMaxCubeExtent - 0.2f;
            mVelocity.y *= -1;
        }
        else if (position.y <= -mMaxCubeExtent)
        {
            position.y = -mMaxCubeExtent + 0.2f;
            mVelocity.y *= -1;
        }
        if (position.z >= mMaxCubeExtent)
        {
            position.z = mMaxCubeExtent - 0.2f;
            mVelocity.z *= -1;
        }
        else if (position.z <= -mMaxCubeExtent)
        {
            position.z = -mMaxCubeExtent + 0.2f;
            mVelocity.z *= -1;
        }
        transform.forward = mVelocity.normalized;
        transform.position = position;
    }
    
    public List<Fish> theFlock;
    private Vector3 FlockingBehaviour()
    {
        Vector3 cohesionVector = new Vector3();
        Vector3 separateVector = new Vector3();
        Vector3 alignmentVector = new Vector3();
        int count = 0;
        for (int index = 0; index < theFlock.Count; index++)
        {
                float distance = (transform.position - theFlock[index].transform.position).sqrMagnitude;
                if (distance > 0 && distance < mRadiusSquaredDistance)
                {
                    cohesionVector += theFlock[index].transform.position;
                    separateVector += theFlock[index].transform.position - transform.position;
                    alignmentVector += theFlock[index].transform.forward;
                    count++;
                }
        }
        if (count == 0)
        {
            return Vector3.zero;
        }
        separateVector /= count;
        separateVector *= -1;
        alignmentVector /= count;
        cohesionVector /= count;
        cohesionVector = (cohesionVector - transform.position);
        Vector3 flockingVector = ((separateVector.normalized * separationWeight) + (cohesionVector.normalized * cohesionWeight) + (alignmentVector.normalized * alignmentWeight));

        return flockingVector;
    }
}