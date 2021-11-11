using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class WorldSelect : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("World");
    }
    // Create meta file for each world which contains world name.
    // For each folder in saves display name with a join button.
    // When creating new world enter seed (?) and name.
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
        }
    }
}