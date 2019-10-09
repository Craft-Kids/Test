using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Bicycle: MonoBehaviour
{
    private static PM_Bicycle _instance = null;
    public static PM_Bicycle instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PM_Bicycle)) as PM_Bicycle;

                if (instance == null)
                {
                    GameObject obj = new GameObject("Singleton");
                    _instance = obj.AddComponent(typeof(PM_Bicycle)) as PM_Bicycle;
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public enum BicycleType //캐릭터 특화 타입
        {
            Hp,         //내구성
            Stabil,     //안정성
            AirResi,    //공기저항
            Lightweight,//경량
            ComboAbil   //콤보능력
        };

        public float Weight;       //무게
        public float AirResist;    //공기저항능력
        public float Stability;    //안정성
        public float BicHp;        //내구성
        public float ComboEffect;  //콤보효과
        public float Shifting;     //변속
}
