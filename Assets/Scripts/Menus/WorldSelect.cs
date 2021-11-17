using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class WorldSelect : MonoBehaviour
{
    public GameObject worldButtonPreset;
    public TextMeshProUGUI txtMesh;
    public GameObject Overlord;

    public void LoadGame()
    {
        // Change Serialization.worldName to load in other world
        //      gameObject.name;
        SceneManager.LoadScene("World");
    }
    // For each folder in saves display name with a join button
    // When creating new world enter seed (?) and name
    // Multiplayer start thing will be from in game menu?


    public void ListWorld()
    {
        if (Directory.Exists(Application.persistentDataPath + '/' + "saves"))
        {
            int startY = 250;
            string[] dirs = Directory.GetDirectories(Application.persistentDataPath + '/' + "saves");
            foreach (string dir in dirs)
            {
                String name = dir.Split('/')[dir.Split('/').Length-1].Split('\\')[1];
                GameObject button = Instantiate(worldButtonPreset, Vector3.zero, Quaternion.Euler(Vector3.zero), Overlord.transform);
                button.SetActive(true);
                button.GetComponent<RectTransform>().localPosition = new Vector3(0, startY, 0); // First one
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
                startY -= 35;
            }
            CreateButton();
        }
    }

    public void CreateButton()
    {
        
        // button.transform.GetChild(0).GetComponent<TextMesh>().text = "What";
    }
}