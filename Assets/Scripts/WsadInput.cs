using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WsadInput : IInput
{
    public WsadInput()
    {
    }

    public float Horizontal
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    float IInput.Vertical
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }
}
