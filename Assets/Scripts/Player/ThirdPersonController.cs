using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour {

    #region Variables
    //Singleton
    private static ThirdPersonController instance;
    public static ThirdPersonController Instance {
        get { return instance; }
    }

    //Events
    public static event Action<bool> OnGroundedChange;

    //input Fields
    private PlayerInput _playerInput;
    private ThirdPersonInputs _input;
    private InputAction _move;
    private InputAction _sprint;
    private bool _IsCurrentDeviceMouse {
        get {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }


    //movement Fields
    private Rigidbody _myRB;
    [Header("Movement Fields")]
    [Space(5)]
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float maxSpeed = 1f;
    private Vector3 forceDirection = Vector3.zero;
    //Smovement
    private float targetCurve = 1f;
    private float currentCurve = 0f;
    [SerializeField] private float turnSpeed = .4f;
    [SerializeField] private AnimationCurve turncurve;

    //NewMovement
    [Header("NewMovementFields")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("Sprint speed of the character in m/s")]
    public bool IsPressingSprint = false;

    [Tooltip("Speed at wich the Character goes from 0-Move or Move-Sprint")]
    public float SpeedChangeRate = 10.0f;

    [Tooltip("How fast the Character Turns towards the new Direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    private float _speed;
    private float _targetRotation = 0.0f;
    private GameObject _mainCamera;
    private float _refRotationVelocity;

    //Jump
    [Header("Jump Fields")]
    [Space(5)]
    [SerializeField] private float jumpForce = 1f;
    public LayerMask GroundLayers;
    [SerializeField] private float _fallMultiplier;


    private bool IsGrounded;
    private float GroundedOffset = 0.7f;
    private float GroundedRadius = 0.335f;

    //FPS
    [Header("Undefined Rest of the Variables")]
    [Space(5)]
    [SerializeField]
    private int _targetFrameRate = -1;

    //Camera
    [SerializeField] private Camera playerCam;
    #endregion

    private void Awake() {
        //Singelton the First
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }

        //Get Main Camera
        if (_mainCamera == null) {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        _myRB = this.GetComponent<Rigidbody>();
        _input = new ThirdPersonInputs();


    }

    private void Start() {
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
    }

    private void OnEnable() {
        _input.PlayerDefault.Enable();
        _input.PlayerDefault.Jump.started += DoJump;
        _move = _input.PlayerDefault.Move;
        _sprint = _input.PlayerDefault.Sprint;
        _sprint.started += OnSprintPressed;
        _sprint.canceled += OnSprintReleased;
    }

    private void OnSprintPressed(InputAction.CallbackContext obj) {;
        IsPressingSprint = true;
    }

    private void OnDisable() {
        _input.PlayerDefault.Disable();
        _input.PlayerDefault.Jump.started -= DoJump;
        _sprint.started -= OnSprintPressed;
        _sprint.canceled -= OnSprintReleased;
    }

    private void OnSprintReleased(InputAction.CallbackContext obj) {
        IsPressingSprint = false;
    }

    private void Update() {

        Application.targetFrameRate = _targetFrameRate;

        UpdateCurveIfMoving();

        SmoothOutJump();

    }


    //FIXED UPDATE
    private void FixedUpdate() {
        //movement
        //Move();
        NewMove();

        //Looking at running direction
        //LookAt();

        //check for Ground
        GroundCheck();
    }

    private void NewMove() {
     
        // set target speed based on _move speed, sprint speed and if sprint is pressed
        float targetSpeed = IsPressingSprint ? SprintSpeed : MoveSpeed;

        // if there is no input, set the target speed to 0
        if (_move.ReadValue<Vector2>() == Vector2.zero) targetSpeed = 0.0f; _speed = 0.0f;

        // Debug.Log(_move.ReadValue<Vector2>());
        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_myRB.velocity.x, 0.0f, _myRB.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset) {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        } else {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(_move.ReadValue<Vector2>().x, 0.0f, _move.ReadValue<Vector2>().y).normalized;

        // if there is a _move input rotate player when the player is moving
        if (_move.ReadValue<Vector2>() != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _refRotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        //Debug.Log(_myRB.velocity);
        
        // _move the player
        _myRB.AddForce ((targetDirection.normalized * _speed), ForceMode.VelocityChange);


        //attempt to only clamp Horizontal Velocity and leave vertical untoched - honestly no clue what is going on 

        Vector3 horizontalVelocity = _myRB.velocity;
        horizontalVelocity.y = 0f;
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, targetSpeed);
        _myRB.velocity = horizontalVelocity + Vector3.up * _myRB.velocity.y;

        //_myRB.velocity = Vector3.ClampMagnitude(_myRB.velocity, targetSpeed);


        /*Debug.Log(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _myRB.transform.position.y, 0.0f) * Time.deltaTime);*/

    }

    private void Move() {

        /*forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(playerCam) * movementForce;
        forceDirection += _move.ReadValue<Vector2>().y * GetCameraForward(playerCam) * movementForce;*/

        forceDirection.x += _move.ReadValue<Vector2>().x * movementForce;
        forceDirection.z += _move.ReadValue<Vector2>().y * movementForce;



        // normalise input direction
        Vector3 inputDirection = new Vector3(_move.ReadValue<Vector2>().x, 0.0f, _move.ReadValue<Vector2>().y).normalized;
        // if there is a _move input rotate player when the player is moving
        if (_move.ReadValue<Vector2>() != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _refRotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _myRB.AddForce(targetDirection + forceDirection, ForceMode.VelocityChange);
        forceDirection = Vector3.zero;

        //make Jump faster on falling
        /*if (_myRB.velocity.y < 0f)
            _myRB.velocity -= Vector3.down * 3 * Physics.gravity.y * Time.fixedDeltaTime;*/

        //cap Max speed
        Vector3 horizontalVelocity = _myRB.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.magnitude > maxSpeed)
            //horizontalVelo needs this so jump isnt interrupted
            _myRB.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * _myRB.velocity.y;

    }

    private void LookAt() {
        Vector3 direction = _myRB.velocity;
        direction.y = 0f;

        if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)

            //this.myRB.rotation = Quaternion.LookRotation(direction, Vector3.up);
            _myRB.rotation = Quaternion.Lerp(_myRB.rotation, Quaternion.LookRotation(direction, Vector3.up), turncurve.Evaluate(currentCurve));

        else
            _myRB.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera) {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera) {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void UpdateCurveIfMoving() {

        if (_move.ReadValue<Vector2>() != Vector2.zero)
            //turnsmoothcurve Setup
            currentCurve = Mathf.MoveTowards(currentCurve, targetCurve, turnSpeed * Time.deltaTime);

        else
            currentCurve = 0f;

        //Debug.Log("Currently" + currentCurve);
    }

    // JUMPING LOGIC
    private void DoJump(InputAction.CallbackContext obj) {
        Debug.Log("DoJump Called");
        
        if (IsGrounded) {
            //forceDirection += Vector3.up * jumpForce;
            float jumpForceWithMass = jumpForce * _myRB.mass;
            _myRB.AddForce((Vector3.up * jumpForceWithMass) + Physics.gravity * 2, ForceMode.Impulse);
        }
    }

    private void SmoothOutJump() {
        if (_myRB.velocity.y < 0) {
            _myRB.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void GroundCheck() {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }


    //GIZMOS
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Gizmos.color = Color.green; // Set the color of the sphere
        Gizmos.DrawWireSphere(spherePosition, GroundedRadius); // Draw the wire sphere
    }
#endif


}
