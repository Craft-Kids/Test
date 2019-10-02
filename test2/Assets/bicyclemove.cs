using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace PathCreation.Examples
{
    public class bicyclemove : MonoBehaviour
    {
        float myspeed;
        int Point;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (myspeed < 0.5f)
                Point = 0;
            else if (myspeed < 1.5f)
                Point = 1;
            else
                Point = 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enter")
            {
                Debug.Log("g");
                myspeed = GetComponent<PathFollower>().speed;
                
            }
            if (other.gameObject.tag == "Exit")
            {
                
            }
        }
    }
}