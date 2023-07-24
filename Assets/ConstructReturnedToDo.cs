using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using System;
using System.IO;
//using System.Text.Json;

public class ConstructReturnedToDo : MonoBehaviour
{

    private List<string> weeksList;

    private DateTime dt { get { return DateTime.Now; } }
    private Item fromJson;

    //private int gameData;
    //private string[] weeks;
    //private string[] weeksStr;

    //internal void PermissionCallbacks_PermissionGranted(string permissionName)
    //{

    //}
    void Start()
    {
        string newDateID = newThisDateID();
        string dateID = PlayerPrefs.GetString("thisDateID");
        if (dateID == "")
        {
            //GetCalendar();
            newDateID = newThisDateID();
            PlayerPrefs.SetString("thisDateID", newDateID);
            PlayerPrefs.SetInt("layoutID", 1);
            PlayerPrefs.Save();

            Item toJson = new Item();
            toJson.weeks = new[] { "test" };
            toJson.weeksStr = new[] { "test" };
            
            string jsonString = JsonUtility.ToJson(toJson);
            ///string jsonString = Json.JsonSerializer.Serialize(toJson);
            File.WriteAllText(Application.persistentDataPath + "/gameData.json", jsonString);
            //fromJson = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/gameData.json"));
            //List<string> weeksList = new List<string>();
            //List<string> weeksStrList = new List<string>();
            //weeksList.Add("0");
            //weeksStrList.Add("0");
            //fromJson.weeks = weeksList.ToArray();
            //fromJson.weeksStr = weeksStrList.ToArray();
            //fromJson.gameDate = 0;
            Debug.Log(jsonString);


            Debug.Log("0");
        }
        else if (dateID != newDateID)
        {
            returnToDo();
            PlayerPrefs.SetString("thisDateID", newDateID);
            PlayerPrefs.Save();
            Debug.Log("1");
        }
        return;
        
    }

    void returnToDo()
    {
        //read & return

        string dateID = PlayerPrefs.GetString("thisDateID");
        string dayToDo(string dayID, int checkBoxNumber) { return (dayID + '+' + dateID + '+' + checkBoxNumber.ToString()); }

        for (int category = 0; category != 4; category++)
        {
            List<int> checkBoxNumber = new List<int>();
            if (category == 0) { checkBoxNumber.Add(1); }
            else if (category == 1) { checkBoxNumber.Add(2); checkBoxNumber.Add(3); checkBoxNumber.Add(4); }
            else if (category == 2) { checkBoxNumber.Add(5); checkBoxNumber.Add(6); checkBoxNumber.Add(7); }
            else if (category == 3) { checkBoxNumber.Add(8); checkBoxNumber.Add(9); checkBoxNumber.Add(10); checkBoxNumber.Add(11); }

            int i = 0;
            List<string> idSaveStrings = new List<string>();
            int[] checkBoxNumberAr = checkBoxNumber.ToArray();
            while (i != checkBoxNumberAr.Length)
            {
                i++;
                int dayID = 1;
                for (int num = 0; num < 7; num++)
                {
                    idSaveStrings.Add(dayToDo(dayID.ToString(), checkBoxNumberAr[i - 1]));
                    //Debug.Log(idSaveStrings[idSaveStrings.Count - 1]);
                    dayID++;
                }
            }
            i = 0;
            int intChecked = 0;
            foreach (var v in idSaveStrings)
            {
                int checkedReturn = PlayerPrefs.GetInt(idSaveStrings[i]);
                if (checkedReturn == 1) { intChecked++; }
                //PlayerPrefs.DeleteKey(idSaveStrings[i]);
                i++;
            }

            string categoryStr = "";
            if (category == 0) { categoryStr = "home"; }
            else if (category == 1) { categoryStr = "rest"; }
            else if (category == 2) { categoryStr = "school"; }
            else if (category == 3) { categoryStr = "development"; }

            int saveInt = 0;
            string saveStringName = dateID + '+' + categoryStr;
            if (intChecked >= 6 || (intChecked >= 4 & categoryStr == "school")) { saveInt = 3; }
            else if (((intChecked == 4) || (intChecked == 5) & (categoryStr != "school")) || (((intChecked == 1) || (intChecked == 2) || (intChecked == 3)) & (categoryStr == "school"))) { saveInt = 2; }
            else if (((intChecked == 1) || (intChecked == 2) || (intChecked == 3)) & (categoryStr != "school")) { saveInt = 1; }
            else if (intChecked == 0) { saveInt = 0; }

            PlayerPrefs.SetInt(saveStringName, saveInt);
            PlayerPrefs.Save();
            Debug.Log(saveStringName);
        }

        //save & clear

        //string path = Item.Combine(Application.streamingAssetsPath, "gameData.json");
        //var loadingRequest = UnityWebRequest.Get(path);
        //loadingRequest.SendWebRequest();
        //while (!loadingRequest.isDone && !loadingRequest.isNetworkError && !loadingRequest.isHttpError) ;
        //string = System.Text.Encoding.UTF8.GetString(loadingRequest.downloadHandler.data);

        //fromJson = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "gameData.json"));
        ///string path = Path.Combine(Application.streamingAssetsPath, "gameData.json");
        ///var fromJson = UnityWebRequest.Get(path);
        string path = Application.persistentDataPath + "/gameData.json";
        string jsonString = File.ReadAllText(path);
        Debug.Log(jsonString);

        //string tempPath = Path.Combine(Application.persistentDataPath + "/data/", "gameData" + ".txt");
        //jsonByte = File.ReadAllBytes(tempPath);
        //Debug.Log(jsonByte);

        fromJson = JsonUtility.FromJson<Item>(jsonString);

        List<string> weeksList = new List<string>();
        List<string> weeksStrList = new List<string>();
        weeksList.AddRange(fromJson.weeks);
        weeksStrList.AddRange(fromJson.weeksStr);
        weeksStrList.Add(dateID.Substring(0,5));
        weeksList.Add(dateID.Substring(6));
        fromJson.weeks = weeksList.ToArray();
        fromJson.weeksStr = weeksStrList.ToArray();
        Debug.Log(weeksList);

        //string tempPath = Path.Combine(Application.persistentDataPath + "/data/", dataFileName + ".txt");
        //string fromJsonStr
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", JsonUtility.ToJson(fromJson));
        ///fromJson.SendWebRequest();
        ///while (!fromJson.isDone && !fromJson.isNetworkError && !fromJson.isHttpError) ;
        //string = System.Text.Encoding.UTF8.GetString(loadingRequest.downloadHandler.data);
        //File.WriteAllText(Application.streamingAssetsPath + "gameData.json", JsonUtility.ToJson(fromJson));
    }

    string newThisDateID()
    {
        //var dayInWeek = ((int)dt.DayOfWeek == 0) ? 7 : (int)dt.DayOfWeek;
        //string finalDayInWeek = "00";
        //string finalLastDayInWeek = "00";
        //if (dayInWeek.ToString().Length == 1) { finalDayInWeek = "0" + dayInWeek.ToString(); }
        //else { finalDayInWeek = dayInWeek.ToString; }
        //Debug.Log(finalDayInWeek);
        //string dateStr = dt.ToString("d");

        //int dayStr2IntWin = dateStr.IndexOf('.');
        //string dayDateStr = dateStr.Substring(0, dayStr2IntWin);
        int dow = (int)dt.DayOfWeek;
        int dayInWeek = 0;
        if (dow== 0) { dayInWeek = 7; }
        else { dayInWeek = dow; }

        int dayDate = dt.Day;
        //Debug.Log(dayDate);
        //Debug.Log(dayInWeek);
        string mondayDateS = dt.AddDays(((int)DayOfWeek.Monday - dayInWeek)).Day.ToString();
        //Debug.Log(mondayDateInt);
        string mondayDate = "00";
        if (mondayDateS.Length == 1) { mondayDate = "0" + mondayDateS; }
        else { mondayDate = mondayDateS; }
        //Debug.Log(mondayDate);
        //Debug.Log(mondayDateInt + 6);
        string sundayDateS = dt.AddDays(((int)DayOfWeek.Monday + 6 - dayInWeek)).Day.ToString();
        string sundayDate = "00";
        if (sundayDateS.Length == 1) { sundayDate = "0" + sundayDateS; }
        else { sundayDate = sundayDateS; }
        //Debug.Log(sundayDate);

        int monthInt = dt.Month;
        //Debug.Log(monthInt);
        string month = "00";
        if (monthInt.ToString().Length == 1) { month = "0" + monthInt.ToString(); }
        else { month = monthInt.ToString(); }
        //Debug.Log(month);

        int yearInt = dt.Year;
        //Debug.Log(yearInt);
        //Debug.Log(yearInt.ToString().Length);
        string year = "0000";
        if (yearInt.ToString().Length == 1) { year = "000" + yearInt.ToString(); }
        else if (yearInt.ToString().Length == 2) { year = "00" + yearInt.ToString(); }
        else if (yearInt.ToString().Length == 3) { year = "0" + yearInt.ToString(); }
        else { year = yearInt.ToString(); }
        //Debug.Log("000" + yearInt.ToString());
        //Debug.Log(year);

        //string saveWeekStringName = mondayDate + "-" + sundayDate + "/" + month + "/" + dateStr.Substring(dateStr.LastIndexOf('.') + 1);
        //Debug.Log(saveWeekStringName);
        //return saveWeekStringName;

        Debug.Log(mondayDate + "-" + sundayDate + "." + month + "." + year);
        return (mondayDate + "-" + sundayDate + "." + month + "." + year);
        //return (dateStr);
    }

    List<DateTime> dateParse(List<string> output, List<string> outputSt)
    {
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
        return DTOut;

    }
    void GetCalendar()
    {
//    #if UNITY_ANDROID
//        List<bool> permissions = new List<bool>() { false, false };
//        List<bool> permissionsAsked = new List<bool>() { false, false };
//        List<Action> actions = new List<Action>() {
//            new Action(() => {
//                permissions[0] = Permission.HasUserAuthorizedPermission(Permission.Write_Calendar);
//                if (true && !permissionsAsked[0])
//                {
//                    Permission.RequestUserPermission(Permission.Write_Calendar);
//                    permissionsAsked[0] = true;
//                    return;
//                }
//            }),
//            new Action(() => {
//                permissions[1] = Permission.HasUserAuthorizedPermission(Permission.Read_Calendar);
//                if (true && !permissionsAsked[1])
//                {
//                    Permission.RequestUserPermission(Permission.Read_Calendar);
//                    permissionsAsked[0] = true;
//                    return;
//                }
//            })
//        };
//        actions[0].Invoke();
//        yield return new WaitForEndOfFrame();
//    #endif
    }

    public class Item
    {
        public string[] weeks;
        public string[] weeksStr;
    }
}
//public class ItemFile
//{
//    public string[]? weeks { get; set; }
//    public string[]? weeksStr { get; set; }
//}
