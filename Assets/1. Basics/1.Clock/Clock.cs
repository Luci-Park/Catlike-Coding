using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform hr, min, sec;

    const float hours2deg = -30f, min2deg = -6f, sec2deg = -6f;
    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hr.localRotation = Quaternion.Euler(0f, 0f, hours2deg * (float)time.TotalHours);
        min.localRotation = Quaternion.Euler(0f, 0f, min2deg * (float)time.TotalMinutes);
        sec.localRotation = Quaternion.Euler(0f, 0f, sec2deg * (float)time.TotalSeconds);
    }

}
