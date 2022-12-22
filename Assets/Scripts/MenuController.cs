using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Button goButton;
    public TMP_InputField nameInput;
    public GameObject loginPanel;
    public GameObject highScorePanel;
    public GameObject usernamePanel;
    public TextMeshProUGUI usernameText;
    private bool readyToStart;

    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayFabName"))
        {
            var request = new LoginWithCustomIDRequest { CustomId = PlayerPrefs.GetString("PlayFabName"), CreateAccount = false};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginWithPlayFabNameSuccess, OnFailure);
        }
        else {
            highScorePanel.SetActive(false); 
            usernamePanel.SetActive(false);

            goButton.onClick.AddListener(GoButtonOnClick);
            nameInput.onValueChanged.AddListener(NameInputValueChanged);
            goButton.interactable = false;
        }
    }

	private void GoButtonOnClick()
    {
        Debug.Log("customid = " + nameInput.text);
		var request = new LoginWithCustomIDRequest { CustomId = nameInput.text, CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnFailure);
	}

    private void NameInputValueChanged(String value)
    {
        if(string.IsNullOrEmpty(value))
        {
            goButton.interactable = false;
        }
        else
        {
            goButton.interactable = true;
        }
	}


    private void OnLoginWithPlayFabNameSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in");
        LoginComplete(PlayerPrefs.GetString("PlayFabName"));
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in");

        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = nameInput.text};
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateDisplayNameSuccess, OnFailure);
    }

    private void OnUpdateDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        LoginComplete(nameInput.text);
    }

    private void LoginComplete(string username)
    {
        GameValues.Reset();
        GameValues.username = username;

        PlayerPrefs.SetString("PlayFabName", username);
        PlayerPrefs.Save();

        loginPanel.SetActive(false);
        highScorePanel.GetComponent<HighScoreController>().LoadHighScores(descriptionText);
        highScorePanel.SetActive(true);
        usernamePanel.GetComponent<UsernamePanelController>().SetUsername(username);
        usernamePanel.SetActive(true);
        readyToStart = true;
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.LogError("PlayFab API call failed: ");
        Debug.LogError(error.GenerateErrorReport());
        descriptionText.text = "Login failed, please try again later";
    }

    private void Update() 
    {
        if (readyToStart && Keyboard.current[Key.Enter].isPressed)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
