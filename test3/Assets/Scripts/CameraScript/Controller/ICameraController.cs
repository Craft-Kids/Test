using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraController
{
    void Move(Vector3 direction);

    void Rotate(Vector3 axis, float amount, float rotateSpeed);

    void ZoomIn(float amount, float minLimit);

    void ZoomOut(float amount, float maxLimit);

    IEnumerator Shake(float amount, float duration, Action shakingToFalse);

    void Tracking(Transform target, Vector3 offset, float smooth);

    void RotateAroundTarget(Transform target, float angle, float rotateSpeed);
}