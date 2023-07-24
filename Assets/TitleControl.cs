using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{

    //public int onUpdate;
    //public string children;
    public int localDayID;
    public int localDayID2;
    public int localDayID3;
    public int localDayID4;
    public int localSubID;

    private int dayID = 0;

    void Start()
    {
        gameObject.SetActive(false);
        int dayID = PlayerPrefs.GetInt("tempDayID");
        int subID = PlayerPrefs.GetInt("tempSubID");
        if (((dayID == localDayID) || ((dayID == localDayID2) & (localDayID2 != 0)) || ((dayID == localDayID3) & (localDayID3 != 0)) || ((dayID == localDayID4) & (localDayID4 != 0))) & subID == localSubID) { gameObject.SetActive(true); }
        //Control(); 
    }
//    void Update()
//    {
//        //if (onUpdate == 1) { Debug.Log('3'); }
//        if (onUpdate == 1) { dayID = PlayerPrefs.GetInt("tempDayID"); }
//        if ((onUpdate == 1) & ((dayID == localDayID) || ((dayID == localDayID2) & (localDayID2 != 0)) || ((dayID == localDayID3) & (localDayID3 != 0)) || ((dayID == localDayID4) & (localDayID4 != 0)))) 
//        { 
//            Debug.Log('3');
//            gameObject.GetComponentsInChildren.SetActive(true); 
//        }
//        else if (onUpdate == 1)
//        {
//            Debug.Log('2');
//            gameObject.GetComponentsInChildren<children>().SetActive(false);
//        }
//        
//    }

    void Control()
    {
        //int dayID = PlayerPrefs.GetInt("tempDayID");
        //Debug.Log(dayID);
        //if ((dayID == localDayID)) { gameObject.SetActive(true); }
        // || ((dayID == localDayID2) & (localDayID2 != 0)) || ((dayID == localDayID3) & (localDayID3 != 0)) || ((dayID == localDayID4) & (localDayID4 != 0))
        //else { gameObject.SetActive(false); }
        gameObject.SetActive(true);
    }
}
