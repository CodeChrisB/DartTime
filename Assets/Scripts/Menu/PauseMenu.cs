using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    
    private TargetSpawner ts;
    public TMP_Text countText;
    public bool isPaused = false;

    private void Start()
    {
        ts = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));
    }

 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
       
        isPaused = !isPaused;
        Menu.SetActive(isPaused);

        Cursor.lockState = !isPaused ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    public void CloseGame() => Application.Quit();
    public void MainMenu() => SceneManager.LoadScene("Menu");
}
