using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Game;
    public GameObject Menu;
    public Camera GameCam;
    public Camera MenuCam;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        bool gameState = !Game.activeSelf;
        Game.SetActive(gameState);
        GameCam.gameObject.SetActive(gameState);

        Menu.SetActive(!gameState);
        MenuCam.gameObject.SetActive(!gameState);

        //set cursor state
        Cursor.lockState = gameState ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    public void CloseGame() => Application.Quit();
    public void MainMenu() => SceneManager.LoadScene("Menu");
}
