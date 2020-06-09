using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Common
{
    public static bool IsControllerConnected()
    {
        var joysticks = Input.GetJoystickNames().ToList();
        joysticks.RemoveAll(Blank);
        return joysticks.Count() > 0;
    }

    private static bool Blank(string s)
    {
        return s.Trim() == "";
    }


}