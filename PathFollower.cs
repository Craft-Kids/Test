using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public List<PathCreator[]> list = new List<PathCreator[]>();

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 0.9f;   // 일단 인라인으로 달리게 설정

        public int track = 0;
        public int select = 0;
        float distanceTravelled;


        //path의 자식 find
        //list.sorting
        void Awake()
        {
            Transform player = GameObject.Find("Path").transform;

            for (int i = 0; i < player.childCount; i++)
            {
                list.Add(player.GetChild(i).GetComponentsInChildren<PathCreator>());
                
                Debug.Log(player.GetChild(i).GetComponentsInChildren<PathCreator>().Length);

            }
        }

        void Start()
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        void Update()
        {
            if (track > 9)
                track = 0;

            pathCreator = (list[track])[select];

            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }

            // 라인변경(일자도로)
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                select = 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                select = 0;
            }
        }

        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "RightEnter")  // 오른쪽으로 코너 진입
            {
                Debug.Log("Right");
                distanceTravelled = 0;
                SpeedCheck();
                track++;
            }

            if (other.gameObject.tag == "LeftEnter")  // 왼쪽으로 코너 진입
            {
                Debug.Log("Left");
                distanceTravelled = 0;
                SpeedCheck();
                track++;
                Array.Reverse(list[track]);  //왼쪽일때 배열 뒤집기 --> select이동은 어떻게?
            }

            if (other.gameObject.tag == "Exit")   // 코너를 통과하면
            {
                Debug.Log("Exit");
                distanceTravelled = 0;
                track++;

                Debug.Log(track);
                // 코너탈출시 속도에 따라 라인 변경?
                // 가까운걸로 라인이 변경된다면 코너진입속도에 따라 라인변경?


                if (select > 2)   // 트랙이 0, 1, 2, 3, 4 중 2, 3, 4 인 상태이면
                    select = 0;   // 라인 0 (첫번째 라인)로 달리게 설정
                else
                    select = 1;
            }
        }

        void SpeedCheck()  // 속도에 따라 라인 변경(오른쪽 코너 기준)
        {
            if (speed < 1.0f)    // 속도가 1.0보다 느리면
                select = 4;    // 오른쪽(인라인) Coner x-5
            else if (speed < 1.5f)
                select = 3;    // Path 4
            else if (speed < 2.0f)
                select = 2;    // Path 3
            else if (speed < 2.5f)
                select = 1;    // Path 2
            else if (speed < 3.0f)
                select = 0;    // Path 1
        }
    }
}