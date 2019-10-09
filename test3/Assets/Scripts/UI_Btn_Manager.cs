using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_Btn_Manager : MonoBehaviour
{
    public bool AccelBtnDown = false;
    public bool DecelBtnDown = false;

    public void Combo_Btn()
    {
        Debug.Log("콤보");
    }
    public void PointerDown()//버튼이 눌린상태일때
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Acceleration_Btn")
        {
            AccelBtnDown = true;
        }
        else
        {
            DecelBtnDown = true;
        }
      
    }
    public void PointerUp()//버튼이 안눌린 상태일때
    {
        if (AccelBtnDown)
        {
            AccelBtnDown = false;
        }
        else if(DecelBtnDown)
        {
            DecelBtnDown = false;
        }
    }
}
