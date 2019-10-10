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
        float speed;

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

            
            //speed = PM_System.instance.Speed;
            //speed = 0.3f;
        }

        void Start()
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        void Update()
        {
            speed = PM_System.instance.Speed;
            if (track >= 10)  //10번째부터는 처음부터
                track = 0;

            pathCreator = list[track][select];


            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
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
                if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽 라인으로 이동
                {
                    //list[track][0].transform.position =
                    //    Vector3.Slerp(list[track][0].transform.position, list[track][1].transform.position, 0.1f);
                    // https://hyunity3d.tistory.com/429 보간을 한번 사용해보면...?


                    // Rotate 해봤는데 베지어 라인 따라 갈때 회전도 고정이여서 순간적으로 한번 돌고 계속 앞에봄
                    // 위에있는 transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction)을 건들여야되겠다
                    //// 
                    //transform.rotation =
                    //    pathcreator.path.getrotationatdistance(distancetravelled,
                    //    (list[track])[1].getcomponent<endofpathinstruction>().endofpathinstruction);

                    if (reversecheck == false)
                        select = 1;
                    else
                        select = 0;  //리벌스 한 상태이면 반대

                    //플레이어가 콜라이더에 충돌한 순간, 현재 path의 마지막 빨간 원의 위치와 다음 path의 첫번째 빨간 원의 위치를 받아와서 잇는 방법
                    //rotate적용
                    //이동할때 시간 길게 주기 (= 플레이어 속도 느리게 하기?)
                    //근데 이어주고 로테이트 적용하면 결국 배지어 아님?

                    //방법1 -> 그렇다고 라인 이을 순 없으니깐 그냥 저렇게 한다.

                }

                if (Input.GetKeyDown(KeyCode.LeftArrow)) // 왼쪽 라인으로 이동
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
                Debug.Log("Right");
                distanceTravelled = 0;
                SpeedCheck(); // 코너 주행중에도 속도 감속 가속 가능, 라인이 바뀔 수 있음
                track++;
                conercheck = true;
                reversecheck = false;

            }

            if (other.gameObject.tag == "LeftEnter")  // 왼쪽으로 코너 진입
            {
                Debug.Log("Left");
                distanceTravelled = 0;
                SpeedCheck();
                track++;
                //Array.Reverse(list[track]);  //왼쪽일때 배열 뒤집기 --> select이동은 어떻게?
                //--> track안에 요소(select)가 뒤집어진거 아녀?

                //두번째 네번째 바퀴때(짝수바퀴때) reverse 안되는 이유는?
                //reverse된게 다음바퀴 때도 그대로라서
                //방법1 -> LeftEnterExit 충돌체크해서 리벌스 되돌린다  Array.Reverse(list[track-1]);
                //방법2 -> 업데이트 젤 첨에 초기화
                //방법3 -> reverse 쓰지말고 라인 직접 바꾸기

                //(일단 방법3 해놓음)

                conercheck = true;
                reversecheck = false;
            }

            if (other.gameObject.tag == "RightExit")   // 코너를 통과하면 가까운 라인으로 이동
            {
                Debug.Log("RightExit");
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
                Debug.Log("LeftExit");
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

        private void OnTriggerStay(Collider other)  //플레이어 반경에 다른 플레이어가 들어오면 슬립스트림 효과 발생
        {
            //방법1 삼각형 콜라이더 만들기. 마야로 삼각형 만들어서 메쉬콜라이더 맹들기
            //방법2 코드로 삼각형 모양 반경 설정

            // -> box 콜라이더로 해둠
            if (other.gameObject.tag == "OtherPlayer")
            {
                Debug.Log("슬립스트림");

                PM_System.instance.Hp += PM_System.instance.HpRecovery;  //현재체력이 지속회복량에 따라 지속회복
                //회복할 수 있는 양 한계(최대체력) 설정하기

                if (pathCreator == other.GetComponent<PathFollower>().pathCreator) //경로차단. 뒤로 플레이어 1.5명 정도의 범위
                {
                    //슬립스트림 위치이며 같은 라인일때 속도 똑같아지도록 설정하려고함
                    //다른 플레이어의 pathCreator을 어떻게 불러오면 좋을까??
                    //지금 해놓은 방법은 null로 뜸
                    Debug.Log("같은 도로");

                    //speed = other.speed;
                    //다른 플레이어 속도도 못가져오는데 왜지?
                }
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