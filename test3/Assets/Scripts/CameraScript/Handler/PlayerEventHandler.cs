using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : EventHandler
{
    public static event TrackingHandler OnTracking;
    public static event RotatingHandler OnRotating;

    public Transform player;
    public Vector3 lastPos;
    public float lastRotatingAngle;

    private void Update()
    {
        Vector3 destination = player.position - lastPos;
        float rotatingAngle = player.rotation.eulerAngles.y - lastRotatingAngle;

        if (destination.magnitude != 0)
        {
            OnTracking?.Invoke(destination);
        }

        if (rotatingAngle != 0)
        {
            OnRotating?.Invoke(player.rotation.eulerAngles.y);
        }

        lastPos = player.position;
        lastRotatingAngle = player.rotation.eulerAngles.y;
    }

}
