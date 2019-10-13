using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Starting Data (position, speed, bound, zoom)
    #region
    [Header("Camera Positioning")]
    public Vector2 startingCamOffset = Vector2.zero;
    public Vector3 offsetFromTarget = Vector3.zero;
    public float lookAtOffest = 2.0f;
    public bool firstPersonAspect = false;

    [Header("Move Controls")]
    public float inOutSpeed = 5.0f;
    public float lateralSpeed = 5.0f;

    [Header("Move Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("Zoom Controls")]
    public float zoomSpeed = 4.0f;
    public float nearZoomLimit = 2.0f;
    public float farZoomLimit = 16.0f;
    public float startingZoom = 5.0f;

    [Header("Shaking Control")]
    public float shakeAmount = 1.0f;
    public float shakeDuration = 1.0f;
    private bool isShaking = false;

    [Header("Event Control")]
    [Range(0.0f, 1.0f)]
    public float smoothFactor = 0.125f;
    public float rotateSpeed = 50.0f;

    #endregion

    // private
    Camera cam;
    ICameraController cameraController;
    Vector3 frameMove;
    float frameRotate;
    float frameZoom;
    Vector3 frameDestination;
    float frameRotatingAngle;

    // public
    public Transform target;

    // Awake / Enable / Disable
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0.0f, Mathf.Abs(startingCamOffset.y), -Mathf.Abs(startingCamOffset.x));
        cameraController = new OrthographicCameraController(transform, cam, startingZoom);
    }
    

    private void OnEnable()
    {
        KeyboardInputHandler.OnMoveInput += UpdateFrameMove;
        KeyboardInputHandler.OnRotateInput += UpdateFrameRotate;
        KeyboardInputHandler.OnZoomInput += UpdateZoom;
        KeyboardInputHandler.OnEventInput += TriggerShake;

        PlayerEventHandler.OnTracking += Tracking;
        PlayerEventHandler.OnRotating += RotateAroundTarget;
    }

    private void OnDisable()
    {
        KeyboardInputHandler.OnMoveInput -= UpdateFrameMove;
        KeyboardInputHandler.OnRotateInput -= UpdateFrameRotate;
        KeyboardInputHandler.OnZoomInput -= UpdateZoom;

        PlayerEventHandler.OnTracking -= Tracking;
        PlayerEventHandler.OnRotating -= RotateAroundTarget;
    }

    #region Input Control
    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }
    
    private void UpdateFrameRotate(float rotateAmount)
    {
        frameRotate += rotateAmount;
    }

    private void UpdateZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    private void TriggerShake()
    {
        if (isShaking)
            return;
      
        isShaking = true;
        
        // when coroutine finish, isShaking makes false state
        StartCoroutine((cameraController.Shake(shakeAmount, shakeDuration, () => { isShaking = false; })));
    }
    #endregion

    #region Player Event Control
    private void Tracking(Vector3 destination)
    {
        frameDestination = destination;
    }

    private void RotateAroundTarget(float rotatingAngle)
    {
        frameRotatingAngle = rotatingAngle;
    }

    #endregion

    private void LockPositionIBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y));
    }

    private void PreventChangeAspect()
    {
        offsetFromTarget = Vector3.zero;
        smoothFactor = 1.0f;
    }

    private void LateUpdate()
    {
        // aspect
        if (firstPersonAspect)
        {
            PreventChangeAspect();
        }

        // move
        if (frameMove != Vector3.zero)
        {
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            cameraController.Move(speedModFrameMove);
            LockPositionIBounds();
            frameMove = Vector3.zero;
        }

        // rotate
        if (frameRotate != 0.0f)
        {
            cameraController.Rotate(Vector3.up, frameRotate * Time.deltaTime, rotateSpeed);
            frameRotate = 0.0f;
        }

        // zoom
        if (frameZoom < 0.0f)
        {
            cameraController.ZoomIn(Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0.0f;
        }
        else if (frameZoom > 0.0f)
        {
            cameraController.ZoomOut(Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, farZoomLimit);
            frameZoom = 0.0f;
        }

        // tracking
        if (frameDestination.magnitude != 0)
        {
            cameraController.Tracking(target, offsetFromTarget, smoothFactor);
            frameDestination = Vector3.zero;
        }

        // rotating

        if (frameRotatingAngle != 0)
        {
            cameraController.RotateAroundTarget(target, frameRotatingAngle, rotateSpeed);
            frameRotatingAngle = 0;
        }

        // hovering
    }
}
