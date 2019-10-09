using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PathCreation;

namespace PathCreation.Examples
{
    public class State : MonoBehaviour
    {
        public GameObject Player;

        int Hp; //체력
        int Stemina; //활력
        int minHp; //한계체력(최소체력?)

        float speed; //속도
        int Acceleration; //가속도


        bool isAccel; //가속,감속 버튼이 눌린상태인가
        bool isDecel;

        // Update is called once per frame
        void Update()
        {
            isAccel = GetComponent<UI_Btn_Manager>().AccelBtnDown;
            isDecel = GetComponent<UI_Btn_Manager>().DecelBtnDown;

            if (isAccel)
            {
                Player.GetComponent<speedControl>().speed += 0.01f;
                Debug.Log("속도증가중");
            }

            if (isDecel)
            {
                Player.GetComponent< speedControl> ().speed -= 0.01f;
                Debug.Log("속도감속중");
            }
        }
    }
}
