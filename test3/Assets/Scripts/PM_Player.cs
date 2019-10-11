using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Player : MonoBehaviour
{
    private static PM_Player _instance = null;
    public static PM_Player instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PM_Player)) as PM_Player;

                if (instance == null)
                {
                    GameObject obj = new GameObject("Singleton");
                    _instance = obj.AddComponent(typeof(PM_Player)) as PM_Player;
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public enum CharacterType //캐릭터 특화 타입
    {
        Accel,
        Cornering,
        Hp,
        Combo
    };

    public float MaxHp; //최대체력
    public float CornerAbil;   //코너링능력
    public float AcelAbil;     //가속능력
    public float DcelAbil;     //감속능력
    public float ComboAbil;    //콤보능력
}
