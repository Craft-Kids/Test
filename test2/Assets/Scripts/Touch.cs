using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using PathCreation;

namespace PathCreation.Examples
{
    public class Touch : MonoBehaviour
    {
        public GameObject Player;
        void Update()
        {
            if (Input.touchCount > 0)//터치가 1개 이상이면.
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)//눌리는 순간
                {
                    Vector3 pos = Input.GetTouch(0).position;
                    if (pos.x <= Screen.width / 2)//왼쪽터치
                    {
                        Debug.Log("왼쪽으로 라인전환");
                        Player.GetComponent<PathFollower>().select = 0;
                    }

                    if (pos.x > Screen.width / 2)//오른쪽터치
                    {
                        Debug.Log("오른쪽으로 라인전환");
                        Player.GetComponent<PathFollower>().select = 1;
                    }
                }
            }
        }
    }

}