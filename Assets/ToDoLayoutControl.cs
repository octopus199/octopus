using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoLayoutControl : MonoBehaviour
{

    //public int onUpdate;
    //public string children;
    public bool thisIsToggle;
    public GameObject[] obj;
    public int[] objLayout;

    public string button0Name;
    public string button1Name;

    public int localLayoutID;
    //public int[] objList;

    private int dayID = 0;
    public GameObject inGameButton0;
    public GameObject inGameButton1;

    public GameObject[] toggles;
    public GameObject[] togglesT;

    public bool thisPP;
    public GameObject PP;
    public int togglesCount;

    public Sprite DontInteractable;
    public Sprite DontInteractableChecked;

    void Start()
    {
        if (!thisIsToggle & !thisPP)
        {
            int layoutID = PlayerPrefs.GetInt("layoutID");
            if (layoutID != localLayoutID) { gameObject.SetActive(false); }
            //Control(); 
        }
        else if (thisIsToggle)
        {
            Debug.Log("Toggle ToDoLayoutControl");
            toggleControl();
            inGameButton0 = GameObject.Find(button0Name);
            inGameButton0.GetComponent<Button>().onClick.AddListener(delegate { onClick0(); });
            inGameButton1 = GameObject.Find(button1Name);
            inGameButton1.GetComponent<Button>().onClick.AddListener(delegate { onClick1(); });
            
        }
        else if (thisPP)
        {
            PP.GetComponent<Button>().onClick.AddListener(delegate { onPPClick(); });
        }
    }

    void Update()
    {
    }

    void onPPClick()
    {
        int dayID = PlayerPrefs.GetInt("tempDayID");
        string dateID = PlayerPrefs.GetString("thisDateID");
        int diamonds = PlayerPrefs.GetInt("diamondValue");
        for (int i = 0; i != togglesCount; i++)
        {
            //inGameToggle0 = GameObject.Find(("Toggle"+(i+1).ToString()+suffix));
            string saveStringName = dayID.ToString() + '+' + dateID + '+' + (i + 1).ToString();
            int checkedReturn = PlayerPrefs.GetInt(saveStringName);
            if (checkedReturn == 1)
            {
                PlayerPrefs.SetInt(saveStringName, 3);
            }
            else if (checkedReturn == 0)
            {
                PlayerPrefs.SetInt(saveStringName, 2);
            }
            else if (checkedReturn == 2)
            {
                PlayerPrefs.SetInt(saveStringName, 2);
            }
            else if (checkedReturn == 3)
            {
                PlayerPrefs.SetInt(saveStringName, 3);
            }
            else
            {
                PlayerPrefs.SetInt(saveStringName, 2);

            }

        }
        PlayerPrefs.SetInt("diamondValue", diamonds);
        PlayerPrefs.SetInt("allowChangeToDo", 0);
        PlayerPrefs.Save();
    }

    IEnumerator toggleCoroutine()
    {
        Debug.Log("Pause");
        yield return new WaitForSeconds(0.13f);
        PlayerPrefs.SetInt("animateClearAll", 1);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.14f);
        Debug.Log("Play");
        toggleControl();
    }
    void toggleControl()
    {
        int layoutID = PlayerPrefs.GetInt("layoutID");
        for (int i = 0; i != obj.Length; i++)
        {
            if (objLayout[i] == layoutID)
            {
                obj[i].SetActive(true);
            }
            else
            {
                obj[i].SetActive(false);
            }
        }
        int dayID = PlayerPrefs.GetInt("tempDayID");
        string dateID = PlayerPrefs.GetString("thisDateID");
        List<GameObject> inGameToggle = new List<GameObject>();
        if (layoutID == 1)
        {
            inGameToggle.AddRange(togglesT);
        }
        else
        {
            inGameToggle.AddRange(toggles);
        }
        int inPP = PlayerPrefs.GetInt("InPP");
        int diamonds = PlayerPrefs.GetInt("diamondValue");
        for (int i = 0; i != inGameToggle.Count; i++)
        {
            //inGameToggle0 = GameObject.Find(("Toggle"+(i+1).ToString()+suffix));
            Debug.Log(inGameToggle[i]);
            string saveStringName = dayID.ToString() + '+' + dateID + '+' + (i + 1).ToString();
            int checkedReturn = PlayerPrefs.GetInt(saveStringName);
            if (checkedReturn == 1)
            {
                inGameToggle[i].GetComponent<Toggle>().isOn = true;
            }
            else if (checkedReturn == 2)
            {
                inGameToggle[i].GetComponent<Toggle>().isOn = false;
                if (inPP == 0)
                {
                    inGameToggle[i].GetComponent<Toggle>().interactable = false;
                    //inGameToggle[i].GetComponent<Image>().sprite = DontInteractable;
                    inGameToggle[i].transform.Find("Background").gameObject.GetComponent<Image>().sprite = DontInteractable;
                }
            }
            else if (checkedReturn == 3)
            {
                inGameToggle[i].GetComponent<Toggle>().isOn = true;
                if (inPP == 0)
                {
                    inGameToggle[i].GetComponent<Toggle>().interactable = false;
                    //inGameToggle[i].GetComponent<Image>().sprite = DontInteractable;
                    inGameToggle[i].transform.Find("Background/Checkmark").gameObject.GetComponent<Image>().sprite = DontInteractableChecked;
                }
            }
            else if (checkedReturn == 0)
            {
                inGameToggle[i].GetComponent<Toggle>().isOn = false;
            }

        }
        PlayerPrefs.SetInt("onChange", 0);
        PlayerPrefs.SetInt("animateClearAll", 0);
        PlayerPrefs.SetInt("diamondValue", diamonds);
        PlayerPrefs.SetInt("allowChangeToDo", 1);
        PlayerPrefs.Save();
    }
    void onClick0()
    {
        PlayerPrefs.SetInt("layoutID", 0);
        PlayerPrefs.SetInt("onChange", 1);
        PlayerPrefs.Save();
        Debug.Log("Merge Layout!");
        StartCoroutine(toggleCoroutine());
    }
    void onClick1()
    {
        PlayerPrefs.SetInt("layoutID", 1);
        PlayerPrefs.SetInt("onChange", 1);
        PlayerPrefs.Save();
        Debug.Log("Merge Layout!");
        StartCoroutine(toggleCoroutine());
    }
    void ToDoReturn()
    {
        
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

//2
}
