using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PM_System : MonoBehaviour
{
    private static PM_System _instance = null;
    public static PM_System instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PM_System)) as PM_System;

                if (instance == null)
                {
                    GameObject obj = new GameObject("Singleton");
                    _instance = obj.AddComponent(typeof(PM_System)) as PM_System;
                }
            }
            return _instance;
        }
    }                     
    void Awake() { 
        DontDestroyOnLoad(gameObject);
    }

    //시스템 파라미터
    [Header("[속도]")]
    public float Acceleration; //가속도
    public float Deceleration; //감속도
    public float Maxspeed;     //최대속도
    public float Speed;        //현재속도
    public float CornerSpeed;  //코너링속도

    [Header("[활력]")]
    public float Mp;
    public float MpDecrease;   //활력지속감소량
    public float MpRecovery;   //활력지속회복량

    [Header("[체력]")]
    public float Hp;           //현재체력
    public float MinHp;        //한계체력
    public float Hp_mValue;    //체력감소량
    public float Hp_pValue;    //체력증가량
    /// <summary>
    /// 체력지속감소량
    /// </summary>
    [Tooltip("체력지속감소량")]
    public float HpDecrease;   //체력지속감소량
    public float HpRecovery;   //체력지속회복량

    [Header("[내구도]")]
    public float BicHp_mValue; //내구도소모
}
