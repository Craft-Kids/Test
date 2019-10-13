using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void TrackingHandler(Vector3 destination);
    public delegate void RotatingHandler(float angleDegree);
}
