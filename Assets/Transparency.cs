using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Transparency : MonoBehaviour
{
    public GameObject[] childs;
    public Color color;
    public Color colorOld;


    void Start()
    {
        for (int i = 0; i != childs.Length; i++)
        {
            //UIVertex uiv = UIVertex.simpleVert;
            //var color32 = color;
            childs[i].GetComponent<Image>().color = color;
        }
        colorOld = color;
    }
    void Update()
    {
        if (colorOld != color)
        {
            for (int i = 0; i != childs.Length; i++)
            {
                childs[i].GetComponent<Image>().color = color;
            }
            colorOld = color;
        }
    }


}
