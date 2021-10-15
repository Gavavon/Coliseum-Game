using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float lookSensitivity = 1f;
    public float triggerAxisThreshold = 0.4f;
    public bool invertYAxis = false;
    public bool invertXAxis = false;
    [HideInInspector]
    public bool inCombat;
    [HideInInspector]

    PlayerStateHandler m_StateManager;
    //PlayerAnimaHandler m_AnimaHandler;

    private void Start()
    {
        m_StateManager = GetComponent<PlayerStateHandler>();
        //m_AnimaHandler = GetComponent<PlayerAnimaHandler>();
    }
    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }
    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f, Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }
    public float GetLookInputsHorizontal()
    {
        return GetMouse(GameConstants.k_MouseAxisNameHorizontal);
    }
    public float GetLookInputsVertical()
    {
        return GetMouse(GameConstants.k_MouseAxisNameVertical);
    }
    float GetMouse(string mouseInputName)
    {
        if (CanProcessInput())
        {
            float i = Input.GetAxisRaw(mouseInputName);

            i *= lookSensitivity;

            i *= 0.01f;

            return i;
        }

        return 0f;
    }
    public void GetMouseAttack()
    {
        if (CanProcessInput())
        {
            if (Input.GetMouseButton(0))
            {
                m_StateManager.m_CurrentState = PlayerStateHandler.State.CombAttack;
            }

            if (Input.GetMouseButton(1))
            {
                m_StateManager.m_CurrentState = PlayerStateHandler.State.CombBlock;
            }
        }
    }
}
