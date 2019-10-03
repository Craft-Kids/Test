using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_text : MonoBehaviour
{
    float time;    
    Text timeText;

    Text progressText;
    Text RankingText;
    Text HpText;

    void Start()
    {
        timeText = GameObject.Find("Time").GetComponent<Text>();
        progressText = GameObject.Find("Progress").GetComponent<Text>();
        RankingText = GameObject.Find("Ranking").GetComponent<Text>();
        HpText = GameObject.Find("Hp_text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //시간
        time += Time.deltaTime;
        timeText.text = "경과시간 :" + time;

        //Hp
        HpText.text = "체력량";

        //진행도
        progressText.text = "진행도";

        //순위
        RankingText.text = "순위";
;    }
}
