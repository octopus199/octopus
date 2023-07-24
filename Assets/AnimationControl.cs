using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControl : MonoBehaviour
{
    //public string onJoinAnimName;

    public bool TabletLayoutConnect;
    public bool restoreTabletLayout;
    public bool invertLayout;
    public bool checkLayoutFromReg;
    //public bool antiMainInSort;
    public string undesirableExitSceneID;
    public bool progressTabControl;
    public bool openSceneControl;
    public bool previousSceneIDControl;
    //public bool distablePreviousSceneIDInExit;
    public bool progressTabIDSortDistable;
    public bool previousSceneIDExitDistable;
    public int sceneTempID;
    public bool restoreHaveBack;
    public bool onlyExit;
    public bool onlyJoin;
    public bool alwaysSkipExit;
    public bool alwaysSkipJoin;
    public bool animationIfPrefs;
    public string[] prefsType;
    public string[] prefsName;
    public string[] activeIf;
    public bool waitAfterPrefsExit;
    public bool waitBeforeJoin;
    public bool turnOffAnimatorAfterJoin;
    public bool doLayoutChange;

    public bool toMin;
    public int spawnOperation;
    public GameObject[] minBindButton;
    public int minOperation;
    public GameObject[] maxBindButton;
    public int maxOperation;
    public GameObject[] activeIfMin;
    public GameObject[] activeIfMax;
    public GameObject[] invisibleOnMin;
    public GameObject[] invisibleOnMax;
    public GameObject[] anotherToMin;
    public bool[] anotherToMinInverse;
    //public string thisSceneID;

    private Animator anim;
    private int isTablet = 0;
    private string sceneID = "";
    private string previousSceneID = "";
    private string[] progress = { "Progress", "Progress3Month", "ProgressMonth", "ProgressWeek" };
    private int ifOnExit = 0;

    void Start()
    {
        if (PlayerPrefs.GetInt("animDistable") == 0)
        {
            if (waitBeforeJoin)
            {
                StartCoroutine(waitJoin());
            }
            onJoinControl();
        }
        else
        {
            Debug.Log("Animations Distabled!");
            anim = GetComponent<Animator>();
            anim.SetBool("SkipJoin", true);
            if (toMin)
            {
                for (int i = 0; i != invisibleOnMin.Length & spawnOperation == 0; i++)
                {
                    invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                    invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                }
                for (int i = 0; i != invisibleOnMax.Length & spawnOperation == 1; i++)
                {
                    invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                    invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                }
                for (int i = 0; i != activeIfMin.Length & spawnOperation == 0; i++)
                {
                    activeIfMin[i].SetActive(false);
                }
                for (int i = 0; i != activeIfMax.Length & spawnOperation == 1; i++)
                {
                    activeIfMax[i].SetActive(false);
                }
                for (int i = 0; i != minBindButton.Length; i++)
                {
                    minBindButton[i].GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(ToMinOperationIfAnimDistabled(anim, false)); });
                    Debug.Log("ToMin " + i + " connected!");
                }
                for (int i = 0; i != maxBindButton.Length; i++)
                {
                    maxBindButton[i].GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(ToMinOperationIfAnimDistabled(anim, true)); });
                    Debug.Log("ToMax " + i + " connected!");
                }
                StartCoroutine(ATM(true));
            }
            if (turnOffAnimatorAfterJoin)
            {
                anim.enabled = false;
            }
        }
    }
    void onJoinControl()
    {
        anim = GetComponent<Animator>();
        isTablet = PlayerPrefs.GetInt("sendTabletLayout");
        if (checkLayoutFromReg == true)
        {
            isTablet = PlayerPrefs.GetInt("layoutID");
        }
        if (openSceneControl)
        {
            sceneID = PlayerPrefs.GetString("tempSceneID");
        }
        if (previousSceneIDControl)
        {
            previousSceneID = PlayerPrefs.GetString("tempSceneID" + sceneTempID);
        }
        //gameObject.SetActive(false);
        //if (onJoin == true)
        //{
        //anim.Play(onJoinAnimName);
        //anim.SetBool("Pause", true);
        //}
        if (toMin)
        {
            if (spawnOperation == 0) { SpawnMin(anim); }
            for (int i = 0; i != minBindButton.Length; i++)
            {
                minBindButton[i].GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(ToMinOperation(anim, false)); });
                Debug.Log("ToMin " + i + " connected!");
            }
            for (int i = 0; i != maxBindButton.Length; i++)
            {
                maxBindButton[i].GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(ToMinOperation(anim, true)); });
                Debug.Log("ToMax " + i + " connected!");
            }
            if (spawnOperation == 1)
            {
                StartCoroutine(ATM(true));
                for (int i = 0; i != activeIfMin.Length; i++)
                {
                    activeIfMin[i].SetActive(false);
                }
                for (int i = 0; i != invisibleOnMax.Length; i++)
                {
                    invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                    invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                    invisibleOnMin[i].GetComponent<Animator>().SetBool("Join", false);
                }
            }
        }

        if (((previousSceneID == "ToDo" & isTablet == 1) || previousSceneID == "Store" || previousSceneID == "ParentProtection" || previousSceneID == "MainVerify") & TabletLayoutConnect)
        {
            //gameObject.SetActive(true);
            Debug.Log("Tablet Returned");
            anim.SetBool("SkipJoin", true);
            //Debug.Log("return");
        }
        else if (onlyJoin)
        {
            anim.SetBool("Join", true);
            //gameObject.SetActive(true);
        }
        else if (alwaysSkipJoin)
        {
            anim.SetBool("SkipJoin", true);
            //gameObject.SetActive(true);
        }
        else if (restoreHaveBack & (previousSceneID == "Main"))
        {
            anim.SetBool("Join", true);
        }
        else if (restoreTabletLayout)
        {

            Debug.Log(sceneID);
            int forReturn = 0;
            for (int i = 0; i != 4; i++)
            {
                if (((isTablet == 1 & !invertLayout) || (isTablet == 0 & invertLayout) || sceneID == progress[i]) & restoreTabletLayout & sceneID != "Store" || (previousSceneIDControl & previousSceneID == progress[i]))
                {
                    //gameObject.SetActive(true);
                    Debug.Log("Tablet Returned");
                    anim.SetBool("SkipJoin", true);
                    forReturn = 1;
                    //Debug.Log("return");
                }
                Debug.Log(i);
            }
            //if (forReturn == 4 || forReturn == 1)
            //{
            //    
            //    Debug.Log(forReturn);
            //}
            if (forReturn == 3 || forReturn == 0)
            {
                anim.SetBool("Join", true);
                Debug.Log(forReturn);
            }
        }
        else if (progressTabControl)
        {
            Debug.Log(progress);
            int forReturn = 0;
            for (int i = 0; i != 4; i++)
            {
                if ((progressTabControl & sceneID != progress[i] & !progressTabIDSortDistable) || (previousSceneIDControl & previousSceneID != progress[i]))
                {
                    //gameObject.SetActive(true);
                    Debug.Log("Tablet Returned");
                    forReturn++;
                    //Debug.Log("return");
                }
            }
            if (forReturn == 4)
            {
                anim.SetBool("SkipJoin", true);
                Debug.Log(forReturn);
            }
            else if (forReturn == 3)
            {
                anim.SetBool("Join", true);
                Debug.Log(forReturn);
            }
        }
        else
        {
            anim.SetBool("SkipJoin", true);
        }

        if (animationIfPrefs)
        {
            if (IsActiveIfReg.Control(prefsType, prefsName, activeIf))
            {
                anim.SetBool("Join", true);
            }
        }
        if (turnOffAnimatorAfterJoin)
        {
            StartCoroutine(TurnOffAnimator());
        }
    }

    void Update()
    {
        int onExit = PlayerPrefs.GetInt("onExit");
        int onChange = PlayerPrefs.GetInt("onChangeLayout");
        if ((onExit == 1 || onChange == 1 & doLayoutChange) & ifOnExit == 0 & PlayerPrefs.GetInt("animDistable") == 0)
        {
            if (openSceneControl)
            {
                sceneID = PlayerPrefs.GetString("tempSceneID");
            }
            Debug.Log(sceneID);
            int elseif2R = 0;
            if (((sceneID == "ToDo" & isTablet == 1) || sceneID == "Store" || sceneID == "ParentProtection" || sceneID == "MainVerify") & TabletLayoutConnect)
            {
                ifOnExit++;
                return;
            }
            else if (restoreHaveBack & (sceneID == "Main"))
            {
                anim.SetBool("Exit", true);
                Debug.Log(anim + " Exit");
            }
            else
            {
                for (int i = 0; i != 4; i++)
                {
                    if (onlyExit & !alwaysSkipExit)
                    {
                        anim.SetBool("Exit", true);
                        Debug.Log(anim + " Exit");
                    }
                    else if ((((isTablet == 0 & !invertLayout) || (invertLayout & isTablet == 1) || sceneID == "Store") & (sceneID != progress[i]) || sceneID == "Store") & !progressTabControl & !alwaysSkipExit & sceneID != undesirableExitSceneID)
                    {
                        elseif2R++;
                    }
                    else if ((progressTabControl & sceneID == progress[i]) || (progressTabControl & previousSceneIDControl & previousSceneID == progress[i] & !previousSceneIDExitDistable) & !alwaysSkipExit & sceneID != undesirableExitSceneID)
                    {
                        anim.SetBool("Exit", true);
                        Debug.Log(anim + " Exit");
                    }
                }
                Debug.Log(elseif2R + sceneID);
                if (elseif2R == 4 || elseif2R == 1)
                {
                    anim.SetBool("Exit", true);
                    Debug.Log(anim + " Exit");
                }
            }
            ifOnExit++;
        }
        int clear = PlayerPrefs.GetInt("animateClearAll");
        if (clear == 1)
        {
            anim.enabled = true;
            anim.SetBool("Exit", false);
            anim.SetBool("Back", false);
            anim.SetBool("Join", false);
            anim.SetBool("FastExit", false);
            anim.SetBool("SkipJoin", false);
            anim.SetBool("Max", false);
            anim.SetBool("Min", false);
            ifOnExit = 0;
        }
        if (animationIfPrefs)
        {
            if (IsActiveIfReg.Control(prefsType, prefsName, activeIf) & PlayerPrefs.GetInt("animDistable") == 0)
            {
                if (((previousSceneID == "ToDo" & isTablet == 1) || previousSceneID == "Store" || previousSceneID == "ParentProtection" || previousSceneID == "MainVerify") & TabletLayoutConnect)
                {
                    anim.SetBool("SkipJoin", true);
                }
                else
                {
                    anim.SetBool("Join", true);
                }
                if (turnOffAnimatorAfterJoin)
                {
                    StartCoroutine(TurnOffAnimator());
                }
            }
            else if (!IsActiveIfReg.Control(prefsType, prefsName, activeIf) & PlayerPrefs.GetInt("animDistable") == 0)
            {
                anim.SetBool("Join", false);
                anim.SetBool("SkipJoin", false);
                if (!waitAfterPrefsExit)
                {
                    anim.SetBool("Exit", true);
                    anim.SetBool("FastExit", true);
                    StartCoroutine(ClearAll());
                }
                else
                {
                    StartCoroutine(WaitExit());
                }
            }
            else if (IsActiveIfReg.Control(prefsType, prefsName, activeIf) & PlayerPrefs.GetInt("animDistable") == 1)
            {
                anim.SetBool("SkipJoin", true);
                if (turnOffAnimatorAfterJoin)
                {
                    StartCoroutine(TurnOffAnimatorInDistable());
                }
            }
            else if (!IsActiveIfReg.Control(prefsType, prefsName, activeIf) & PlayerPrefs.GetInt("animDistable") == 1)
            {
                anim.SetBool("SkipJoin", false);
                anim.SetBool("Join", false);
                anim.SetBool("Back", true);
                StartCoroutine(ClearAll());

            }
        }

    }
    IEnumerator SpawnMin(Animator anim)
    {
        for (int i = 0; i != activeIfMin.Length; i++)
        {
            activeIfMin[i].SetActive(true);
        }
        for (int i = 0; i != activeIfMax.Length; i++)
        {
            activeIfMax[i].SetActive(false);
        }
        for (int i = 0; i != invisibleOnMin.Length; i++)
        {
            invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", true);
            invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", false);
            invisibleOnMax[i].GetComponent<Animator>().SetBool("Join", false);
        }
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Min", true);
        anim.SetBool("SkipJoin", false);
        anim.SetBool("Join", false);
        StartCoroutine(ATM(false));
    }
    IEnumerator ToMinOperation(Animator anim, bool anti)
    {
        if (!anti)
        {
            Debug.Log("ToMin");
        }
        else
        {
            Debug.Log("ToMax");
        }
        if (!anti & minOperation == 0 || anti & maxOperation == 0)
        {
            anim.SetBool("Min", true);
            anim.SetBool("SkipJoin", false);
            anim.SetBool("Join", false);
            for (int i = 0; i != activeIfMin.Length; i++)
            {
                activeIfMin[i].SetActive(true);
            }
            for (int i = 0; i != activeIfMax.Length; i++)
            {
                activeIfMax[i].SetActive(false);
            }
            StartCoroutine(ATM(false));
            for (int i = 0; i != invisibleOnMax.Length; i++)
            {
                invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", false);
                invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", true);
                Debug.Log(i + " to visible (ToMin)");
            }
            for (int i = 0; i != invisibleOnMin.Length; i++)
            {
                invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                invisibleOnMin[i].GetComponent<Animator>().SetBool("Join", false);
                invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                Debug.Log(i + " to invisible (ToMin)");
            }
        }
        else
        {
            anim.SetBool("Min", false);
            anim.SetBool("Max", true);
            for (int i = 0; i != activeIfMin.Length; i++)
            {
                activeIfMin[i].SetActive(false);
            }
            for (int i = 0; i != activeIfMax.Length; i++)
            {
                activeIfMax[i].SetActive(true);
            }
            StartCoroutine(ATM(true));
            for (int i = 0; i != invisibleOnMax.Length; i++)
            {
                invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                invisibleOnMax[i].GetComponent<Animator>().SetBool("Join", false);
                invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                Debug.Log(i + " to invisible (ToMax)");
            }
            for (int i = 0; i != invisibleOnMin.Length; i++)
            {
                invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", false);
                invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", true);
                Debug.Log(i + " to visible (ToMax)");
            }
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("Max", false);
            for (int i = 0; i != anotherToMin.Length; i++)
            {
                Animator localAnim = anotherToMin[i].GetComponent<Animator>();
                localAnim.SetBool("Max", false);
            }

        }
    }
    IEnumerator ToMinOperationIfAnimDistabled(Animator anim, bool anti)
    {
        if (!anti)
        {
            Debug.Log("ToMin");
        }
        else
        {
            Debug.Log("ToMax");
        }
        if (!anti & minOperation == 0 || anti & maxOperation == 0)
        {
            anim.SetBool("MinNoA", true);
            anim.SetBool("SkipJoin", false);
            for (int i = 0; i != activeIfMin.Length; i++)
            {
                activeIfMin[i].SetActive(true);
            }
            for (int i = 0; i != activeIfMax.Length; i++)
            {
                activeIfMax[i].SetActive(false);
            }
            StartCoroutine(ATM(false));
            for (int i = 0; i != invisibleOnMax.Length; i++)
            {
                invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", false);
                invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", true);
                Debug.Log(i + " to visible (ToMin)");
            }
            for (int i = 0; i != invisibleOnMin.Length; i++)
            {
                invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                Debug.Log(i + " to invisible (ToMin)");
            }
        }
        else
        {
            anim.SetBool("MinNoA", false);
            anim.SetBool("MaxNoA", true);
            for (int i = 0; i != activeIfMin.Length; i++)
            {
                activeIfMin[i].SetActive(false);
            }
            for (int i = 0; i != activeIfMax.Length; i++)
            {
                activeIfMax[i].SetActive(true);
            }
            StartCoroutine(ATM(true));
            for (int i = 0; i != invisibleOnMax.Length; i++)
            {
                invisibleOnMax[i].GetComponent<Animator>().SetBool("SkipJoin", false);
                invisibleOnMax[i].GetComponent<Animator>().SetBool("StayInvisible", true);
                Debug.Log(i + " to invisible (ToMax)");
            }
            for (int i = 0; i != invisibleOnMin.Length; i++)
            {
                invisibleOnMin[i].GetComponent<Animator>().SetBool("StayInvisible", false);
                invisibleOnMin[i].GetComponent<Animator>().SetBool("SkipJoin", true);
                Debug.Log(i + " to visible (ToMax)");
            }
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("MaxNoA", false);
            for (int i = 0; i != anotherToMin.Length; i++)
            {
                Animator localAnim = anotherToMin[i].GetComponent<Animator>();
                localAnim.SetBool("MaxNoA", false);
            }

        }
    }

    IEnumerator ATM(bool inverse)
    {
        for (int i = 0; i != anotherToMin.Length; i++)
        {
            Animator localAnim = anotherToMin[i].GetComponent<Animator>();
            if (!anotherToMinInverse[i] & !inverse || anotherToMinInverse[i] & inverse)
            {
                localAnim.SetBool("Min", true);
                localAnim.SetBool("SkipJoin", false);
                localAnim.SetBool("Join", false);
            }
            else
            {
                localAnim.SetBool("Min", false);
                localAnim.SetBool("Max", true);
                yield return new WaitForSeconds(0.1f);
                localAnim.SetBool("Max", false);
            }
        }
    }

    IEnumerator waitJoin()
    {
        yield return new WaitForSeconds(0.10f);
        onJoinControl();
    }
    IEnumerator TurnOffAnimator()
    {
        yield return new WaitForSeconds(0.74f);
        anim.enabled = false;
    }
    IEnumerator TurnOffAnimatorInDistable()
    {
        yield return new WaitForSeconds(0.2f);
        anim.enabled = false;
    }
    IEnumerator WaitExit()
    {
        yield return new WaitForSeconds(0.27f);
        anim.SetBool("Exit", true);
        anim.SetBool("Back", true);
        yield return new WaitForSeconds(0.10f);
        anim.enabled = true;
        anim.SetBool("Exit", false);
        anim.SetBool("Join", false);
        anim.SetBool("Back", false);
        anim.SetBool("SkipJoin", false);
        anim.SetBool("Max", false);
        anim.SetBool("Min", false);
        ifOnExit = 0;
    }
    IEnumerator ClearAll()
    {
        yield return new WaitForSeconds(0.37f);
        anim.enabled = true;
        anim.SetBool("Exit", false);
        anim.SetBool("Join", false);
        anim.SetBool("FastExit", false);
        anim.SetBool("Back", false);
        anim.SetBool("SkipJoin", false);
        anim.SetBool("Max", false);
        anim.SetBool("Min", false);
        ifOnExit = 0;
    }

}
