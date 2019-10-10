using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class OtherPathFollower : MonoBehaviour
    {
        public List<PathCreator[]> list = new List<PathCreator[]>();

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;

        public int track = 0;
        public int select = 0;
        float distanceTravelled;

        bool conercheck = false;
        bool reversecheck = false;

        //list.sorting
        void Awake()
        {
            Transform player = GameObject.Find("Path").transform;

            for (int i = 0; i < player.childCount; i++)
            {
                list.Add(player.GetChild(i).GetComponentsInChildren<PathCreator>());
            }
        }

        void Start()
        {
            speed = 1.1f;
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        void Update()
        {
            if (track >= 10)  //10번째부터는 처음부터
                track = 0;

            pathCreator = list[track][select];


            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + 0.1f, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }


            if (conercheck == true)  // 코너일때, 콤보시스템 동작중이지 않을 때
            {
                SpeedCheck();
            }


            // 라인변경(일자도로)
            // 어떻게 하면 부드러운 회전을 할 수 있을까?
            if (conercheck == false)  // 직선일 때만 라인변경가능
            {
                if (Input.GetKeyDown(KeyCode.D)) // 오른쪽 라인으로 이동
                {
                    if (reversecheck == false)
                        select = 1;
                    else
                        select = 0;  //리벌스 한 상태이면 반대
                }

                if (Input.GetKeyDown(KeyCode.A)) // 왼쪽 라인으로 이동
                {
                    if (reversecheck == false)
                        select = 0;
                    else
                        select = 1;  //리벌스 한 상태이면 반대
                }
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
                //Debug.Log("Right");
                distanceTravelled = 0;
                SpeedCheck(); // 코너 주행중에도 속도 감속 가속 가능, 라인이 바뀔 수 있음
                track++;
                conercheck = true;
                reversecheck = false;

            }

            if (other.gameObject.tag == "LeftEnter")  // 왼쪽으로 코너 진입
            {
                //Debug.Log("Left");
                distanceTravelled = 0;
                SpeedCheck();
                track++;

                conercheck = true;
                reversecheck = false;
            }

            if (other.gameObject.tag == "RightExit")   // 코너를 통과하면 가까운 라인으로 이동
            {
                //Debug.Log("RightExit");
                distanceTravelled = 0;
                track++;

                //하이라키 창에 직선도로 순서 바꿈
                if (select < 2)   // 트랙 0, 1, 2, 3, 4 중
                {
                    select = 0;   // 인라인
                }
                else if (select == 2)
                {
                    //가속 중이라면 아웃라인
                    //감속 중이라면 인라인
                    select = 1;  // 아웃라인
                }
                else if (select > 2)
                {
                    select = 1;  // 아웃라인
                }

                conercheck = false;
                reversecheck = false;

            }

            if (other.gameObject.tag == "LeftExit")
            {
                //Debug.Log("LeftExit");
                distanceTravelled = 0;
                track++;

                //하이라키 창에 직선도로 순서 바꿈
                if (select < 2)   // 트랙 0, 1, 2, 3, 4 중
                {
                    select = 0;   // 인라인
                }
                else if (select == 2)
                {
                    //가속 중이라면 아웃라인
                    //감속 중이라면 인라인
                    select = 1;  // 아웃라인
                }
                else if (select > 2)
                {
                    select = 1;  // 아웃라인
                }

                conercheck = false;
                reversecheck = true;
            }
        }

        void SpeedCheck()  // 속도에 따라 라인 변경(오른쪽 코너 기준)
        {
            if (speed < 1.0f)    // 속도가 1.0보다 느리면
                select = 4;    // 오른쪽(인라인)
            else if (speed < 1.5f)
                select = 3;
            else if (speed < 2.0f)
                select = 2;
            else if (speed < 2.5f)
                select = 1;
            else if (speed < 3.0f)
                select = 0;
        }

    }
}