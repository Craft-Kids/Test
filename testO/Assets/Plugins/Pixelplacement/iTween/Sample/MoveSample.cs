using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("z", 10, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        //                                ��ǥ = �Ÿ�, �̵� �ִϸ��̼� easeType = easeInOutExpo ��, loopType = pingPong,  delay = 1
	}
}

