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
    public TMP_Text buttonText;
    public GameObject Camera;
    public float TransitionTime = 1.5f;
    public void StartGame()
    {
        PlayerPrefs.SetString(PlayerKeys.USERNAME, text.text);
        SceneManager.LoadScene("Game");
    }

    public void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        PlayerPrefs.SetInt(PlayerKeys.LEVEL, 0);
        mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        if (text.text.Length >= 3)
        {
            button.interactable = true;
            buttonText.text = "Start Game";
        }
        else if(text.text.Length<3)
        {
            button.interactable = false;
            buttonText.text = "Enter your Name";

        }
    }

    public void OnDropdownValueChange(int value)
    {
        Debug.Log(value);
        PlayerPrefs.SetInt(PlayerKeys.LEVEL, value);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ToInformation()
    {
        LeanTween.rotate(Camera, new Vector3(0, 90, 0), TransitionTime);
        LeanTween.move(Camera, new Vector3(7.5f, 2, 0), TransitionTime);
    }

    public void ToMainView()
    {
        LeanTween.rotate(Camera, new Vector3(20, -110, 0), TransitionTime);
        LeanTween.move(Camera, new Vector3(5, 2, 0.25f), TransitionTime);

    }

    public void ToStats()
    {
        LeanTween.rotate(Camera, new Vector3(0,0,0), TransitionTime);
        LeanTween.move(Camera, new Vector3(6.69f, 2.36f, 3.6f), TransitionTime);
    }
}
