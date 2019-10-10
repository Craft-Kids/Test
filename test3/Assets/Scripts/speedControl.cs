using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    public class speedControl : MonoBehaviour //스피드 관련은 다 여기서 건들자
    {
        float speed;

        private void Awake()
        {
            speed = PM_System.instance.Speed;
            Debug.Log(speed);
        }


        // Update is called once per frame
        void Update()
        {
            PM_System.instance.Speed = speed;
            //Debug.Log(PM_System.instance.Speed); // <3> 여기는 증가하는데,,. -> PathFollwer의 49번째 줄로 가보시오
        }

        public void OnSpeedUp()  //가속 중
        {
            speed += 0.01f;
            //Debug.Log(PM_System.instance.Speed); // <1> OtherPlayer와 함께 달릴때는 speed가 증가하지 않고 계속 1이다.....
            //Debug.Log(speed); // <2> 이 스크립트 안의 speed는 잘 증가한다. 그럼 문제는 싱글톤에 저장할때인가?
        }

        public void OnSpeedDown()  //감속 중
        {
            speed -= 0.01f;
        }

        //최소속도로 감속
        //public void SpeedDown
    }
}