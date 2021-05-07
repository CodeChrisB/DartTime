using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public Button button;
    public TMP_InputField mainInputField;
    public TMP_Text text;
    public void StartGame()
    {
        PlayerPrefs.SetString(PlayerKeys.USERNAME, text.text);
        SceneManager.LoadScene("Game");
    }






    public void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        if (text.text.Length >= 3)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
