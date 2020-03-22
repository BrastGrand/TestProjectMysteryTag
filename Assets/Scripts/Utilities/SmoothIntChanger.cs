﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothInt64Changer
{
    public Int64 CurValue
    {
        get { return curValue; }
    }

    private Int64 curValue = 0;
    private Int64 newValue = 0;

    public void Init(Int64 newValue)
    {
        //выставляем оба, чтобы первое значение выставлялось сразу, а не набиралось
        this.curValue = newValue;
        this.newValue = newValue;
    }

    public void SetNewValue(Int64 newValue)
    {
        this.newValue = newValue;
    }


    public bool UpdateValue()
    {
        if (newValue != curValue)
        {
            float sign = Mathf.Sign(newValue - curValue);
            float increment = Mathf.Max(Mathf.Abs(newValue - curValue) * Time.deltaTime * 3.0f, 1.0f);
            curValue = curValue + (Int64)(increment * sign);
            if (Mathf.Abs(curValue - newValue) < 2)
                curValue = newValue;

            return true;
        }

        return false;
    }
}
