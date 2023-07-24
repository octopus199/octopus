using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParentProtect : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public TMP_InputField txtIn;
    public GameObject NextButton;
    private int stade = 0;
    private string tempP;
    
    void Start()
    {
        onStart();
    }

    private void onStart()
    {
        string enterType = PlayerPrefs.GetString("PPOperationType");
        if (enterType == "Create")
        {
            txt.text = "Придумайте пароль:";
            txtIn.text = "";
        }
        else
        {
            txt.text = "Введите пароль:";
            txtIn.text = "";
        }
        NextButton.GetComponent<Button>().onClick.AddListener(delegate { onEnter(enterType); });
    }
    private void onEnter(string enterType)
    {
        string input = txtIn.text;
        if (enterType == "Create")
        {
            if (stade == 0)
            {
                tempP = input;
                txt.text = "Повторите пароль:";
                txtIn.text = "";
                stade = 1;
            }
            else if (stade == 1)
            {
                if (input == tempP)
                {
                    tempP = "";
                    PasswordCreate(input);
                    PlayerPrefs.SetInt("PPActive", 1);
                    PlayerPrefs.Save();
                    OpenScene openScene = GetComponent<OpenScene>();
                    openScene.sceneName = "Main";
                    openScene.onClick();
                }
                else
                {
                    txt.text = "Неверный пароль! Попробуйте ещё раз:";
                    txtIn.text = "";
                }
            }
        }
        else if (enterType == "Protect")
        {
            if (input == getTruePassword())
            {
                OpenScene openScene = GetComponent<OpenScene>();
                openScene.sceneName = "MainVerify";
                openScene.onClick();
                PlayerPrefs.SetInt("InPP", 1);
                PlayerPrefs.Save();
            }
            else
            {
                txt.text = "Неверный пароль! Попробуйте ещё раз:";
                txtIn.text = "";
            }
        }
        else if (enterType == "Distable")
        {
            if (input == getTruePassword())
            {
                PlayerPrefs.SetInt("PPActive", 0);
                PlayerPrefs.Save();
                OpenScene openScene = GetComponent<OpenScene>();
                openScene.sceneName = "Main";
                openScene.onClick();
            }
        }
    }

    private string getTruePassword()
    {
        if (File.Exists(Application.persistentDataPath + "/OctopusFile001.dat"))
        {
            BinaryFormatter boolF = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/OctopusFile001.dat", FileMode.Open);
            PasswordProtect data = (PasswordProtect)boolF.Deserialize(file);
            file.Close();
            string password = data.password;

            //int bin = Int32.Parse(password);
            //Debug.Log("bin " + bin);
            //byte[] binString = BitConverter.GetBytes(bin);
            //Array.Reverse(binString);
            //Debug.Log("binString " + binString);
            //int newBin = Int32.Parse(System.Text.Encoding.UTF8.GetString(binString));
            //Debug.Log("newBin " + newBin);
            //int binaryInt = newBin / 4754037;
            //Debug.Log("binaryInt " + binaryInt);
            //byte[] binaryString = BitConverter.GetBytes(binaryInt);
            //Array.Reverse(binaryString);
            //Debug.Log("binaryString " + binaryString);
            //string result = System.Text.Encoding.UTF8.GetString(binaryString);
            //Debug.Log("result " + result);

            Debug.Log("Game data loaded!");
            return password;
        }
        else if (File.Exists(Application.persistentDataPath + "/OctopusFile003.dat"))
        {
            BinaryFormatter boolF = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/OctopusFile003.dat", FileMode.Open);
            PasswordProtect data = (PasswordProtect)boolF.Deserialize(file);
            file.Close();
            string password = data.password;

            //int bin = Int32.Parse(password);
            //byte[] binString = BitConverter.GetBytes(bin);
            //int newBin = Int32.Parse(System.Text.Encoding.UTF8.GetString(binString));
            //int binaryInt = newBin / 4754037;
            //byte[] binaryString = BitConverter.GetBytes(binaryInt);
            //string result = System.Text.Encoding.UTF8.GetString(binaryString);

            Debug.Log("Game data loaded!");

            FileStream fileA = File.Create(Application.persistentDataPath + "/OctopusFile001.dat");
            PasswordProtect dataA = new PasswordProtect();
            dataA.password = password;
            boolF.Serialize(file, data);
            fileA.Close();

            return password;
        }
        else
        {
            Debug.LogError("File not found!");
            return "";
        }
    }
    private void PasswordCreate(string newPass)
    {
        BinaryFormatter boolF = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/OctopusFile001.dat");
        FileStream fileB = File.Create(Application.persistentDataPath + "/OctopusFile003.dat");
        PasswordProtect data = new PasswordProtect();
        //byte[] binaryString = Encoding.UTF8.GetBytes(newPass);
        //Debug.Log("binaryString " + binaryString);
        //int binaryInt = BitConverter.ToInt32(binaryString, 0);
        //Debug.Log("binaryInt " + binaryInt);
        //int newBin = binaryInt * 4754037;
        //Debug.Log("newBin " + newBin);
        //byte[] binString = Encoding.UTF8.GetBytes(newBin.ToString());
        //Debug.Log("binString " + binString);
        //int bin = BitConverter.ToInt32(binString);
        //Debug.Log("bin " + bin);
        data.password = newPass;
        boolF.Serialize(file, data);
        boolF.Serialize(fileB, data);
        file.Close();
    }
    private void PasswordDelite()
    {
        if (File.Exists(Application.persistentDataPath + "/OctopusFile001.dat"))
        {
            File.Delete(Application.persistentDataPath + "/OctopusFile001.dat");
            File.Delete(Application.persistentDataPath + "/OctopusFile003.dat");
            Debug.Log("Game data deleted!");
        }
        else
        {
            Debug.LogError("File not found!");
        }
    }
}

[Serializable]
class PasswordProtect
{
    public string password;
}
