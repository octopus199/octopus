using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MartinSkinRestore : MonoBehaviour
{

    public Sprite MartinColor0;
    public Sprite[] MartinColor;

    public Sprite[] MartinGesture;

    public Sprite[] MartinClothes;

    public int maxArraysCount;

    public GameObject Martin;
    public GameObject Gesture;
    public GameObject Clothes;

    void Start()
    {
        int gesture = PlayerPrefs.GetInt("MartinGesture");
        int color = PlayerPrefs.GetInt("MartinColor");
        int clothes = PlayerPrefs.GetInt("MartinClothes");
        for (int i = 0; i != maxArraysCount; i++)
        {
            if (i != 0)
            {
                if (gesture == i)
                {
                    Gesture.GetComponent<Image>().sprite = MartinGesture[i - 1];
                }
                if (color == i)
                {
                    Martin.GetComponent<Image>().sprite = MartinColor[i - 1];
                }
                if (clothes == i)
                {
                    Clothes.GetComponent<Image>().sprite = MartinClothes[i - 1];
                }
            }
            else
            {
                if (color == 0)
                {
                    Martin.GetComponent<Image>().sprite = MartinColor0;
                    return;
                }
            }
        }
    }

}
