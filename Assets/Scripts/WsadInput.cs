using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WsadInput : IInput
{
    public WsadInput()
    {
    }

    public float Vertical()
    {
        return Input.GetAxis("Vertical");
    }
}
