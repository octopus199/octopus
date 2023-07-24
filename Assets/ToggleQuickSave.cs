using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleQuickSave : MonoBehaviour
{
    public string prefsType;
    public string prefsName;
    public string value;
    public bool openSceneConnect;

    public bool prefsToggleControl;
    public GameObject thisObj;

    void Start()
    {
        string[] prefsTypeA = { prefsType };
        string[] prefsNameA = { prefsName };
        string[] valueA = { value };

        bool checkedReturn = IsActiveIfReg.Control(prefsTypeA, prefsNameA, valueA);
        if (checkedReturn)
        {
            thisObj.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            thisObj.GetComponent<Toggle>().isOn = false;
        }

        QuickSavePrefs prefs = thisObj.GetComponent<QuickSavePrefs>();
        if (prefsToggleControl)
        {
            prefs.Construct__(prefsTypeA, prefsNameA, valueA, prefsToggleControl);
            Debug.Log("prefs connected");
        }
        thisObj.GetComponent<Toggle>().onValueChanged.AddListener(delegate { prefs.Save(); });

        if (openSceneConnect)
        {
            Debug.Log("StartConnect");
            //OpenScene openScene = new OpenScene(ToggleName);
            //inGameToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { openScene.onClick(); });
            
            thisObj.GetComponent<Toggle>().onValueChanged.AddListener(delegate { StartCoroutine(OpenS()); });
        }

        

    }
    public bool isActive() 
    {
        string[] prefsTypeA = { prefsType };
        string[] prefsNameA = { prefsName };
        string[] valueA = { value };
        return IsActiveIfReg.Control(prefsTypeA, prefsNameA, valueA); 
    }
    IEnumerator OpenS()
    {
        yield return new WaitForSeconds(0.01f);
        OpenScene openScene = GetComponent<OpenScene>();
        openScene.onClick();
    }

}
