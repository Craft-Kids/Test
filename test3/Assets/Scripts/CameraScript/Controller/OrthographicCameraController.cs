using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicCameraController : ICameraController
{
    private Transform transform;
    private Camera cam;

    public OrthographicCameraController (Transform transform, Camera cam, float startingZoom)
    {
        this.cam = cam;
        this.cam.orthographicSize = startingZoom;
        this.transform = transform;
    }
    public void Move(Vector3 direction)
    {
        transform.position += transform.TransformDirection(direction) * Time.deltaTime;
    }

    public void Rotate(Vector3 axis, float amount, float rotateSpeed)
    {
        transform.Rotate(axis, amount * Time.deltaTime * rotateSpeed);
    }

    public void ZoomIn(float amount, float maxLimit)
    {
        if (cam.orthographicSize <= maxLimit) return;

        cam.orthographicSize = Mathf.Max(cam.orthographicSize - amount, maxLimit);
    }

    public void ZoomOut(float amount, float minLimit)
    {
        if (cam.orthographicSize >= minLimit) return;

        cam.orthographicSize = Mathf.Min(cam.orthographicSize + amount, minLimit);
    }

    public IEnumerator Shake(float amount, float duration, Action shakingToFalse)
    {
        Vector3 origin_Point = transform.position;
        float delta = 0.0f;

        while(delta <= duration)
        {
            transform.position = UnityEngine.Random.insideUnitSphere * amount;
            delta += Time.deltaTime;
            yield return null;
        }

        transform.position = origin_Point;
        shakingToFalse();
    }

    public void Tracking(Transform target, Vector3 offset, float smooth)
    {
        Vector3 destinationPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, destinationPosition, smooth);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

    public void RotateAroundTarget(Transform target, float angle, float rotateSpeed)
    {
        Vector3 originPos = transform.position;
        Vector3 destinationPosition;
        transform.RotateAround(target.position + Vector3.up * transform.position.y, Vector3.up, angle * rotateSpeed);
        destinationPosition = transform.position;

        transform.position = originPos;
        transform.position = Vector3.Slerp(originPos, destinationPosition, 1.0f);
        transform.LookAt(target);
    }
}
