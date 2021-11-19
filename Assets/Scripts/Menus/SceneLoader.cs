using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject worldsMenu;

    public static void LoadGame()
    {
        SceneManager.LoadScene("World");
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
    // these functions should be moved to WorldSelect so SceneLoader can be static.
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

}