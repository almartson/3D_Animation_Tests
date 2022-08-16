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

    [Tooltip("New Input System Actions List: Move")]
    private InputAction _playerMoveInputAction;
    [Tooltip("New Input System Actions List: Jump")]
    private InputAction _playerJumpInputAction;
    
    // Move vectors
    private Vector3 _playerMoveVector3 = Vector3.zero;
    private Vector2 _inputControlAction = Vector2.zero;
    
    private void Awake()
    {
        // Get this GameObject (GO) (Player's) Character Controller:
        _playerCharacterController = GetComponent<CharacterController>();
        
        // Game Controls:
        // 1- Get this GO's PlayerInput Component:
        _playerInput = GetComponent<PlayerInput>();

    }

    private void Start()
    {
    
        // 2- Get New Input System Action List:
        //
        _playerMoveInputAction = _playerInput.actions["Move"];
        _playerJumpInputAction = _playerInput.actions["Jump"];

    }

    
    void Update()
    {
        _playerIsGrounded = _playerCharacterController.isGrounded;
        if (_playerIsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Get (New) input System's Control player actions:
        _inputControlAction = _playerMoveInputAction.ReadValue<Vector2>();
        _playerMoveVector3.Set(_inputControlAction.x, 0, _inputControlAction.y);
        _playerCharacterController.Move(_playerMoveVector3 * (Time.deltaTime * _playerSpeed));

        // Changes the height position of the player..
        if (_playerJumpInputAction.triggered && _playerIsGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_playerJumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _playerCharacterController.Move(_playerVelocity * Time.deltaTime);
    }
}