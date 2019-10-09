using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    public class speedControl : MonoBehaviour //스피드 관련은 다 여기서 건들자
    {
        public float speed = 0.9f;

        private void Awake()
        {
            speed = PM_System.instance.Speed;
            Debug.Log(speed);

        }


        // Update is called once per frame
        void Update()
        {
            
        }
    }
}