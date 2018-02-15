using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variabili
    public GameObject playerCamera;


    public float slopeStep = 0.1f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 7.0f;
    public float crouchSpeedMultiplier = 0.5f;

    public float crouchHeightMultiplier = 0.4f;

    public float viewSensitivity = 70;
    public float smoothView = 0.1f;
    public float viewAngle = 40.0f;

    public float walkSound = 1.0f;
    public float runSound = 2.0f;
    public float idleSound = 0.0f;
    public float crouchSoundMutiplier = 0.5f;

    public float stickToGroundVelocity = 0.5f;

    // componenti
    private Rigidbody rb;
    private CapsuleCollider cl;

    // stati del personaggio
    public bool isIdle = true;
    private bool isWalking;
    private bool isRunning;
    private bool isCrouch;
    public bool isClimbing;

    private bool isGrounded;
    private RaycastHit groundHit;
    private Vector2 moveDirection;
    private Vector2 viewDirection;
    private float moveSpeed;

    public float soundProduced;

    // supporto
    private Vector2 smoothV;
    private Vector2 lookVector;
    private float crouchHeightDiff;

    void Start()
    {
        // componenti
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<CapsuleCollider>();

        // calcolo altezza crouch
        crouchHeightDiff = cl.height * crouchHeightMultiplier;

        // disabilita il mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        // lettura input ed aggiornamento stato
        GetInput();

        // movimento
        Move();

        // rotazione
        Rotate();
    }

    private void GetInput()
    {
        // input movimento
        moveDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        // input camera
        viewDirection = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y"));

        // sprint
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        // crouch
        bool toggleCrouch = Input.GetKeyDown(KeyCode.LeftControl);
        bool noCrouch = Input.GetKeyDown(KeyCode.Space);

        // aggiornamento stato
        isIdle = moveDirection == Vector2.zero;

        isWalking = !isIdle && !sprint && !isClimbing;
        isRunning = !isIdle && sprint && !isClimbing;

        isCrouch = toggleCrouch ? !isCrouch : isCrouch;
        if (noCrouch && !toggleCrouch && isCrouch)
        {
            toggleCrouch = true;
            isCrouch = false;
        }

        // velocità
        moveSpeed = isRunning ? runSpeed : walkSpeed;
        moveSpeed = isCrouch ? moveSpeed * crouchSpeedMultiplier : moveSpeed;

        // crouch
        if (toggleCrouch && isCrouch)
        {
            // disattiva
            cl.height -= crouchHeightDiff;
            transform.position -= Vector3.up * crouchHeightDiff / 2.0f;
            playerCamera.transform.localPosition -= Vector3.up * crouchHeightDiff / 2.0f;
        }
        else if(toggleCrouch && !isCrouch)
        {
            // attiva
            cl.height += crouchHeightDiff;
            transform.position += Vector3.up * crouchHeightDiff / 2.0f;
            playerCamera.transform.localPosition += Vector3.up * crouchHeightDiff / 2.0f;
        }

        // suoni emessi (in-game) dal personaggio
        if(isIdle)
            soundProduced = idleSound;
        else if (isWalking)
            soundProduced = walkSound;
        else if (isRunning)
            soundProduced = runSound;
        else if (isClimbing)
            soundProduced = walkSound;

        if (isCrouch)
            soundProduced *= crouchSoundMutiplier;
    }

    void Move()
    {
        // sblocca il movimento
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // controllo contatto con il pavimento
        isGrounded = Physics.Raycast(transform.position , Vector3.down, out groundHit, cl.height / 2.0f + slopeStep);

        // muove il personaggio
        if (!isIdle)
        {
            if (isClimbing)
            {
                // arrampicata
                rb.velocity = new Vector3(0.0f, moveDirection.y * moveSpeed, 0.0f);
            }
            else
            {
                // applica al vettore di movimento l'input
                Vector3 movement =
                    transform.forward * moveDirection.y +
                    transform.right * moveDirection.x;

                // normalizza la direzione
                if (movement.sqrMagnitude > 1)
                {
                    movement.Normalize();
                }

                // applica la velocità
                movement *= moveSpeed;

                // applica il movimento
                rb.velocity = new Vector3(movement.x, rb.velocity.y - stickToGroundVelocity, movement.z);
            }
        }
        else
        {
            // blocca il personaggio per evitare slide inattesi
            if (isGrounded || isClimbing)
                rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Rotate()
    {
        // movimento lineare e non brusco
        viewDirection = Vector2.Scale(viewDirection, new Vector2(viewSensitivity * smoothView, viewSensitivity * smoothView));
        smoothV.x = Mathf.Lerp(smoothV.x, viewDirection.x, 1.0f / smoothView);
        smoothV.y = Mathf.Lerp(smoothV.y, viewDirection.y, 1.0f / smoothView);

        // agiornamento vettore visuale
        lookVector += smoothV;

        // restrizione dell'angolo di visuale lungo y
        lookVector.y = Mathf.Clamp(lookVector.y, -viewAngle, viewAngle);

        // rotazione camera lungo y
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVector.y, Vector3.right);
        // rotazione personaggio lungo x
        rb.MoveRotation(Quaternion.AngleAxis(lookVector.x, transform.up));
    }

    public void ToggleClimbing()
    {
        isClimbing = !isClimbing;
    }
}
