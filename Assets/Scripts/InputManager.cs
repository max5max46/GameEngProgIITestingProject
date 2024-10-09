using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    //public InputAction playerControls;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction run;
    private InputAction pause;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
    }

    private void HandleCameraInput()
    {
        cameraInput = look.ReadValue<Vector2>();

        // Get mouse input for the camera
        //cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Get scroll input for camera zoom
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Send inputs to CameraManager
        cameraManager.zoomInput = scrollInput;
        cameraManager.cameraInput = cameraInput;        
    }


    private void OnEnable()
    {
        playerControls = new PlayerInputActions();

        move = playerControls.Player.Move;
        look = playerControls.Player.Look;
        jump = playerControls.Player.Jump;
        run = playerControls.Player.Run;
        pause = playerControls.Player.Pause;
        move.Enable();
        look.Enable();
        jump.Enable();
        run.Enable();
        pause.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        run.Disable();
        pause.Disable();
    }


    private void HandleMovementInput()
    {
        movementInput = move.ReadValue<Vector2>();
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = pause.WasPerformedThisFrame(); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (run.IsPressed() && moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        jumpInput = jump.WasPerformedThisFrame(); // Detect jump input (spacebar)
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }






}
