using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using System.Threading.Tasks;

public class OpenScene : MonoBehaviour
{
    public int ChangeScene;
    public int RestoreTempScene;
    public int SaveSceneToTemp;
    public int SaveSceneToTemp2;
    public bool saveSceneIDFromAnim;
    public string sceneName;
    public string thisSceneName;
    public int dayID;
    public int subID;
    public string ButtonName;

    public int indexID;

    public bool sendTabletLayout;
    public bool sendHaveBack;

    public bool sendAboutThis;

    //public int TurnOnDebug;

    private AssetBundle myLoadedAssetBundle;
    private GameObject inGameButton;
    private OpenScene thisObj;
    public TextMeshProUGUI txt;
    public bool onlyConnect;
    
    private string[] scenePaths; 

    public OpenScene(string name)
    {
        Debug.Log("OpenScene Connect");
        inGameButton = GameObject.Find(name);
        thisObj = GetComponent<OpenScene>();
        ChangeScene = thisObj.ChangeScene;
        RestoreTempScene = thisObj.RestoreTempScene;
        SaveSceneToTemp = thisObj.SaveSceneToTemp;
        saveSceneIDFromAnim = thisObj.saveSceneIDFromAnim;
        sceneName = thisObj.sceneName;
        thisSceneName = thisObj.thisSceneName;
        dayID = thisObj.dayID;
        subID = thisObj.subID;
        indexID = thisObj.indexID;
        sendTabletLayout = thisObj.sendTabletLayout;
        sendHaveBack = thisObj.sendHaveBack;
        sendAboutThis = thisObj.sendAboutThis;
    }
    void Start()
    {
        if (!onlyConnect)
        {
            inGameButton = GameObject.Find(ButtonName);
            inGameButton.GetComponent<Button>().onClick.AddListener(delegate { onClick(); });
        }
    }

    public void onClick()
    {
        Debug.Log("Merge Scene");
        if (saveSceneIDFromAnim) 
        {
            if (sceneName == "")
            {
                sceneName = "Store";
                if (thisSceneName != "Store")
                {
                    sceneName = PlayerPrefs.GetString(("tempSceneID" + RestoreTempScene.ToString()));
                }
                else if (thisSceneName == "Store")
                {
                    sceneName = PlayerPrefs.GetString(("tempSceneID" + "6"));
                }
                Debug.Log("Scene Name Restored! " + sceneName);
            }
            PlayerPrefs.SetString("tempSceneID", sceneName);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("onExit", 1);
        PlayerPrefs.SetInt("sendTabletLayout", 0);
        if (sendTabletLayout == true)
        {
            PlayerPrefs.SetInt("sendTabletLayout", 1);
        }
        PlayerPrefs.Save();
        StartCoroutine(PlayAnim());
        //Thread.Sleep(1000);
        //if (indexID != 0) { }
        
    }
    
    IEnumerator PlayAnim()
    {
        if (PlayerPrefs.GetInt("animDistable") == 0)
        {
            yield return new WaitForSeconds(0.27f);
        }
        
        if (SaveSceneToTemp != 0)
        {
            PlayerPrefs.SetString(("tempSceneID" + SaveSceneToTemp.ToString()), thisSceneName);
            int dayID_back = PlayerPrefs.GetInt("tempDayID");
            int subID_back = PlayerPrefs.GetInt("tempSubID");
            PlayerPrefs.SetInt(("tempDayID_back" + SaveSceneToTemp.ToString()), dayID_back);
            PlayerPrefs.SetInt(("tempSubID_back" + SaveSceneToTemp.ToString()), subID_back);
        }
        if (SaveSceneToTemp2 != 0)
        {
            PlayerPrefs.SetString(("tempSceneID" + SaveSceneToTemp2.ToString()), thisSceneName);
            int dayID_back = PlayerPrefs.GetInt("tempDayID");
            int subID_back = PlayerPrefs.GetInt("tempSubID");
            PlayerPrefs.SetInt(("tempDayID_back" + SaveSceneToTemp2.ToString()), dayID_back);
            PlayerPrefs.SetInt(("tempSubID_back" + SaveSceneToTemp2.ToString()), subID_back);
        }
        if (RestoreTempScene != 0)
        {
            sceneName = PlayerPrefs.GetString(("tempSceneID" + RestoreTempScene.ToString()));
            dayID = PlayerPrefs.GetInt(("tempDayID_back" + RestoreTempScene.ToString()));
            subID = PlayerPrefs.GetInt(("tempSubID_back" + RestoreTempScene.ToString()));
        }
        if (ChangeScene == 1) { SceneManager.LoadScene(sceneName, LoadSceneMode.Single); }
        PlayerPrefs.SetInt("tempDayID", dayID);
        PlayerPrefs.SetInt("tempSubID", subID);
        PlayerPrefs.SetInt("tempIndexID", indexID);
        PlayerPrefs.SetInt("onExit", 0);
        PlayerPrefs.Save();
    }
}
