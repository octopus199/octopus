using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IsActiveIfReg : MonoBehaviour
{
    public string[] type;
    public string[] prefsName;
    public string[] activeIf;

    public GameObject obj;

    void Start()
    {
        if (Control(type, prefsName, activeIf))
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }

    public static bool Control(string[] type, string[] prefsName, string[] activeIf)
    {
        for (int i = 0; i != prefsName.Length; i++)
        {
            if (type[i] == "int")
            {
                int arg = PlayerPrefs.GetInt(prefsName[i]);
                int ifA = Int32.Parse(activeIf[i]);
                if (arg == ifA)
                {
                    return true;
                }
            }
            else if (type[i] == "string")
            {
                string arg = PlayerPrefs.GetString(prefsName[i]);
                if (arg == activeIf[i])
                {
                    return true;
                }
            }
            else if (type[i] == "HasKey")
            {
                bool arg = PlayerPrefs.HasKey(prefsName[i]);
                if (activeIf[i] == "true" & arg)
                {
                    return true;
                }
                else if (activeIf[i] == "false" & !arg)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
