using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject worldsMenu;

    public void LoadGame()
    {
        SceneManager.LoadScene("World");
    }

    public void WorldMenu()
    {
        worldsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        worldsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}