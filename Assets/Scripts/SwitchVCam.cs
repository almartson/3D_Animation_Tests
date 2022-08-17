using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/// <summary>
/// This Script MUST be attached to an Aiming (Cinemachine) Virtual Camera (Prefab or GameObject). 
/// </summary>
public class SwitchVCam : MonoBehaviour
{

    [SerializeField] private PlayerInput _playerInput;

    private CinemachineVirtualCamera _virtualCamera;
    
    private InputAction _aimPlayerInputAction;
    
    [SerializeField]
    private int _cameraPriorityBoostAmount = 10;
    
    // UI - GUI

    [SerializeField] private Canvas _thirdPersonPlayerUICanvas;

    [SerializeField] private Canvas _zoomAimPlayerUICanvas;
    

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _aimPlayerInputAction = _playerInput.actions["Aim"];
    }


    private void OnEnable()
    {
        _aimPlayerInputAction.performed += _ => StartAim();
        _aimPlayerInputAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        _aimPlayerInputAction.performed -= _ => StartAim();
        _aimPlayerInputAction.canceled -= _ => CancelAim();
    }
    
    
    #region AlMartson's Custom Methods

    private void StartAim()
    {
        // Rise (Boost) the Camera's priority by a high number, (like 10 for example), for it to be shown WITH PRIORITY, even if there is another out there (like the Moving 3rd Person Cinemachine Virtual Camera):
        //
        _virtualCamera.Priority += _cameraPriorityBoostAmount;

        // GUI - UI Canvas
        //
        _zoomAimPlayerUICanvas.enabled = true;
        //
        _thirdPersonPlayerUICanvas.enabled = false;
    }
    
    private void CancelAim()
    {
        // Lower the Camera's priority by a high number, (like 10 for example), for it NOT to be shown WITH PRIORITY if there is another out there (like the Moving 3rd Person Cinemachine Virtual Camera). Trying to get it to a standard original base number (like 1, for example):
        //
        _virtualCamera.Priority -= _cameraPriorityBoostAmount;
        
        // GUI - UI Canvas
        //
        _zoomAimPlayerUICanvas.enabled = false;
        //
        _thirdPersonPlayerUICanvas.enabled = true;
    }

    
    #endregion AlMartson's Custom Methods
}
