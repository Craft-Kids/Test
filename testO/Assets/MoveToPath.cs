using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPath : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .75f);
    }
}
