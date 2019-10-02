using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        PathCreator temp;
        int i;
        int left = 0;
        int right = 4;

        public PathCreator[] track1;
        public PathCreator[] coner1;
        public PathCreator[] track2;
        public PathCreator[] coner2;
        public PathCreator[] track3;

       // PathCreator[,] path;    

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;

        public int track = 0;
        public int select = 0;
        float distanceTravelled;

        void Start()
        {  
            pathCreator = track1[select];

            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }

        void Update()
        {
            //Debug.Log(track);
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
                default:
                    {
                        pathCreator = track3[select];
                        break;
                    }
            }

            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
                select = 1;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                select = 0;
        }

        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (col.tag == "curve") // && 오른쪽으로 회전
            //{
            //    Vector2 angles = transform.eulerAngles;

            //    if (angles.y > -5)  //플레이어가 오른쪽으로 회전한다면
            //    {
            //        SpeedCheck();
            //    }
            //    else if (angles.y < 5)  // 플레이어가 왼쪽으로 회전한다면
            //    {
            //        for (i = 0; i < 5 / 2; i++)
            //        {
            //            temp = anypath[left];
            //            anypath[left] = anypath[right];
            //            anypath[right] = temp;

            //            left++;
            //            right--;

            //            SpeedCheck();
            //        }
            //    }
            //}

                if (other.gameObject.tag == "RightEnter")
            {
                Debug.Log("g");
                distanceTravelled = 0;
                SpeedCheck();
                track++;
            }

            if (other.gameObject.tag == "LeftEnter")
            {
                Debug.Log("g");
                distanceTravelled = 0;

                for (int i = 0; i < 5 / 2; i++)
                {
                    temp = coner1[left];
                    coner1[left] = coner1[right];
                    coner1[right] = temp;

                    left++;
                    right--;

                    SpeedCheck();
                }

                track++;
            }

            if (other.gameObject.tag == "Exit")
            {
                Debug.Log("g");
                distanceTravelled = 0;
                track++;

                if (select > 2)
                    select = 1;
               // pathCreator = path[][Point];
            }
        }

        void SpeedCheck()  // 속도에 따라 라인 변경
        {
            if (speed < 1.0f)     // 속도가 1.0보다 느리면
                select = 4;     // 오른쪽(인라인) Path 5
            else if (speed >= 1.0f && speed < 1.5f)
                select = 3;     // Path 4
            else if (speed >= 1.5f && speed < 2.0f)
                select = 2;     // Path 3
            else if (speed >= 2.0f && speed < 2.5f)
                select = 1;     // Path 2
            else if (speed >= 2.5f && speed < 3.0f)
                select = 0;     // Path 1
        }
        //public PathCreator[] anypath;
        //public PathCreator pathCreator;
        //public EndOfPathInstruction endOfPathInstruction;
        //public float speed = 5;
        //public int select = 0;
        //float distanceTravelled;
        //int Point;

        //void Start()
        //{
        //    if (anypath != null)
        //    {
        //        pathCreator = anypath[select];
        //        // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
        //        pathCreator.pathUpdated += OnPathChanged;
        //    }
        //}

        //void Update()
        //{
        //    pathCreator = anypath[select];
        //    if (pathCreator != null)
        //    {
        //        distanceTravelled += speed * Time.deltaTime;
        //        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        //        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        //    }
        //    if (Input.GetKeyDown(KeyCode.RightArrow) && select < 2)
        //        select++;
        //    if (Input.GetKeyDown(KeyCode.LeftArrow) && select > 0)
        //        select--;


        //}

        //// If the path changes during the game, update the distance travelled so that the follower's position on the new path
        //// is as close as possible to its position on the old path
        //void OnPathChanged()
        //{
        //    distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        //}

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "RightEnter")
        //    {
        //        Debug.Log("g");
        //        if (speed < 0.5f)
        //            select = 2;
        //        else if (speed < 1.5f)
        //            select = 1;
        //        else
        //            select = 0;
        //    }
        //    if (other.gameObject.tag == "LeftEnter")
        //    {
        //        Debug.Log("g");
        //        if (speed < 0.5f)
        //            select = 0;
        //        else if (speed < 1.5f)
        //            select = 1;
        //        else
        //            select = 2;
        //    }

        //}


        /*------------------2번---------------------------------*/
        //int Point;

        //public PathCreator[] path1;
        //public PathCreator[] path2;
        //public PathCreator coner;
        //public PathCreator pathCreator;
        //public EndOfPathInstruction endOfPathInstruction;
        //public float speed = 5;
        //public int select = 2;
        //float distanceTravelled;

        //void Start()
        //{
        //        pathCreator = path1[select];
        //        // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
        //        pathCreator.pathUpdated += OnPathChanged;
        //}

        //void Update()
        //{

        //    if (pathCreator != null)
        //    {
        //        distanceTravelled += speed * Time.deltaTime;
        //        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        //        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        //    }
        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //        select = 1;
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //        select = 0;

        //    if (speed < 0.5f)
        //        Point = 0;
        //    else if (speed < 1.5f)
        //        Point = 1;
        //    else
        //        Point = 2;
        //}

        //// If the path changes during the game, update the distance travelled so that the follower's position on the new path
        //// is as close as possible to its position on the old path
        //void OnPathChanged()
        //{
        //    distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        //}


        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Enter")
        //    {
        //        Debug.Log("g");
        //        distanceTravelled = 0;
        //        pathCreator = coner;
        //    }
        //    if (other.gameObject.tag == "Exit")
        //    {
        //        distanceTravelled = 0;
        //        pathCreator = path2[Point];
        //    }
        //}

        /*-----------1번----------------------------------------*/
        //public PathCreator []anypath;
        //public PathCreator pathCreator;
        //public EndOfPathInstruction endOfPathInstruction;
        //public float speed = 5;
        //public int select = 0;
        //float distanceTravelled;

        //void Start() {
        //    if (anypath != null)
        //    {
        //        pathCreator = anypath[select];
        //        // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
        //        pathCreator.pathUpdated += OnPathChanged;
        //    }
        //}

        //void Update()
        //{
        //    pathCreator = anypath[select];
        //    if (pathCreator != null)
        //    {
        //        distanceTravelled += speed * Time.deltaTime;
        //        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        //        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        //    }
        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //        select = 1;
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //        select = 0;
        //}

        //// If the path changes during the game, update the distance travelled so that the follower's position on the new path
        //// is as close as possible to its position on the old path
        //void OnPathChanged() {
        //    distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        //}
    }
}