using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class WorldSelect : MonoBehaviour
{
    [Header("List Worlds")]
    public GameObject worldButtonPreset;
    public TextMeshProUGUI txtMesh;
    public GameObject Overlord;
    [Header("Start Y - World Buttons")]
    public int startY = 200;
    [Header("Create New World")]
    public GameObject createNewWorldPanel;
    public GameObject listWorldsPanel;
    public GameObject inputField;

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
                bool exists = false;
                String name = dir.Split('/')[dir.Split('/').Length - 1].Split('\\')[1];
                foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
                {
                    if (button.gameObject.name == name)
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    CreateButton(name);
                }
            }
        }
    }

    public void CreateButton(String name)
    {
        GameObject button = Instantiate(worldButtonPreset, Vector3.zero, Quaternion.Euler(Vector3.zero), Overlord.transform);
        button.SetActive(true);
        button.gameObject.name = name;
        button.GetComponent<RectTransform>().localPosition = new Vector3(0, startY, 0); // First one
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        startY -= 35;
    }

    public void HideButtons()
    {
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
        {
            button.gameObject.SetActive(true);
        }
    }

    public void ShowCreateNewWorld()
    {
        HideButtons();
        listWorldsPanel.SetActive(false);
        createNewWorldPanel.SetActive(true);
    }
    
    public void BackToListWorlds()
    {
        createNewWorldPanel.SetActive(false);
        listWorldsPanel.SetActive(true);
        ShowButtons();
    }

    public void CreateNewWorld()
    {
        String newWorldName = inputField.GetComponent<TMP_InputField>().text;
        Serialization.worldName = newWorldName;
        // Change Scene To World
    }

    public void LoadWorld()
    {
        Serialization.worldName = EventSystem.current.currentSelectedGameObject.name;
    }
}