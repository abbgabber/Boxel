using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class WorldSelect : MonoBehaviour
{
    public GameObject worldButtonPreset;

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
            string[] dirs = Directory.GetDirectories(Application.persistentDataPath + '/' + "saves");
            foreach (string dir in dirs)
            {
                Debug.Log(dir);
            }
            CreateButton();
        }
    }

    public void CreateButton()
    {
        GameObject button = Instantiate(worldButtonPreset, Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject;
        button.gameObject.SetActive(true);
        // button.transform.GetChild(0).GetComponent<TextMesh>().text = "What";
    }
}