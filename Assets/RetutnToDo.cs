using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
//using System.IO;

public class RetutnToDo : MonoBehaviour
{
    public string category;
    public int stade;

    //private List<string> weeksList;

    //private DateTime dt;
    //private Item fromJson;

    //private SaveToDo todo;

    void Start()
    {
        gameObject.SetActive(false);
        restore();
    }

    void restore()
    {
        //fromJson = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/gameData.json"));
        //List<string> weeksList = new List<string>();
        //weeksList.AddRange(fromJson.weeks);
        int indexID = PlayerPrefs.GetInt("tempIndexID");
        string dateID = PlayerPrefs.GetString(("tempDateID" + indexID.ToString()));
        //string saveWeekStringName = saveDateCalculate();
        string saveStringName = dateID + '+' + category;
        int stadeRead = PlayerPrefs.GetInt(saveStringName);
        Debug.Log(indexID);
        Debug.Log(dateID);
        Debug.Log(saveStringName);
        Debug.Log(stadeRead);
        if ((stade == stadeRead) || ((stade == 1) & (stadeRead == 0)))
        {
            gameObject.SetActive(true);
        }
    }

//    public class Item
//    {
//        public int gameDate;
//        public string[] weeks;
//        public string[] weeksStr;
//    }
}
