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

    public Image HpBar;
    public Text HpText;

    public Text SpeedText;

    public Image MpBar;
    public Text MpText;

    public Image Limit; //한계체력눈금
    RectTransform rect;

    void Awake()
    {
        //눈금위치설정
        rect = Limit.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + HpBar.GetComponent<RectTransform>().rect.width * 0.85f, rect.anchoredPosition.y);
        Limit.GetComponent<RectTransform>().transform.position = rect.position;
    }

    // Update is called once per frame
    void Update()
    {
        //시간
        time += Time.deltaTime;
        timeText.text = "경과시간 :" + time.ToString("N2");

        //Hp
        HpText.text = (int)PM_System.instance.Hp + "/" + PM_Player.instance.MaxHp ;
        HpBar.fillAmount = PM_System.instance.Hp / PM_Player.instance.MaxHp;

        //Mp
        MpText.text = (int)PM_System.instance.Mp + "/60";
        MpBar.fillAmount = PM_System.instance.Mp / PM_Player.instance.MaxHp;//왜 60은 안될까,,

        //속도
        SpeedText.text = PM_System.instance.Speed.ToString("N2") + "km/h";

        //진행도
        progressText.text = "진행도";

        //순위
        RankingText.text = "순위";
    }
}
