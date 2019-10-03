using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    int Hp; //체력
    int Stemina; //활력
    int minHp; //한계체력(최소체력?)

    int speed;//속도
    int Acceleration; //가속도


    bool isAccel;//가속,감속 버튼이 눌린상태인가
    bool isDecel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isAccel = GetComponent<UI_Btn_Manager>().AccelBtnDown;
        isDecel = GetComponent<UI_Btn_Manager>().DecelBtnDown;

        if (isAccel)
        {
            Debug.Log("속도증가중");
        }

        if(isDecel)
        {
            Debug.Log("속도감속중");
        }
    }
}
