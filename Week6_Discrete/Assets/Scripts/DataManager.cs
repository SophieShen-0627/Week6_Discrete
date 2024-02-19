using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager datas;

    [Header ("Player")]
    [Space (20)]
    public float PlayerMovingSpeed = 5f;
    public float DashPointDetectRange = 1f;

    [Header("DashPoints")]
    [Space(20)]
    public Color DashPointOriginalColor = new Color();
    public Color DashPointCloseColor = new Color();
    public Color DashPointChosenColor = new Color();

    [Header("Enemy")]
    [Space(20)]
    public float HitStopTime = 0.1f;
    public float MovingSpeed = 5f;
    public float HitStopTimeScale = 0.2f;
    private void Awake()
    {
        datas = this;
    }
}
