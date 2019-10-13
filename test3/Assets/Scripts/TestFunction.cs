using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace PathCreation.Examples
{
    public class TestFunction : MonoBehaviour
    {
        float MinSpeed = 1f;//최소속도이자 기본주행속도
        float MediSpeed = 1.4f;//속도70
        public bool isHpHealing = false;//체력회복중인가
        public bool isMpHealing = true;//활력회복중인가

        public GameObject player;

        void Update()
        {
;            BasicDrive();//기본주행체크(상시)

            if(isHpHealing)//체력이 회복중 상태면
            {
                HpCure(0);//체력회복
            }
            if(isMpHealing)//활력이 회복중 상태면
            {
                //Debug.Log("활력회복상태");
                MpCure();//활력회복
            }
        }
        public void SetValue(InputField ipf)//플레이셋팅에서 입력받은값을 저장
        {
            switch (int.Parse(ipf.tag))
            {
                case 0:
                    PM_System.instance.Acceleration = float.Parse(ipf.text); //0가속도
                    break;
                case 1:
                    PM_System.instance.Deceleration = float.Parse(ipf.text); //1감속도
                    break;
                case 2:
                    PM_System.instance.Maxspeed = float.Parse(ipf.text); //2최고속도
                    break;
                case 3:
                    PM_System.instance.MpDecrease = float.Parse(ipf.text); //3활력감소량
                    break;
                case 4:
                    PM_System.instance.MpRecovery = float.Parse(ipf.text); //4활력회복량
                    break;
                case 5:
                    PM_System.instance.Hp_mValue = float.Parse(ipf.text); //5체력감소량
                    break;
                case 6:
                    PM_System.instance.Hp_pValue = float.Parse(ipf.text); //6체력회복량
                    break;
                case 7:
                    PM_Player.instance.MaxHp = float.Parse(ipf.text); //7최대체력
                    break;
                case 8:
                    PM_System.instance.CornerSpeed = float.Parse(ipf.text); //8코너링속도
                    break;
            }
        }

        public void PrintValue()//입력값 출력
        {
            Debug.Log(PM_System.instance.Acceleration);
            Debug.Log(PM_System.instance.Deceleration);
            Debug.Log(PM_System.instance.Maxspeed);
            Debug.Log(PM_System.instance.MpDecrease);
            Debug.Log(PM_System.instance.MpRecovery);
            Debug.Log(PM_System.instance.Hp_mValue);
            Debug.Log(PM_System.instance.Hp_pValue);
            Debug.Log(PM_Player.instance.MaxHp);
            Debug.Log(PM_System.instance.CornerSpeed);
        }


        public void HpDamage(int num, bool add)//체력감소(0:지속 or 1:일회성, 추가소모여부)
        {
            //속도에따라 체력소비량이 바뀐다.(지금은 적용X)
            if (PM_System.instance.Hp > 0)
            {
                if (num == 0)
                {
                    if (add)//1.5배소모됨
                    {
                        PM_System.instance.Hp -= PM_System.instance.HpDecrease * 1.5f;
                    }
                    else
                    {
                        PM_System.instance.Hp -= PM_System.instance.HpDecrease;
                        //Debug.Log("체력댐지");

                    }
                }
                else
                {
                    PM_System.instance.Hp -= PM_System.instance.Hp_mValue;
                }

                if(PM_System.instance.Mp >= PM_System.instance.Hp)//활력이 체력보다 크거나 같으면
                {
                    PM_System.instance.Mp = PM_System.instance.Hp;
                    isMpHealing = false;
                }
            }
        }
        public void HpCure(int num)//체력회복(0:지속, 1:일회성, 2:슬립스트림(최대체력까지회복))
        {
            if (num == 2)
            {
                if(PM_System.instance.Hp < PM_Player.instance.MaxHp)
                    PM_System.instance.Hp += PM_System.instance.HpRecovery;
                Debug.Log("슬립체력회복");
            }
            else if (PM_System.instance.Hp < PM_System.instance.HpLimit)//한계체력이상을 회복할수없다.
            {
                if (num == 0)
                {
                    PM_System.instance.Hp += PM_System.instance.HpRecovery;
                }
                else if(num == 1)
                {
                    PM_System.instance.Hp += PM_System.instance.Hp_pValue;
                }
            }
            //슬립스트림중이라면..피가안닳나 ? ..
            if (PM_System.instance.Mp < PM_System.instance.Hp)//활력이 체력보다 작으면
            {
                isMpHealing = true;
            }


        }
        public void MpDamage()//활력지속감소
        {
            Debug.Log("활력닳는중");
            isMpHealing = false;//활력회복중단
            if (PM_System.instance.Mp > 0)
                PM_System.instance.Mp -= PM_System.instance.MpDecrease;

        }
        public void MpCure()//활력지속회복
        {
            if (PM_System.instance.Mp < PM_System.instance.Hp && PM_System.instance.Mp < 60)//현재체력보다 낮아야하고, 최대60까지 회복가능
                PM_System.instance.Mp += PM_System.instance.MpRecovery;
        }
        public void Dancing()//댄싱버튼 눌렸을때
        {
            if (PM_System.instance.Mp > 0)//활력이 남았나?
            {
                if (player.GetComponent<PathFollower>().discheck == false)//앞사람과 거리가 충분한가? (경로차단에서는 이 함수가 호출안되게 수정-수진)
                {
                    //Debug.Log("가속중,,");
                    player.GetComponent<speedControl>().OnSpeedUp();  //가속함수 호출
                    MpDamage(); //활력감소함수호출
                }
                
            }
            //else if (PM_System.instance.Mp == 0) //활력이 0이되면
            //{
            //    isMpHealing = true;
            //}
        }
        public void SpeedDown()//감속버튼 눌렸을때
        {
            if (PM_System.instance.Speed > MediSpeed)// 속도가 70초과면(1 X 50 = 기본속도, 70 => 1.4)
            {
                player.GetComponent <speedControl>().OnSpeedDown(); //감속함수 호출
                HpDamage(0, true); //체력지속감소량 1.5배 소모.
            }
            else if (PM_System.instance.Speed <= MinSpeed)// 속도가 50이하면
            {
                //아무동작도 실행하지 않음
            }
            else
            {
                player.GetComponent<speedControl>().OnSpeedDown(); //감속함수 호출
                //Debug.Log("속도감속2");

            }
        }

        public void BasicDrive()//기본주행체크(항시)
        {
            if (PM_System.instance.Speed <= MinSpeed)//최소속도인가? ( 최소속도 = 기본주행속도)
            {
                isHpHealing = true; //지속체력회복상태로 변경
            }
            else if (PM_System.instance.Hp > 0)//체력이 있나?
            {
                isHpHealing = false;
                HpDamage(0, false);//지속체력감소
            }
            else //체력이 없음
            {
                if (PM_System.instance.Speed > 1)
                    player.GetComponent<speedControl>().OnSpeedDown(); // 최소속도로 감속
                Debug.Log("속도감속1");
            }


            //if 슬립스트림 범위내인가?
            //if 경로차단중인가?
        }

        public void ChangeLine(int LineCount)//라인변경체크(int 라인갯수)
        {
            if (LineCount >= 2)//라인이 2개이상일때
            {
                if (true)//이동할 라인에 사람이 없는지확인
                {
                    //라인변경함수 호출
                    HpDamage(1, false);//체력1회 소모
                }
            }
        }

        public void Cornering()//코너주행시, 인아웃라인변경(진입과 탈출은 별도로 존재함)
        {
            //적정속도일때(범위는 임시로설정)
            if (PM_System.instance.Speed >= 1.2 && PM_System.instance.Speed < 1.4)
            {
                //if() 현재 최종 아웃라인 인가?
                //코스이탈
                //else 아웃라인으로 이동
            }
            //적정속도이상
            else if (PM_System.instance.Speed >= 1.4)
            {
                //if() 현재 최종 아웃라인 인가?
                //코스이탈
                //else 아웃라인으로 이동
            }
            //else if() 현재 최종 인라인 인가?
            //
            //else 인라인으로 이동
        }
    }
}