using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuickSavePrefs : MonoBehaviour
{
    public string[] type;
    public string[] prefsName;
    public string[] value;
    public bool setButton;
    public bool allowConnect;
    public bool onlyConnect;
    public bool[] changeValueOn2Click;
    public string[] value2;

    public GameObject thisObj;
    private bool toggleConnect = false;

    private GameObject inGameButton;
    private string[] tT;
    private string[] tN;
    private string[] tV;

    public void Construct__(string[]  TType, string[] TName, string[] TValue, bool connect)
    {
        if (allowConnect)
        {
            tT = TType;
            tN = TName;
            tV = TValue;
            toggleConnect = connect;
        }
    }
    void Start()
    {
        if (setButton)
        {
            thisObj.GetComponent<Button>().onClick.AddListener(delegate { Control(type, prefsName, value, changeValueOn2Click, value2, thisObj); });
        }
        else if (onlyConnect)
        {

        }
        else
        {
            Control(type, prefsName, value, changeValueOn2Click, value2, thisObj);
        }
    }

    public static void Control(string[] type, string[] prefsName, string[] value, bool[] change, string[] value2, GameObject obj)
    {
        QuickSavePrefs prefs = obj.GetComponent<QuickSavePrefs>();
        for (int i = 0; i != prefsName.Length; i++)
        {
            if (type[i] == "int")
            {
                int arg = Int32.Parse(value[i]);
                Debug.Log("arg " + arg);
                Debug.Log(change[i]);
                Debug.Log(prefs.toggleConnect);
                Debug.Log(prefsName[i]);
                Debug.Log(PlayerPrefs.GetInt(prefsName[i]));
                if (prefs.toggleConnect)
                {
                    Debug.Log(prefs.tT[0]);
                    Debug.Log(prefs.tN[0]);
                    Debug.Log(prefs.tV[0]);
                    //ToggleQuickSave toggle = GetComponent<ToggleQuickSave>();
                    if (IsActiveIfReg.Control(prefs.tT, prefs.tN, prefs.tV))
                    {
                        arg = Int32.Parse(value2[i]);
                        Debug.Log("New arg (from toggleConnect) " + arg);
                    }
                }
                else if (change[i] & PlayerPrefs.GetInt(prefsName[i]) == arg)
                {
                    arg = Int32.Parse(value2[i]);
                    Debug.Log("New arg " + arg);
                }
                PlayerPrefs.SetInt(prefsName[i], arg);
                PlayerPrefs.Save();
            }
            else if (type[i] == "string")
            {
                string lvalue = value[i];
                Debug.Log("lvalue " + lvalue);
                Debug.Log(change[i]);
                Debug.Log(prefs.toggleConnect);
                Debug.Log(prefsName[i]);
                Debug.Log(PlayerPrefs.GetString(prefsName[i]));
                if (prefs.toggleConnect)
                {
                    Debug.Log(prefs.tT);
                    Debug.Log(prefs.tN);
                    Debug.Log(prefs.tV);
                    if (IsActiveIfReg.Control(prefs.tT, prefs.tN, prefs.tV))
                    {
                        lvalue = value2[i];
                        Debug.Log("New lvalue (from toggleConnect) " + lvalue);
                    }
                }
                else if (change[i] & PlayerPrefs.GetString(prefsName[i]) == lvalue)
                {
                    lvalue = value2[i];
                    Debug.Log("New lvalue " + lvalue);
                }
                PlayerPrefs.SetString(prefsName[i], lvalue);
                PlayerPrefs.Save();
            }
        }
        Debug.Log("Save Prefs");
    }
    public void Save()
    {
        Control(type, prefsName, value, changeValueOn2Click, value2, thisObj);
    }
}
