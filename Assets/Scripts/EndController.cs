using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{
    public int maxLevel;
    public GameObject wonText;
    public GameObject lostText;
    public GameObject resultsText;
    public TextMeshProUGUI descriptionText;
    public GameObject highScorePanel;
    public GameObject usernamePanel;

    private float startTime;
    private TextMeshProUGUI resultsTextMesh;
    private TimeSpan duration;

    // Start is called before the first frame update
    private void Start()
    {
        // Hacks
        /*GameValues.username = "Test6";
        GameValues.lives = 3;
        GameValues.level = 2;
        GameValues.lastResult = LastResult.Won;
        GameValues.startTime = Time.time;
        GameValues.endTime = Time.time + 24.122f;
        var request = new LoginWithCustomIDRequest { CustomId = GameValues.username, CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnFailure);
*/
        highScorePanel.SetActive(false);
        usernamePanel.GetComponent<UsernamePanelController>().SetUsername(GameValues.username);
        // highScorePanel.GetComponent<HighScoreController>().LoadHighScores(descriptionText);
        // highScorePanel.SetActive(true);

        LoadHighScores();

        switch (GameValues.lastResult)
        {
            case LastResult.Won:
                lostText.SetActive(false);
                SaveToLeaderboard();
                SetResultsText();
            break;
            case LastResult.Lost:
                wonText.SetActive(false);
                SetResultsText();
            break;
            default:
                wonText.SetActive(false);
                lostText.SetActive(false);
            break;
        }
    }

    private void SaveToLeaderboard()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Completion Time",
                    Value = GameValues.elapsedTimeMillis
                }
            }
        }, result=> OnStatisticsUpdated(result), OnFailure);

    }

    private void LoadHighScores()
    {
        if (GameValues.lastResult == LastResult.Won)
        {
            var data = new PlayerData(GameValues.username, GameValues.elapsedTimeMillis);
            highScorePanel.GetComponent<HighScoreController>().LoadHighScores(descriptionText, data);
        }
        else
        {
            highScorePanel.GetComponent<HighScoreController>().LoadHighScores(descriptionText);
        }
        highScorePanel.SetActive(true);
    }
/*
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in");
        SaveToLeaderboard();
    }
*/
    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult) {
        Debug.Log("Successfully submitted high score");
    }

    private void OnFailure(PlayFabError error){
        Debug.LogError("PlayFab API call failed: ");
        Debug.LogError(error.GenerateErrorReport());
        descriptionText.text = "Failed to save high score";
    }

    private void SetResultsText()
    {
        resultsText.GetComponent<TMPro.TextMeshProUGUI>().text = GenerateResultsText();
    }

    private string GenerateResultsText()
    {
        int levelsCompleted;
        if (GameValues.lastResult == LastResult.Won)
        {
            return string.Format("Completed all levels in {0}", GameValues.ElapsedTimeMillisString());
        }
        else
        {
            levelsCompleted = GameValues.level - 1;
        }

        switch (levelsCompleted)
        {
            case 0:
                return string.Format("Completed no levels");
            case 1:
                return string.Format("Completed 1 level"); 
            default:
                return string.Format("Completed {0} levels", levelsCompleted);
        }
    }

    // Update is called once per frame
    private void Update() 
    {
        if (Keyboard.current[Key.Enter].isPressed)
        {
            GameValues.Reset();
            SceneManager.LoadScene("Level1");
        }
    }
}