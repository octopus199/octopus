//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.Serialization;
//using Object = UnityEngine.Object;

public class QuickAnim : MonoBehaviour
{

    //mm - Min, Max. 
    //Min - on switch to thumbnail view.
    //Max - on switch to full view. 
    public bool mmIsActive = false;
    public bool mmUseMax = true;
    public bool mmUseMin = true;
    public bool mmSpawnUseJE = false;
    public string mmSpawnUseJEMM;
    public bool mmSpawnMin = false;
    public Object[] mmBindButtons;
    public bool[] mmBindButtonsInvert;
    public Object[] mmAnotherObj;
    public Object[] mmInvisibleOnMax;
    public Object[] mmInvisibleOnMin;
    public Object[] mmActiveOnMax;
    public Object[] mmActiveOnMin;
    public string mmAVNToMax;
    public string mmAVNMaxWait;
    public string mmAVNToMin;
    public string mmAVNMinWait;
    public string mmAVNGoInvisible;
    public string mmAVNGoVisible;
    public string mmPrefs;
    public string mmPrefsToMaxValue;
    public string mmPrefsToMinValue;
    public enum EO_mmSpawnUseJEMM
    {
        Max = 0,
        Min = 1
    }

    //je - Join, Exit. 
    //Join - on open scene.
    //Exit - on exit scene.
    public bool jeIsActive;
    public bool jeUseJoin = true;
    public bool jeUseExit = true;
    public bool jeTurnOffAnimatorAfterJoin;
    public bool jeLayoutSwitchEnabled;
    public string jeTabletRestore = "DoNotRestore";
    public string jeThisObjectLayout;
    public bool jeLayoutChange;
    public Object[] jeLayoutChangeBindButtonsToTablet;
    public Object[] jeLayoutChangeBindButtonsToClassic;
    public Object[] jeLayoutSwitchAnotherObjects;
    public int[] jeLayoutSwitchAnotherObjectsLayout;
    public string[] jeLayoutScenesAlwaysTablet;
    public string[] jeLayoutScenesAlwaysClassic;
    public bool jeAnimIfPrefs;
    public string[] jeAnimIfPrefsType;
    public string[] jeAnimIfPrefsName;
    public string[] jeAnimIfPrefsValue;
    public bool jeWaitBeforeJoin;
    public bool jeWaitBeforeExit;
    public float jeWaitBeforeJoinSeconds = 0.0f;
    public float jeWaitBeforeExitSeconds = 0.0f;
    public float jeDurationJoinSeconds = 0.2f;
    public float jeDurationExitSeconds = 0.2f;
    public string jeAVNJoin;
    public string jeAVNExit;
    public enum EO_jeTabletRestore
    {
        DoNotRestore = 0,
        RestoreFromReg = 1,
        RestoreFromPreviousScene = 2
    }
    public enum EO_jeThisObjectLayout
    {
        Classic = 0,
        Tablet = 1
    }

    //another
    public string aAVNInvisible;
    public string aAVNVisible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MM(string operation)
    {
        //Operation spawn. On open scene.
        if (operation == "spawn")
        {
            //if animations turned on in preferences.
            if (PlayerPrefs.GetInt("animDistable") == 0)
            {
                //Spawn max
                if (mmUseMax & ((!mmSpawnMin & !mmSpawnUseJE) || (mmSpawnUseJE & mmSpawnUseJEMM == "Max")))
                {
                    if (!mmSpawnMin & !mmSpawnUseJE) 
                    {
                        GetComponent<Animator>().SetBool(mmAVNToMax, true);
                    } else
                    {
                        GetComponent<Animator>().SetBool(jeAVNJoin, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }

                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Animator>().SetBool(mmAVNToMax, false);
                    GetComponent<Animator>().SetBool(jeAVNJoin, false);
                    if (!mmSpawnMin & !mmSpawnUseJE)
                    {
                        GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    } else
                    {
                        GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, false);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, false);
                    }
                }
                //Spawn min
                else if (mmUseMin & ((mmSpawnMin & !mmSpawnUseJE) || (mmSpawnUseJE & mmSpawnUseJEMM == "Min")))
                {
                    if (mmSpawnMin & !mmSpawnUseJE)
                    {
                        GetComponent<Animator>().SetBool(mmAVNToMin, true);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(jeAVNJoin, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }

                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Animator>().SetBool(mmAVNToMax, false);
                    GetComponent<Animator>().SetBool(jeAVNJoin, false);
                    if (!mmSpawnMin & !mmSpawnUseJE)
                    {
                        GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, false);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, false);
                    }
                }
            }
            //if animations turned off in preferences.
            else if (PlayerPrefs.GetInt("animDistable") == 1)
            {
                //Spawn max
                if (mmUseMax & ((!mmSpawnMin & !mmSpawnUseJE) || (mmSpawnUseJE & mmSpawnUseJEMM == "Max")))
                {
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject)mmActiveOnMin[i]).SetActive(false);
                    }

                    if (!mmSpawnMin & !mmSpawnUseJE)
                    {
                        GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }
                }
                //Spawn min
                else if (mmUseMin & ((mmSpawnMin & !mmSpawnUseJE) || (mmSpawnUseJE & mmSpawnUseJEMM == "Min")))
                {
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }

                    if (mmSpawnMin & !mmSpawnUseJE)
                    {
                        GetComponent<Animator>().SetBool(mmAVNMinWait, true);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }
                }
            }
        }
        //Operation update. On update prefs
        else if (operation == "update")
        {
            //if animations turned on in preferences.
            if (PlayerPrefs.GetInt("animDistable") == 0)
            {
                //Spawn max
                if (mmUseMax & PlayerPrefs.GetString(mmPrefs) == mmPrefsToMaxValue)
                {
                    GetComponent<Animator>().SetBool(mmAVNMinWait, false);
                    GetComponent<Animator>().SetBool(aAVNVisible, false);
                    GetComponent<Animator>().SetBool(aAVNInvisible, false);
                    GetComponent<Animator>().SetBool(mmAVNToMax, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMinWait, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }

                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Animator>().SetBool(mmAVNToMax, false);
                    GetComponent<Animator>().SetBool(mmAVNMaxWait, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMax, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, false);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, false);
                    }
                }
                //Spawn min
                else if (mmUseMin & PlayerPrefs.GetString(mmPrefs) == mmPrefsToMinValue)
                {
                    GetComponent<Animator>().SetBool(mmAVNMaxWait, false);
                    GetComponent<Animator>().SetBool(aAVNVisible, false);
                    GetComponent<Animator>().SetBool(aAVNInvisible, false);
                    GetComponent<Animator>().SetBool(mmAVNToMax, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMin, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }

                    yield return new WaitForSeconds(0.2f);
                    GetComponent<Animator>().SetBool(mmAVNToMin, false);
                    GetComponent<Animator>().SetBool(mmAVNMinWait, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNToMin, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMinWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(mmAVNGoInvisible, false);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(mmAVNGoVisible, false);
                    }
                }
            }
            //if animations turned off in preferences.
            else if (PlayerPrefs.GetInt("animDistable") == 1)
            {
                //Spawn max
                if (mmUseMax & PlayerPrefs.GetString(mmPrefs) == mmPrefsToMaxValue)
                {
                    GetComponent<Animator>().SetBool(mmAVNMinWait, false);
                    GetComponent<Animator>().SetBool(aAVNVisible, false);
                    GetComponent<Animator>().SetBool(aAVNInvisible, false);
                    GetComponent<Animator>().SetBool(mmAVNMaxWait, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMinWait, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }
                }
                //Spawn min
                else if (mmUseMin & PlayerPrefs.GetString(mmPrefs) == mmPrefsToMinValue)
                {
                    GetComponent<Animator>().SetBool(mmAVNMaxWait, false);
                    GetComponent<Animator>().SetBool(aAVNVisible, false);
                    GetComponent<Animator>().SetBool(aAVNInvisible, false);
                    GetComponent<Animator>().SetBool(mmAVNMinWait, true);

                    for (int i = 0; i != mmAnotherObj.Length; i++)
                    {
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMaxWait, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmAnotherObj[i]).GetComponent<Animator>().SetBool(mmAVNMinWait, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMin.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNVisible, false);
                        ((GameObject) mmInvisibleOnMax[i]).GetComponent<Animator>().SetBool(aAVNInvisible, true);
                    }
                    for (int i = 0; i != mmInvisibleOnMax.Length; i++)
                    {
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNInvisible, false);
                        ((GameObject) mmInvisibleOnMin[i]).GetComponent<Animator>().SetBool(aAVNVisible, true);
                    }
                    for (int i = 0; i != mmActiveOnMin.Length; i++)
                    {
                        ((GameObject) mmActiveOnMax[i]).SetActive(true);
                    }
                    for (int i = 0; i != mmActiveOnMax.Length; i++)
                    {
                        ((GameObject) mmActiveOnMin[i]).SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator JE(string operation)
    {
        //On open scene or prefs change
        if (operation == "Join" & jeUseJoin)
        {
            //if animations turned on in preferences.
            if (PlayerPrefs.GetInt("animDistable") == 0)
            {
                if (mmIsActive)
                {
                    StartCoroutine(MM());
                } else
                {
                    GetComponent<Animator>().SetBool(jeAVNJoin, true);
                }
                string layout;
                //Layout restore: Classic / Tablet.
                if (jeTabletRestore == "RestoreFromReg")
                {
                    layout = PlayerPrefs.GetString("scene_PreviousSceneLayout");
                }
                else if (jeTabletRestore == "RestoreFromPreviousScene")
                {

                }

                yield return new WaitForSeconds(jeDurationJoinSeconds);

                if (jeTurnOffAnimatorAfterJoin)
                {
                    yield return new WaitFoeSeconds(0.01f);
                    GetComponent<Animator>().enabled("false");
                }
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(QuickAnim))]
public class CustomGUIEditor : Editor
{
    QuickAnim _target;

    private void OnEnable()
    {
        _target = target as QuickAnim;
        
    }
    public QuickAnim.EO_mmSpawnUseJEMM EO_mmSpawnUseJEMM;
    public QuickAnim.EO_jeTabletRestore EO_jeTabletRestore;
    public QuickAnim.EO_jeThisObjectLayout EO_jeThisObjectLayout;

    //mm
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmBindButtons = new EditorGUILayoutArrays.ArrayFieldSettings("  |  Buttons go this to Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmBindButtonsInvert = new EditorGUILayoutArrays.ArrayFieldSettings("  |  Inverse (go this to Min)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmAnotherObj = new EditorGUILayoutArrays.ArrayFieldSettings("  |  Another GameObject's going to Min/Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmInvisibleOnMax = new EditorGUILayoutArrays.ArrayFieldSettings("  |  GameObject's invisible on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmInvisibleOnMin = new EditorGUILayoutArrays.ArrayFieldSettings("  |  GameObject's invisible on Min");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmActiveOnMax = new EditorGUILayoutArrays.ArrayFieldSettings("  |  GameObject's active on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmActiveOnMin = new EditorGUILayoutArrays.ArrayFieldSettings("  |  GameObject's active on Min");

    //js
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jsAnimIfPrefsType = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Prefs value type (for example: int)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jsAnimIfPrefsName = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |   Prefs name (for excample: importantInt)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jsAnimIfPrefsValue = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Prefs value for start anim (for excample: 0)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutChangeBindButtonsToTablet = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |    |  Buttons switch layout to Tablet");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutChangeBindButtonsToClassic = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |    |  Buttons switch layout to Classic");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutSwitchAnotherObjects = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Another GameObject's switch layout");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutSwitchAnotherObjectsLayout = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Another GameObject layout's (int)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutScenesAlwaysTablet = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Scenes, always Tablet");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_jeLayoutScenesAlwaysClassic = new EditorGUILayoutArrays.ArrayFieldSettings("  |    |  Scenes, always Classic");

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Min and Max anim's - anim's for switch this object view to thumbnail (in some case go invisible) or full view.", MessageType.Info);
        _target.mmIsActive = EditorGUILayout.ToggleLeft("Use Min and Max anim's", _target.mmIsActive);
        if (_target.mmIsActive)
        {
            _target.mmUseMax = EditorGUILayout.Toggle("  |  Use Max animation", _target.mmUseMax);
            _target.mmUseMin = EditorGUILayout.Toggle("  |  Use Min animation", _target.mmUseMin);
            _target.mmSpawnUseJE = EditorGUILayout.Toggle("  |  Use Join for Spawn", _target.mmSpawnUseJE);
            if (!_target.mmSpawnUseJE)
            {
                _target.mmSpawnMin = EditorGUILayout.Toggle("  |    |  Spawn in Min", _target.mmSpawnMin);
            } else if (_target.mmSpawnUseJE & _target.jeIsActive)
            {
                EO_mmSpawnUseJEMM = (QuickAnim.EO_mmSpawnUseJEMM)EditorGUILayout.EnumPopup("  |    |  After Join layout:", EO_mmSpawnUseJEMM);
            } else 
            {
                EditorGUILayout.HelpBox("You must enable Join and Exit, or uncheck 'Use Join for Spawn'!", MessageType.Warning);
            }
            EditorGUILayout.HelpBox(" |   Bind Buttons ", MessageType.None);
            _target.mmBindButtons = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmBindButtons, _target.mmBindButtons,"      -  Size", "      -  GameObject");
            _target.mmBindButtonsInvert = EditorGUILayoutArrays.BooleanArrayField(AFS_mmBindButtonsInvert, _target.mmBindButtonsInvert, "      -  Size", "      -  GameObject");
            EditorGUILayout.HelpBox(" |   Another GameObject's properties ", MessageType.None);
            _target.mmAnotherObj = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmAnotherObj, _target.mmAnotherObj, "      -  Size", "      -  GameObject");
            _target.mmInvisibleOnMax = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmInvisibleOnMax, _target.mmInvisibleOnMax, "      -  Size", "      -  GameObject");
            _target.mmInvisibleOnMin = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmInvisibleOnMin, _target.mmInvisibleOnMin, "      -  Size", "      -  GameObject");
            _target.mmActiveOnMax = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmActiveOnMax, _target.mmActiveOnMax, "      -  Size", "      -  GameObject");
            _target.mmActiveOnMin = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmActiveOnMin, _target.mmActiveOnMin, "      -  Size", "      -  GameObject");
            EditorGUILayout.HelpBox(" |   Anim Value Name's: ", MessageType.None);
            _target.mmAVNToMax = EditorGUILayout.TextField("  |  To Max: ", _target.mmAVNToMax);
            _target.mmAVNMaxWait = EditorGUILayout.TextField("  |  Max standby mode: ", _target.mmAVNMaxWait);
            _target.mmAVNToMin = EditorGUILayout.TextField("  |  To Min: ", _target.mmAVNToMin);
            _target.mmAVNMinWait = EditorGUILayout.TextField("  |  Min standby mode: ", _target.mmAVNMinWait);
            _target.mmAVNGoInvisible = EditorGUILayout.TextField("  |  Go Invisible: ", _target.mmAVNGoInvisible);
            _target.mmAVNGoVisible = EditorGUILayout.TextField("  |  Go Visible: ", _target.mmAVNGoVisible);
            EditorGUILayout.HelpBox(" |   Pref's for start animation: ", MessageType.None);
            _target.mmPrefs = EditorGUILayout.TextField("  |  Pref: ", _target.mmPrefs);
            _target.mmPrefsToMaxValue = EditorGUILayout.TextField("  |  Value to Max: ", _target.mmPrefsToMaxValue);
            _target.mmPrefsToMinValue = EditorGUILayout.TextField("  |  Value to Min: ", _target.mmPrefsToMinValue);
            EditorGUILayout.HelpBox("\n", MessageType.None);
        }
        EditorGUILayout.HelpBox("Join and Exit anim's - anim's for opening or closing the scene.", MessageType.Info);
        _target.jeIsActive = EditorGUILayout. ToggleLeft("Use Join and Exit anim's", _target.jeIsActive);
        if(_target.jeIsActive)
        {
            _target.jeUseJoin = EditorGUILayout.Toggle("  |  Use Join animation", _target.jeUseJoin);
            _target.jeUseExit = EditorGUILayout.Toggle("  |  Use Exit animation", _target.jeUseExit);
            EditorGUILayout.HelpBox(" |   Turn off Animator script after Join.", MessageType.None);
            _target.jeTurnOffAnimatorAfterJoin = EditorGUILayout.Toggle("  |  Turn off Animator", _target.jeTurnOffAnimatorAfterJoin);
            EditorGUILayout.HelpBox(" |   Enable switch layout. Layouts:\n     -  Classic - Classic or old layout.\n     -  Tablet - Tablet or new layout.\n         Differ from Classic by a white translucent background.", MessageType.None);
            _target.jeLayoutSwitchEnabled = EditorGUILayout.Toggle("  |  Enable switch layout", _target.jeLayoutSwitchEnabled);
            if(_target.jeLayoutSwitchEnabled)
            {
                EditorGUILayout.HelpBox(" |    |   Tablet layout restore mode:\n         -  Do Not Restore - Don't restore tablet layout.\n         -  Restore From Reg - Restore tablet layout from preferences.\n         -  Restore From Previous Scene - This script checks previous\n             scene, if tablet layout in previous scene is active,\n             it's aply also for this scene.", MessageType.None);
                EO_jeTabletRestore = (QuickAnim.EO_jeTabletRestore)EditorGUILayout.EnumPopup("  |    |  Tablet layout restore:", EO_jeTabletRestore);
                EO_jeThisObjectLayout = (QuickAnim.EO_jeThisObjectLayout)EditorGUILayout.EnumPopup("  |    |  This object layout:", EO_jeThisObjectLayout);
                EditorGUILayout.HelpBox(" |    |   Layout Change - Enable switch layout", MessageType.None);
                _target.jeLayoutChange = EditorGUILayout.Toggle("  |    |  Layout change", _target.jeLayoutChange);
                if (_target.jeLayoutChange)
                {
                   _target.jeLayoutChangeBindButtonsToTablet = EditorGUILayoutArrays.GameObjectArrayField(AFS_jeLayoutChangeBindButtonsToTablet, _target.jeLayoutChangeBindButtonsToTablet,"               -  Size", "               -  GameObject"); 
                   _target.jeLayoutChangeBindButtonsToClassic = EditorGUILayoutArrays.GameObjectArrayField(AFS_jeLayoutChangeBindButtonsToClassic, _target.jeLayoutChangeBindButtonsToClassic,"               -  Size", "               -  GameObject"); 
                }
                EditorGUILayout.HelpBox(" |    |   Another GameObject's switch layout.\n         -  Array 0 - Array with another GameObject layout switch.\n         -  Array 1 - Array with layout number's another GameObjects.\n             -  0 - Classic.\n             -  1 - Tablet.", MessageType.None);
                _target.jeLayoutSwitchAnotherObjects = EditorGUILayoutArrays.GameObjectArrayField(AFS_jeLayoutSwitchAnotherObjects, _target.jeLayoutSwitchAnotherObjects, "           -  Size", "           -  GameObject");
                _target.jeLayoutSwitchAnotherObjectsLayout = EditorGUILayoutArrays.IntArrayField(AFS_jeLayoutSwitchAnotherObjectsLayout, _target.jeLayoutSwitchAnotherObjectsLayout, "           -  Size", "           -  GameObject");
                EditorGUILayout.HelpBox(" |    |   Write down always tablet or classic layout scenes here (string).", MessageType.None);
                _target.jeLayoutScenesAlwaysTablet = EditorGUILayoutArrays.StringArrayField(AFS_jeLayoutScenesAlwaysTablet, _target.jeLayoutScenesAlwaysTablet, "           -  Size", "           -  Scene");
                _target.jeLayoutScenesAlwaysClassic = EditorGUILayoutArrays.StringArrayField(AFS_jeLayoutScenesAlwaysClassic, _target.jeLayoutScenesAlwaysClassic, "           -  Size", "           -  Scene");
            }
            EditorGUILayout.HelpBox(" |   Anim if pref's - Play animation if \n |   value in preference == value in this array's.", MessageType.None);
            _target.jeAnimIfPrefs = EditorGUILayout. Toggle("  |  Play anim's if pref", _target.jeAnimIfPrefs);
            if(_target.jeAnimIfPrefs)
            {
                _target.jeAnimIfPrefsType = EditorGUILayoutArrays.StringArrayField(AFS_jsAnimIfPrefsType, _target.jeAnimIfPrefsType, "           -  Size", "           -  Value type");
                _target.jeAnimIfPrefsName = EditorGUILayoutArrays.StringArrayField(AFS_jsAnimIfPrefsName, _target.jeAnimIfPrefsName, "           -  Size", "           -  Pref name");
                _target.jeAnimIfPrefsValue = EditorGUILayoutArrays.StringArrayField(AFS_jsAnimIfPrefsValue, _target.jeAnimIfPrefsValue, "           -  Size", "           -  Value for start anim");
            }
            EditorGUILayout.HelpBox(" |   Wait before Join / Exit, seconds (float). Default: 0.0f", MessageType.None);
            _target.jeWaitBeforeJoin = EditorGUILayout.Toggle("  |  Wait before Join", _target.jeWaitBeforeJoin);
            if(_target.jeWaitBeforeJoin)
            {
                _target.jeWaitBeforeJoinSeconds = EditorGUILayout.FloatField("  |    |  Wait Seconds (float)", _target.jeWaitBeforeJoinSeconds);
            }
            _target.jeWaitBeforeExit = EditorGUILayout.Toggle("  |  Wait before Exit", _target.jeWaitBeforeExit);
            if(_target.jeWaitBeforeExit)
            {
                _target.jeWaitBeforeExitSeconds = EditorGUILayout.FloatField("  |    |  Wait Seconds (float)", _target.jeWaitBeforeExitSeconds);
            }
            EditorGUILayout.HelpBox(" |   Duration of Join / Exit anims, seconds (float). Default: 0.2f", MessageType.None);
            _target.jeDurationJoinSeconds = EditorGUILayout.FloatField("  |    |  Duration of Join", _target.jeDurationJoinSeconds);
            _target.jeDurationExitSeconds = EditorGUILayout.FloatField("  |    |  Duration of Exit", _target.jeDurationExitSeconds);
            EditorGUILayout.HelpBox(" |   Anim Value Name's: ", MessageType.None);
            _target.jeAVNJoin = EditorGUILayout.TextField("  |  Join: ", _target.jeAVNJoin);
            _target.jeAVNExit = EditorGUILayout.TextField("  |  Exit: ", _target.jeAVNExit);
            EditorGUILayout.HelpBox("\n", MessageType.None);
            
        }
        EditorGUILayout.HelpBox(" |   Anim Value Name's: ", MessageType.None);
        _target.aAVNInvisible = EditorGUILayout.TextField("Invisible: ", _target.aAVNInvisible);
        _target.aAVNVisible = EditorGUILayout.TextField("Visible: ", _target.aAVNVisible);
        enumReturnString();
    }
    public void enumReturnString()
    {
        if (EO_mmSpawnUseJEMM == QuickAnim.EO_mmSpawnUseJEMM.Max) { _target.mmSpawnUseJEMM = "Max"; } else if (EO_mmSpawnUseJEMM == QuickAnim.EO_mmSpawnUseJEMM.Min) { _target.mmSpawnUseJEMM = "Min"; }
        if (EO_jeTabletRestore == QuickAnim.EO_jeTabletRestore.DoNotRestore) { _target.jeTabletRestore = "DoNotRestore"; } else if (EO_jeTabletRestore == QuickAnim.EO_jeTabletRestore.RestoreFromReg) { _target.jeTabletRestore = "RestoreFromReg"; } else if (EO_jeTabletRestore == QuickAnim.EO_jeTabletRestore.RestoreFromPreviousScene) { _target.jeTabletRestore = "RestoreFromPreviousScene"; }
        if (EO_jeThisObjectLayout == QuickAnim.EO_jeThisObjectLayout.Classic) { _target.jeThisObjectLayout = "Classic"; } else if (EO_jeThisObjectLayout == QuickAnim.EO_jeThisObjectLayout.Tablet) { _target.jeThisObjectLayout = "Tablet"; }
    }
}
#endif
