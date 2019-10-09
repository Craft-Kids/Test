using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_text_Manager : MonoBehaviour
{
    float time; 
    public Text timeText;

    public Text progressText;
    public Text RankingText;

    public Text HpText;

    public Text SpeedText;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //시간
        time += Time.deltaTime;
        timeText.text = "경과시간 :" + time.ToString("N2");

        //Hp
        HpText.text = (int)PM_System.instance.Hp + "/120" ;

        //속도
        SpeedText.text = PM_System.instance.Speed.ToString("N2") + "km/h";

        //진행도
        progressText.text = "진행도";

        //순위
        RankingText.text = "순위";
;    }

}
