using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public enum GateState
    {
        Opened,
        Closed
    }
    public GateState m_GateState;
    public GameObject Gate;
    public float GateSpeed;
    public Transform Opened;
    public Transform Closed;

    void OnTriggerEnter(Collider other)
    {
        if (m_GateState == GateState.Opened)
        {
            m_GateState = GateState.Closed;
        }
        else 
        {
            m_GateState = GateState.Opened;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (m_GateState == GateState.Opened)
        {
            m_GateState = GateState.Closed;
        }
        else
        {
            m_GateState = GateState.Opened;
        }

    }
    public void moveGate(Transform endLocation) 
    {
        if (Gate.transform.position.y != endLocation.position.y)
        {
            Vector3 TempDirection;
            if (Gate.transform.position.y > endLocation.position.y)
            {
                TempDirection = Vector3.down;
            }
            else 
            {
                TempDirection = Vector3.up;
            }
            Gate.transform.Translate(TempDirection * GateSpeed * Time.deltaTime);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!Freeze.instance.FREEZE)
        {
            switch (m_GateState)
            {
                case GateState.Opened:
                    moveGate(Opened);
                    break;
                case GateState.Closed:
                    moveGate(Closed);
                    break;

            }
        }
    }
}
