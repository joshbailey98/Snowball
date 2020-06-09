﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager
{
    public static string username;
    public static int maxLevel = 3;
    public static bool LoggedIn { get { return username != null; } }

    public static void Logout()
    {
        username = null;
    }
    
    public static string GetUsername()
    {
        return username ?? "TEST";
    }
}
