using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject crosshair;
    public GameObject World;
    public SceneLoader sceneLoader;

    public void ResumeGame()
    {
        Modify.PauseGame(pauseMenu, crosshair);
    }
    public void SaveGame()
    {
        World.GetComponent<World>().SaveAllChunks(); // saves world data
        // ADD SO INV IS SAVED
    }

    public void ExitGame()
    {
        SaveGame();
        SceneLoader.QuitGame();
    }
}
