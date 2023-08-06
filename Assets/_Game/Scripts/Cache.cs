using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Cache
{

    //Cache for Coroutine

    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if (!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------

    //Cache for String

    private static Dictionary<string, string> dict_string = new Dictionary<string, string>();

    public static string GetString(string key)
    {
        if (!dict_string.ContainsKey(key))
        {
            dict_string[key] = new string(key);
        }

        return dict_string[key];
    }

    //-------------------------------------------------------------------------------------------------------------

}
