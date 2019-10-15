using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("z", 10, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        //                                좌표 = 거리, 이동 애니메이션 easeType = easeInOutExpo 임, loopType = pingPong,  delay = 1
	}
}

