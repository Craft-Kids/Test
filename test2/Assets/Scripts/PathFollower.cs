using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator[] track1;
        public PathCreator[] coner1;
        public PathCreator[] track2;
        public PathCreator[] coner2;
        public PathCreator[] track3;
        public PathCreator[] coner3;
        public PathCreator[] track4;
        public PathCreator[] coner4;
        public PathCreator[] track5;
        public PathCreator[] coner5;

        PathCreator[][] path = new PathCreator[10][];    

        //PathCreator[] coner = { };
        //PathCreator[,] path = new PathCreator[2, 4] {
        //    { track1 , track2, track3, track4 },
        // { coner1, coner2, coner3, coner4 }};

      
        // PathCreator[,] path;    

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 0.9f;   // 일단 인라인으로 달리게 설정

        public int track = 0;
        public int select = 0;
        float distanceTravelled;
        
        void Start()
        {
            //path[0] = new PathCreator[] { track1[0] };
            //pathCreator = track1[select];
            //coner = (PathCreator[])coner1.Clone();
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        void Update()
        {
            //Debug.Log(track);
            if (track > 9)
                track = 0;
            switch (track)
            {
                case 0:
                    {
                        pathCreator = track1[select];
                        break;
                    }
                case 1:
                    {
                        pathCreator = coner1[select];
                        break;
                    }
                case 2:
                    {
                        pathCreator = track2[select];
                        break;
                    }
                case 3:
                    {
                        pathCreator = coner2[select];
                        break;
                    }
                case 4:
                    {
                        pathCreator = track3[select];
                        break;
                    }
                case 5:  
                    {
                        pathCreator = coner3[select];
                        break;
                    }
                case 6:
                    {
                        pathCreator = track4[select];
                        break;
                    }
                case 7:
                    {
                        pathCreator = coner4[select];
                        break;
                    }
                case 8:
                    {
                        pathCreator = track5[select];
                        break;
                    }
                case 9:
                    {
                        pathCreator = coner5[select];
                        break;
                    }
                default:
                    {
                        pathCreator = coner5[select];
                        break;
                    }
            }

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
            }

            if (other.gameObject.tag == "Exit")   // 코너를 통과하면
            {
                Debug.Log("Exit");
                distanceTravelled = 0;
                track++;

                if (select > 2)   // 트랙이 0, 1, 2, 3, 4 중 2, 3, 4 인 상태이면
                    select = 1;   // 라인 1 (두번째 라인)로 달리게 설정
                else
                    select = 0;


                // pathCreator = path[][Point];
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