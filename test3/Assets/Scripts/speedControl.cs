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

        }

        public void OnSpeedUp()  //가속 중
        {
            speed += 0.01f;
        }

        public void OnSpeedDown()  //감속 중
        {
            speed -= 0.01f;
        }

        //최소속도로 감속
        //public void SpeedDown
    }
}