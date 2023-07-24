using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
//using static LoadWeeks;

public class LoadWeeks : MonoBehaviour
{
    public bool TextCustomizeThisDateID;
    public bool TextCustomizeTempDateID;
    public bool TempDateIDSave;
    public bool ClicableControl;
    public int TXTIndex;
    public string buttonName;

    private FromJson fromJson;
    private DateTime dt { get { return DateTime.Now; } }
    public TextMeshProUGUI txt;
    public GameObject inGameButton;

    private int dayID;
    private int subID;

    void Start()
    {
        if (TextCustomizeTempDateID == true || TempDateIDSave == true || ClicableControl == true)
        {
            dayID = PlayerPrefs.GetInt("tempDayID");
            subID = PlayerPrefs.GetInt("tempSubID");

            string path = Application.persistentDataPath + "/gameData.json";
            string jsonString = File.ReadAllText(path);
            Debug.Log(jsonString);

            fromJson = JsonUtility.FromJson<FromJson>(jsonString);
            //fromJson = JsonUtility.FromJson<FromJson>(File.ReadAllText(Application.streamingAssetsPath + "/gameData.json"));
            List<string> weeksList = new List<string>();
            weeksList.AddRange(fromJson.weeks);

            Debug.Log(subID);
            Debug.Log(dayID);
            Debug.Log(TXTIndex);

            int yearInt = dt.Year;
            Debug.Log(yearInt);
            Debug.Log(yearInt.ToString().Length);
            string year = "0000";
            if (yearInt.ToString().Length == 1) { year = "000" + yearInt.ToString(); }
            else if (yearInt.ToString().Length == 2) { year = "00" + yearInt.ToString(); }
            else if (yearInt.ToString().Length == 3) { year = "0" + yearInt.ToString(); }
            else { year = yearInt.ToString(); }

            //string dateStr = dt.ToString("d");
            //string month = dateStr.Substring(dateStr.IndexOf('.') + 1, dateStr.LastIndexOf('.') - 3);
            //string saveStringName = month + "/" + dateStr.Substring(dateStr.LastIndexOf('.') + 1);



            //weeksList.Sort();
            //List<int> outputInt = weeksList.BinarySearch(month.ToString() + '/' + dateStr.Substring(dateStr.LastIndexOf('.') + 1));
            //Debug.Log((month.ToString() + '/' + dateStr.Substring(dateStr.LastIndexOf('.') + 1)));
            //Debug.Log(weeksList.Find(f => f == (month.ToString() + '/' + dateStr.Substring(dateStr.LastIndexOf('.') + 1))));

            List<string> weeksStrList = new List<string>();
            weeksStrList.AddRange(fromJson.weeksStr);
            //Debug.Log();
            List<string> output = new List<string>();
            List<string> outputSt = new List<string>();
            List<string> outputForP = new List<string>();
            List<string> outputStForP = new List<string>();
            //int i = 0;
            for (int i = 0; weeksList.Count != i; i++)
            {
                Debug.Log("i = " + i);
                if (weeksList[i] == (subIDCal(1, 1, 0) + '.' + year))
                {
                    output.Add(weeksStrList[i]);
                    outputSt.Add(weeksList[i]);
                    Debug.Log(weeksList[i]);
                    Debug.Log(weeksStrList[i]);

                }
                else if (weeksList[i] == (subIDCal(1, 1, -1) + '.' + year))
                {
                    outputForP.Add(weeksStrList[i]);
                    outputStForP.Add(weeksList[i]); ;
                    Debug.Log(weeksList[i]);
                    Debug.Log(weeksStrList[i]);

                }

                //i++;
            }
            List<DateTime> laterMonth = new List<DateTime>();
            laterMonth.AddRange(dateParse(outputForP, outputStForP));
            List<DateTime> thisMonth = new List<DateTime>();
            thisMonth.AddRange(dateParse(output, outputSt));
            if ((thisMonth.Count != 0) & (laterMonth.Count != 0))
            {
                Debug.Log("If!");
                for (int i = 0; laterMonth.Count != i; i++)
                {
                    if (laterMonth[i].Month == thisMonth[0].Month)
                    {
                        output.Insert(0, outputForP[i + 1]);
                    }
                }
            }
            if (output.Count >= 1)
            {
                Debug.Log("If 2!");
                string scriptOutput = "";
                string scriptOutputWeeksStr = "";
                if ((output.Count - 1) >= (TXTIndex - 1))
                {
                    scriptOutput = output[TXTIndex - 1];
                    scriptOutputWeeksStr = outputSt[TXTIndex - 1];
                    Debug.Log(output[TXTIndex - 1]);
                    Debug.Log(outputSt[TXTIndex - 1]);
                }
                else
                {
                    inGameButton = GameObject.Find(buttonName);
                    inGameButton.GetComponent<Button>().interactable = false;
                    return;
                }
                if (TextCustomizeTempDateID == true)
                {
                    txt.text = (scriptOutput + ' ' + subIDCal(1, 2, 0));
                }
                if (TempDateIDSave = true)
                {
                    //int monthInt = dt.Month;
                    //string month = "00";
                    //if (monthInt.ToString().Length == 1) { month = "0" + monthInt.ToString(); }
                    //else { month = monthInt.ToString(); }
                    //string dateID = scriptOutput + "." + month + "." + year;
                    string tempDateID = scriptOutput + "." + scriptOutputWeeksStr;
                    PlayerPrefs.SetString(("tempDateID" + TXTIndex.ToString()), tempDateID);
                    PlayerPrefs.Save();
                }
                if (ClicableControl == true)
                {
                    inGameButton = GameObject.Find(buttonName);
                    inGameButton.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                if (ClicableControl == true)
                {
                    inGameButton = GameObject.Find(buttonName);
                    inGameButton.GetComponent<Button>().interactable = false;
                }
            }
        }
        if (TextCustomizeThisDateID)
        {
            string dateID = PlayerPrefs.GetString("thisDateID");
            string dateIDS = dateID.Substring(6, 2);
            if (dateIDS.Substring(0,1) == "0")
            {
                dateIDS = dateIDS.Substring(1, 1);
            }
            Debug.Log(dateIDS);
            int month = int.Parse(dateIDS);
            int[] dayID1A = { 12, 3, 6, 9 };
            int[] dayID2A = { 1, 4, 7, 10 };
            int[] dayID3A = { 2, 5, 8, 11 };
            Debug.Log(month);
            //if (dayID1.Contains(month)) { dayID = 1; }
            //if (dayID2.Contains(month)) { dayID = 2; }
            //if (dayID3.Contains(month)) { dayID = 3; }
            for (int i = 0; i != 4; i++)
            {
                if (dayID1A[i] == month) { subID = i + 1; dayID = 1; }
                if (dayID2A[i] == month) { subID = i + 1; dayID = 2; }
                if (dayID3A[i] == month) { subID = i + 1; dayID = 3; }
            }
            Debug.Log(subID);
            txt.text = (dateID.ToString().Substring(0,5) + ' ' + subIDCal(1, 2, 0));
            ///txt.text = (dateID);
        }
        //if (output>Count == 5)
        //{
        //    
        //}
    }
    List<DateTime> dateParse(List<string> output, List<string> outputSt)
    {
        Debug.Log("dateParse Start!");
        List<DateTime> DTOut = new List<DateTime>();
        for (int i = 0; output.Count != i; i++)
        {

            //string s = "23.08.2008";

            string dateInStr = (output[i].ToString().Substring(output[i].IndexOf('-') + 1) + '.' + outputSt[i].ToString());
            DateTime date1 = DateTime.ParseExact(dateInStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            //DateTime startOfWeek = date1.AddDays(((int)(dt.DayOfWeek) * -1) + 1);
            
            //bool isSunday = dt.DayOfWeek == 0;
            //var dayOfweek = isSunday == false ? (int)dt.DayOfWeek : 7;

            DateTime startOfWeek = date1.AddDays(6);

            Debug.Log(startOfWeek.Month);
            DTOut.Add(startOfWeek);
        }
        Debug.Log("dateParse End!");
        return DTOut;

    }
    string subIDCal(int negative, int oper, int dayIDk)
    {
        Debug.Log("subIDCal Start!");
        List<string> monthL = new List<string>();
        List<string> monthSL = new List<string>();
        int dayIDk2 = 0;
        if (dayID + dayIDk <= 0)
        {
            dayIDk2 = 3;
        }
        if (dayID + dayIDk + dayIDk2 == 1) { string[] monthLA = { "12", "03", "06", "09" }; monthL.AddRange(monthLA); string[] monthSLA = { "Декабря", "Марта", "Июня", "Сентября" }; monthSL.AddRange(monthSLA); }
        else if (dayID + dayIDk + dayIDk2 == 2) { string[] monthLA = { "01", "04", "07", "10" }; monthL.AddRange(monthLA); string[] monthSLA = { "Января", "Апреля", "Июля", "Октября" }; monthSL.AddRange(monthSLA); }
        else if (dayID + dayIDk + dayIDk2 == 3) { string[] monthLA = { "02", "05", "08", "11" }; monthL.AddRange(monthLA); string[] monthSLA = { "Февраля", "Мая", "Августа", "Ноября" }; monthSL.AddRange(monthSLA); }

        int k = 0;
        if ((subID - negative) < 0)
        {
            k = 11;
            Debug.Log(subID - negative);
        }
        Debug.Log(k);
        Debug.Log(subID + k - negative);
        string month = monthL[subID + k - negative];
        string monthS = monthSL[subID + k - negative];

        Debug.Log("subIDCal End!");
        if (oper == 1)
        {
            return month;
        }
        else
        {
            return monthS;
        }
    }
        

    public class FromJson
    {
        public string[] weeks;
        public string[] weeksStr;
    }

}
