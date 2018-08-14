using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Clock : MonoBehaviour {

    public Transform hoursTransform, minutesTransoform, secondsTransoform;
    public bool continuous;

    const float degreesPerHour = 30f;
    const float degreesPerMinute = 6f;
    const float degreesPerSecond = 6f;

    private void Update()
    {
        if (continuous)
        {
            ContinuousUpdate();
        }
        else
        {
            DiscreteUpdate();
        }
    }

    private void DiscreteUpdate()
    {
       DateTime time = DateTime.Now;
       hoursTransform.rotation =
            Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
       minutesTransoform.rotation =
            Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
       secondsTransoform.rotation =
            Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }

    private void ContinuousUpdate()
    {
        DateTime time = DateTime.Now;
        TimeSpan timeSpan = time.TimeOfDay;
        hoursTransform.rotation =
             Quaternion.Euler(0f, (float)timeSpan.TotalHours * degreesPerHour, 0f);
        minutesTransoform.rotation =
             Quaternion.Euler(0f, (float)timeSpan.TotalMinutes * degreesPerMinute, 0f);
        secondsTransoform.rotation =
             Quaternion.Euler(0f, (float)timeSpan.TotalSeconds * degreesPerSecond, 0f);
    }

}
