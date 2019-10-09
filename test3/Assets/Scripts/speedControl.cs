using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    public class speedControl : MonoBehaviour //스피드 관련은 다 여기서 건들자
    {
        public float speed;

        private void Awake()
        {
            speed = 0.9f;
            Debug.Log(speed);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}