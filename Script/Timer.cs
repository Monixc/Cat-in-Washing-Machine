using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;

    void Start()
    {
        time = 0;
    }
    void Update()
    {
        if (Goal.goal == false)
        {
            time += Time.deltaTime;
        }
        int t = Mathf.FloorToInt(time);
        Text uiText = GetComponent<Text>();

        //��, �� ���
        int minutes = t / 60;
        int seconds = t % 60;

        // 00:00 �������� ǥ��
        uiText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
