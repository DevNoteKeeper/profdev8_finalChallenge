using System;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text dateTimeText;
    DateTime today;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        today = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        string dateTime = today.ToString("dd/MM/yyyy, ddd hh:mm tt");
        dateTimeText.text = dateTime;

        string time = today.ToString("hh:mm tt");
        timeText.text = time;
    }
}
