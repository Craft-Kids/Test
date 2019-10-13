using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : InputHandler
{
    // Event Handlers
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    public static event EventInputHandler OnEventInput;

    // Update is called once per frame
    void Update()
    {
        #region Input Invoke
        // Move
        //if(Input.GetKey(KeyCode.W))
        //{
        //    OnMoveInput?.Invoke(Vector3.forward);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    OnMoveInput?.Invoke(Vector3.back);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    OnMoveInput?.Invoke(Vector3.left);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    OnMoveInput?.Invoke(Vector3.right);
        //}

        // Rotate
        //if (Input.GetKey(KeyCode.E))
        //{
        //    OnRotateInput?.Invoke(-1.0f);
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    OnRotateInput?.Invoke(1.0f);
        //}

        //// Zoom
        //if (Input.GetKey(KeyCode.Z))
        //{
        //    OnZoomInput?.Invoke(-1.0f);
        //}
        //if (Input.GetKey(KeyCode.X))
        //{
        //    OnZoomInput?.Invoke(1.0f);
        //}

        //if (Input.GetKey(KeyCode.V))
        //{
        //    OnEventInput?.Invoke();
        //}
        #endregion
    }
}
