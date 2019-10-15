using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Transform[] path;

    void OnDrawGizmos()
    {
        iTween.DrawPath(path);
    }

    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 10, "easetype", iTween.EaseType.linear,
            "orienttopath", true, "looktime", .6, "looptype", iTween.LoopType.loop, "movetopath", false));
    }
    
    //orienttopath = 앞에보기

    //참고 블로그임 https://mrbinggrae.tistory.com/180?category=843062
}
