using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Player Objects")]
    public Camera playerCamera;
    public float cameraLockMax;
    public float cameraLockMin;

    [Header("Verticle Player Movement")]
    public float gravityDownForce = 20f;
    public LayerMask groundCheckLayers = -1;
    public float groundCheckDistance = 0.05f;
    public Transform groundCheck;

    [Header("Horizontal Player Movement")]
    public float maxSpeedOnGround = 10f;
    public float movementSharpnessOnGround = 15;
    public float nonCombatSpeedBoost = 2;
    public float killHeight = -50f;

    public float rotationSpeed = 200f;
    public float blockingRotationMultiplier = 0.4f;

    public Vector3 characterVelocity { get; set; }
    public bool isGrounded { get; private set; }
    public bool isWalking { get; private set; }
    public bool isDead { get; private set; }

    PlayerInputHandler m_InputHandler;
    CharacterController m_Controller;
    PlayerStateHandler m_StateManager;
    //public Text m_Text;
    Vector3 m_GroundNormal;
    float m_CameraVerticalAngle = 0f;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();

        m_InputHandler = GetComponent<PlayerInputHandler>();

        m_Controller.enableOverlapRecovery = true;

        m_StateManager = GetComponent<PlayerStateHandler>();

    }
    void Update()
    {

        outBounds();

        GroundCheck();

        HandleCharacterMovement();

        if (m_InputHandler.inCombat) 
        {
            //playerCamera.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //m_Text.text = "Current State: " + m_StateManager.m_CurrentState;

    }
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundCheckLayers);
    }
    void outBounds()
    {
        if (!isDead && transform.position.y < killHeight)
        {
            //m_Stats.Kill();
        }
    }
    void HandleCharacterMovement()
    {
        {
            if (m_InputHandler.invertXAxis)
            {
                transform.Rotate(new Vector3(0f, (-1 * m_InputHandler.GetLookInputsHorizontal() * rotationSpeed), 0f), Space.Self);
            }
            else
            {
                transform.Rotate(new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * rotationSpeed), 0f), Space.Self);
            }

        }

        {
            if (m_InputHandler.invertYAxis)
            {
                m_CameraVerticalAngle += m_InputHandler.GetLookInputsVertical() * rotationSpeed;
            }
            else
            {
                m_CameraVerticalAngle -= m_InputHandler.GetLookInputsVertical() * rotationSpeed;
            }

            m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -cameraLockMax, cameraLockMin);

            playerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);
        }

        bool inCombat = m_InputHandler.inCombat;
        {
            float speedModifier = inCombat ? 1f : nonCombatSpeedBoost;

            Vector3 worldspaceMoveInput = transform.TransformVector(m_InputHandler.GetMoveInput());

            if (isGrounded)
            {
                Vector3 targetVelocity = worldspaceMoveInput * maxSpeedOnGround * speedModifier;

                characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, movementSharpnessOnGround * Time.deltaTime);
            }
            else
            {

                characterVelocity += worldspaceMoveInput * Time.deltaTime;

                float verticalVelocity = characterVelocity.y;

                Vector3 horizontalVelocity = Vector3.ProjectOnPlane(characterVelocity, Vector3.up);

                horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, speedModifier);

                characterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

                characterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
            }
        }

        Vector3 capsuleBottomBeforeMove = GetCapsuleBottomHemisphere();
        Vector3 capsuleTopBeforeMove = GetCapsuleTopHemisphere(m_Controller.height);
        m_Controller.Move(characterVelocity * Time.deltaTime);

        if (Physics.CapsuleCast(capsuleBottomBeforeMove, capsuleTopBeforeMove, m_Controller.radius, characterVelocity.normalized, out RaycastHit hit, characterVelocity.magnitude * Time.deltaTime, -1, QueryTriggerInteraction.Ignore))
        {
            characterVelocity = Vector3.ProjectOnPlane(characterVelocity, hit.normal);
        }
    }
    Vector3 GetCapsuleBottomHemisphere()
    {
        // Gets the center point of the bottom hemisphere of the character controller capsule  
        return transform.position + (transform.up * m_Controller.radius);
    }
    Vector3 GetCapsuleTopHemisphere(float atHeight)
    {
        // Gets the center point of the top hemisphere of the character controller capsule
        return transform.position + (transform.up * (atHeight - m_Controller.radius));
    }
}
