using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ReturnDiamonds : MonoBehaviour
{
    private GameObject inGameText;
    string textRegName;

    private SaveToDo todo;
    private int? diamonds_old = 0;

    public TextMeshProUGUI txt;

    void Update()
    {
        int diamonds = PlayerPrefs.GetInt("diamondValue");
        if (diamonds == diamonds_old || (diamonds == 0 & diamonds_old == null)) { }
        else 
        {
            txt.text = diamonds.ToString();
            diamonds_old = diamonds;
            if (diamonds == 0) { diamonds_old = null; }
            //Debug.Log(diamonds_old);
        }
        //Debug.Log('1');
    }

}
