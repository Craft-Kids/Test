using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace PathCreation.Examples
{
    public class UI_Btn_Manager : MonoBehaviour
    {
        public Button AcelBtn;
        public GameObject DecelBtn;
        public GameObject Option;
        TestFunction GameManager;//TestFunction의 함수들
        bool isAccelBtnDown = false;//버튼의상태
        bool isDecelBtnDown = false;

        public void Awake()
        {
            GameManager = GameObject.Find("UImanager").GetComponent<TestFunction>();
            //AcelBtn = GetComponent<Button>();
            
        }
        public void Update()
        {
            Touch();
            if (isAccelBtnDown)
            {
                GameManager.Dancing();//가속가능여부 체크 및 실행
                if (PM_System.instance.Mp <= 0) //활력이 0이되면
                {
                    //Debug.Log("가속버튼 false");
                    isAccelBtnDown = false;
                    GameManager.isMpHealing = true;
                }
            }
            if (isDecelBtnDown)
            {
                GameManager.SpeedDown();//감속가능여부 체크 및 실행
            }

        }
        public void Combo_Btn()
        {
            Debug.Log("콤보");
        }
        public void PointerDown(GameObject btn)//버튼이 눌린상태일때
        {
            if (btn.name == "Acceleration_Btn")
            {
                isAccelBtnDown = true;
            }
            else if (btn.name == "Deceleration_Btn")
            {
                isDecelBtnDown = true;
            }
        }
        public void PointerUp(GameObject btn)
        {
            if (btn.name == "Acceleration_Btn")
            {
                isAccelBtnDown = false;
                GameManager.isMpHealing = true; //가속버튼에서 손을 떼면 활력회복상태로 전환
            }
            else if (btn.name == "Deceleration_Btn")
            {
                isDecelBtnDown = false;
            }
        }

        [System.Obsolete]
        public void OptionOpen_Btn()
        {
            Option.SetActive(true);
        }
        [System.Obsolete]
        public void OptionClose_Btn()
        {
            Option.SetActive(false);
        }

        public void GameEnd_Btn()
        {
            Application.Quit();
        }

        public void GamePlay_Btn()//플레이셋팅
        {
            SceneManager.LoadScene("PlaySetting");
        }

        public void InGame_Btn()//게임시작
        {
            SceneManager.LoadScene("UI_Scene");
        }

        void Touch()//터치감지
        {
            if (Input.touchCount > 0) //터치가 1개 이상이면.
            {
                //if (EventSystem.current.IsPointerOverGameObject() == false)//UI제외한 터치
                //{
                    if (Input.GetTouch(0).phase == TouchPhase.Began)//눌리는 순간
                    {
                        Vector3 pos = Input.GetTouch(0).position;
                        if (pos.x <= Screen.width / 2)//왼쪽터치
                        {
                            Debug.Log("왼쪽으로 라인전환");
                        }

                        if (pos.x > Screen.width / 2)//오른쪽터치
                        {
                            Debug.Log("오른쪽으로 라인전환");
                        }
                    }
                //}
            }
        }
    }
}