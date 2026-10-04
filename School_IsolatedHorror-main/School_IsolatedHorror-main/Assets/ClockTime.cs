using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockTime : MonoBehaviour
{
    private TMP_Text tmp;
    private DateTime CurTime;

    // Start is called before the first frame update
    void Start()
    {
        tmp = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CurTime = DateTime.Now;

        tmp.text = $"{CurTime.Hour.ToString("D2")}:{CurTime.Minute.ToString("D2")}";
    }
}
