using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _playerJumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;

    private bool _playerIsGrounded;
    private Vector3 _playerVelocity;
    private CharacterController _playerCharacterController;
    private PlayerInput _playerInput;

    private Transform _mainCameraTransform;

    [Tooltip("New Input System Actions List: Move")]
    private InputAction _playerMoveInputAction;
    [Tooltip("New Input System Actions List: Jump")]
    private InputAction _playerJumpInputAction;
    [Tooltip("New Input System Actions List: Jump")]
    private InputAction _playerShootInputAction;
    
    // Move vectors
    private Vector3 _playerMoveVector3 = Vector3.zero;
    private Vector2 _inputControlAction = Vector2.zero;
    
    // Rotation Speed parameter, in Degrees
    [Tooltip("Rotation Speed parameter, in Degrees")]
    [SerializeField] private float _playerRotationSpeed = 5.0f;
    
    // Shooting
    //
    [Tooltip("The Bullet's Prefab to get & Instantiate the GameObjects from")]
    [SerializeField] private GameObject _playerBulletPrefab;
    
    [Tooltip("The Bullet's GameObject's Parent in the Hierarchy (for organizing, clean and neat)")]
    [SerializeField] private Transform _playerBulletParentTransform;
    
    [Tooltip("The Gun's barrel Transform")]
    [SerializeField] private Transform _gunBarrelTransform;

    [SerializeField] private float _bulletHitMissDistance = 25.0f;
    
    
    #region Animations
    
    // Animations, Blend Trees & Animator Controller:
    //
    /// <summary>
    /// 3D Character (Player)'s 'Animator Controller'
    /// </summary>
    private Animator _playerAnimator;
    //
    // References (constants) to Animator/Animations Parameters:
    //
    private static readonly int _MOVE_X_ANIMATION_PARAMETER_ID = Animator.StringToHash("MoveX");
    //
    private static readonly int _MOVE_Z_ANIMATION_PARAMETER_ID = Animator.StringToHash("MoveZ");
    //
    // (Pistol...) Jump Animation:
    //
    private static readonly int _JUMP_ANIMATION_ANIMATION_PARAMETER_ID = Animator.StringToHash("Pistol Jump");
    
    // Smooth Damp Input System (for a smooth transitioning between each movement):
    //
    [Tooltip("Current Player Animations Blending Blending value")]
    [SerializeField]
    Vector2 _currentPlayerAnimationsRunStrafeBlendVector2;
    //
    [Tooltip("Current Player Animations Blending Velocity value")]
    [SerializeField]
    Vector2 _playerAnimationsRunStrafeVelocity;
    //
    [Tooltip("Current Player Animations Blending SMOOTH TIME value. The Smaller the value, the Faster the transitions between different Input Actions (and Animations) will be")]
    [SerializeField] private float _playerAnimationRunStrafeSmoothTime = 0.1f;

    [Tooltip("Current Player Animation's TRANSITION TIME value. The Smaller the value, the Faster the transitions between different Animations will be")]
    [SerializeField] private float _playerAnimationRunToPistolJumpTransitionTime = 0.15f;

    #endregion Animations


    private void Awake()
    {
        // Get this GameObject (GO) (Player's) Character Controller:
        _playerCharacterController = GetComponent<CharacterController>();
        
        // Game Controls:
        // 1- Get this GO's PlayerInput Component:
        _playerInput = GetComponent<PlayerInput>();
        
        // Get Main Camera Transform reference
        //
        if (Camera.main != null)
        {
            _mainCameraTransform = Camera.main.transform;
        }
        
        // 2- Get New Input System Action List:
        //
        _playerMoveInputAction = _playerInput.actions["Move"];
        _playerJumpInputAction = _playerInput.actions["Jump"];
        _playerShootInputAction = _playerInput.actions["Shoot"];
        
        // Lock the Mouse Cursor to the center of the screen:
        //
        Cursor.lockState = CursorLockMode.Locked;

        
        // ANIMATIONS (Animator Controller) setup:
        //
        _playerAnimator = GetComponent<Animator>();

    }

    
    private void OnEnable()
    {
        _playerShootInputAction.performed += _ => ShootGun();
    }


    private void OnDisable()
    {
        _playerShootInputAction.performed -= _ => ShootGun();
    }


    // private void Start()
    // {
    // }

    
    void Update()
    {
        _playerIsGrounded = _playerCharacterController.isGrounded;
        if (_playerIsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // 0- Get Input from Player:
        // Get (New) input System's Control player actions:
        _inputControlAction = _playerMoveInputAction.ReadValue<Vector2>();
        //
        // Apply a SOFTENING / SMOOTHING REDUCTION of the INTENSITY of the INPUT received over Time (e.g.: an raw INPUT = [ (X, Y) = ( -0.7, -1 ) ] ...and then suddenly the player changes to [ ( 1, 1 ) !!! ]... so instead of using one FRAME = ( -0.7, -1 ) AND THE NEXT FRAME => ( 1, 1 )... we would SOFTEN the VALUE of that FRAME to = ( -0.6, -0.9 ) ): To Move the Player's 3D Character (Animations Blending Tree) using the Smoothing Animations Blending CURRENT VALUE:
        //
        _currentPlayerAnimationsRunStrafeBlendVector2 = Vector2.SmoothDamp(_currentPlayerAnimationsRunStrafeBlendVector2, _inputControlAction, ref _playerAnimationsRunStrafeVelocity, _playerAnimationRunStrafeSmoothTime);
        
        
        // Set Input from Player (in a private variable):
        //
        ////// ORIGINAL, before introducing Animations Blend Trees: _playerMoveVector3.Set(_inputControlAction.x, 0, _inputControlAction.y);
        //
        // Improvement with Animations: Move the Player according to SMOOTH (DAMPED) values, taking into consideration previous values of the Movement (and the previous Animation...):
        //
        _playerMoveVector3.Set(_currentPlayerAnimationsRunStrafeBlendVector2.x, 0, _currentPlayerAnimationsRunStrafeBlendVector2.y);
        //
        // 1- MOVE PLAYER:
        // Take into account the Main Camera's Direction, when the Player is MOVING: if we press the Button (Control or Joystick) to move to the Right (for example), then it will move the Player to the RIGHT DIRECTION in relation to the CAMERA'S PERSPECTIVE: 
        //
        _playerMoveVector3 = _playerMoveVector3.x * _mainCameraTransform.right.normalized +
                             _playerMoveVector3.z * _mainCameraTransform.forward.normalized;
        //
        // Set Vertical movement to zero:
        //
        _playerMoveVector3.y = 0f;
        
        // (Finally) Set the Movement, according to the Time between Frames (Time.deltaTime):
        //
        _playerCharacterController.Move(_playerMoveVector3 * (Time.deltaTime * _playerSpeed));
        
        
        // ANIMATIONS:
        // Blend Strafe Animation
        // x:
        _playerAnimator.SetFloat(_MOVE_X_ANIMATION_PARAMETER_ID, _currentPlayerAnimationsRunStrafeBlendVector2.x);
        //
        // z:
        _playerAnimator.SetFloat(_MOVE_Z_ANIMATION_PARAMETER_ID, _currentPlayerAnimationsRunStrafeBlendVector2.y);
        

        // 2- JUMP:
        // Changes the height position of the player..
        if (_playerJumpInputAction.triggered && _playerIsGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_playerJumpHeight * -3.0f * _gravityValue);
            
            // ANIMATIONS:
            // 1- Pistol JUMP:
            //
            _playerAnimator.CrossFade(_JUMP_ANIMATION_ANIMATION_PARAMETER_ID, _playerAnimationRunToPistolJumpTransitionTime);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _playerCharacterController.Move(_playerVelocity * Time.deltaTime);
        
        // 3- ROTATE:
        // Rotate towards the Main Camera's Direction:
        // We get the Rotation Angle in Degrees around the Y-axis (the Green in color)
        //
        float targetAngle = _mainCameraTransform.eulerAngles.y;
        //
        // We need a NEW ROTATION, so we calculate using a Quaternion:
        //
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        //
        // We assign the new Rotation (i.e.: the Quaternion) to the Player's Transform (in a SMOOTH way: using Lerp):
        //
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _playerRotationSpeed * Time.deltaTime);

    }// End Update
    
    
    #region AlMartson's Custom Methods


    /// <summary>
    /// We'll use a Raycast to fake the shooting:
    /// We will make it seem as a shot from the center of the Main Camera, towards the CENTER of the screen (using the Camera forward vector3 taken from its Transform). 
    /// </summary>
    private void ShootGun()
    {
        
        // Bullet Prefab Instantiation:
        // NOTE: TODO TO-DO: To make an OBJECT POOL for Optimizing this one:
        //
        GameObject bullet = GameObject.Instantiate(_playerBulletPrefab, _gunBarrelTransform.position, Quaternion.identity, _playerBulletParentTransform);
            
        // Optimization TO-DO TODO: Set this BulletController.cs as an Interface, and make sure ALL BULLETS ALWAYS have a Bullet Controller!!!
        //
        BulletController bulletController = bullet.GetComponent<BulletController>();

        
        RaycastHit raycastHit;
        //
        // We'll shoot the Raycast Vector3 from the Center of the Main Camera, forwards in a Straight Line (in Spanish: 'en una Linea Recta' ;)
        //
        if (Physics.Raycast(_mainCameraTransform.position, _mainCameraTransform.forward, out raycastHit,
                Mathf.Infinity))
        {

            if (bulletController)
            {
                // Assign the Collision Object to my: Target (a Vector3 attribute)
                //
                bulletController.Target = raycastHit.point;
                //
                // State that there was a Collision: true
                //
                bulletController.IsThereABulletHit = true;

            }// End if (bulletController)
        }
        else
        {
            // No collision
            //
            if (bulletController)
            {
                // Assign the Collision Object to my: Target (a Vector3 attribute).
                // PROBLEM: THere is NO COLLISION, so we are going to fake it: using the Camera forward vector3 + an ARBITRARY DISTANCE: we will say it disappear after travelling some distance...
                //
                bulletController.Target = _mainCameraTransform.position + _mainCameraTransform.forward * _bulletHitMissDistance;
                //
                // State that there was a Collision: true
                //
                bulletController.IsThereABulletHit = false;

            }// End if (bulletController)     
            
        }// End else of if (Physics.Raycast....) - Collision!
        
    }// End private void ShootGun()

    #endregion AlMartson's Custom Methods
    
}