using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

//# include <iostream>;
//# include <string>;


public class SaveToDo : MonoBehaviour
{
    private GameObject inGameToggle;
    private DateTime dt { get { return DateTime.Now; } }

    private int checkedReturn;
    private string saveStringName;

    public int checkBoxNumber;
    public string ToggleName;

    //public bool thisPP;

    void Start()
    {
        inGameToggle = GameObject.Find(ToggleName);
        int dayID = PlayerPrefs.GetInt("tempDayID");
        string dateID = PlayerPrefs.GetString("thisDateID");
        saveStringName = dayID.ToString() + '+' + dateID + '+' + checkBoxNumber.ToString();

        Debug.Log(saveStringName);

        //PlayerPrefs.SetInt(saveStringName, 1);
        //PlayerPrefs.Save();

        //if (PlayerPrefs.HasKey(saveStringName))
        //{
        //    int checkedReturn = PlayerPrefs.GetInt(saveStringName);
        //    if (checkedReturn == 1)
        //    {
        //        inGameToggle.GetComponent<Toggle>().isOn = true;
        //    }
        //    else
        //    {
        //        inGameToggle.GetComponent<Toggle>().isOn = false;
        //    }
        //}
        //else
        //{
        //    int checkedReturn = 0;
        //    inGameToggle.GetComponent<Toggle>().isOn = false;
        //
        //}
        //Debug.Log(dt.ToString("R"));


        inGameToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { onIsOn(); });


    }

    public void onIsOn()
    {
        if (PlayerPrefs.GetInt("allowChangeToDo") == 1)
        {
            int trueOrFalse = 0;
            if (inGameToggle.GetComponent<Toggle>().isOn == true)
            {
                trueOrFalse = 1;
            }
            PlayerPrefs.SetInt(saveStringName, trueOrFalse);
            PlayerPrefs.Save();
            getDiamond(trueOrFalse);
        }

    }
    public void getDiamond(int operation)
    {
        int value = PlayerPrefs.GetInt("diamondValue");
        if (operation == 1)
        {
            value = value + 1;
        }
        else if (operation == 0)
        {
            value = value - 1;
        }
        PlayerPrefs.SetInt("diamondValue", value);
        PlayerPrefs.Save();
    }
}


