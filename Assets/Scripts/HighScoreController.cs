using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData
{
    public PlayerData(string name, int statValue)
    {
        Name = name;
        StatValue = statValue;
    }

    public string Name { get; }
    public int StatValue { get; }

    public override string ToString() => $"({Name}: {StatValue})";
}

public class HighScoreController : MonoBehaviour
{
    public int maxHighScores;
    public GameObject highScoreName;
    public GameObject highScoreNone;
    public GameObject highScoreTime;

    private TextMeshProUGUI descriptionText;

    void Start() 
    {
        highScoreName.SetActive(false);
        highScoreTime.SetActive(false);
    }

    public void LoadHighScores(TextMeshProUGUI description)
    {
        LoadHighScores(description, null);
    }

    public void LoadHighScores(TextMeshProUGUI description, PlayerData newEntry)
    {
        descriptionText = description;
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest {
            StatisticName = "Completion Time",
            StartPosition = 0,
            MaxResultsCount = 100
        }, result=> DisplayLeaderboard(result, newEntry), OnFailure);
    }

    private void DisplayLeaderboard(GetLeaderboardResult result)
    {
        DisplayLeaderboard(result, null);
    }

    private void DisplayLeaderboard(GetLeaderboardResult result, PlayerData newEntry)
    {
        descriptionText.text = "Press Enter key to start game";
        if (newEntry != null || result.Leaderboard.Count > 0)
        {
            highScoreNone.SetActive(false);
            highScoreName.SetActive(true);
            highScoreTime.SetActive(true);

            List<PlayerData> players = new List<PlayerData>();

            bool newEntryFound = false;
            foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
            {
                string name;
                if (string.IsNullOrEmpty(entry.DisplayName))
                {
                    name = entry.PlayFabId;
                }
                else
                {
                    name = entry.DisplayName;
                }
                // Check if newEntry would overwrite the current StatValue.
                PlayerData data;
                if (newEntry != null && String.Equals(name, newEntry.Name))
                {
                    Debug.Log("found entry");
                    data = new PlayerData(name, Math.Min(entry.StatValue, newEntry.StatValue));
                    newEntryFound = true;
                } 
                else
                {
                    data = new PlayerData(name, entry.StatValue);
                }
                Debug.Log(data);
                players.Add(data);
            }
            // Add additional entry if not already added.
            if (newEntry != null && !newEntryFound) {
                Debug.Log("extra: " + newEntry);
                players.Add(newEntry);
            }
            // Sort from smallest to largest.
            players.Sort((s1, s2) => s1.StatValue.CompareTo(s2.StatValue));

            highScoreName.GetComponent<TextMeshProUGUI>().text = "1. " + players[0].Name;
            highScoreTime.GetComponent<TextMeshProUGUI>().text = GameValues.MillisToString((int)players[0].StatValue);

            for (int i = 1; i < players.Count; i++)
            {
                var pos = highScoreName.transform.position + new Vector3(0, i * -40, 0);
                var clone = Instantiate(highScoreName, pos, Quaternion.identity, highScoreName.transform.parent) as GameObject;
                clone.GetComponent<TextMeshProUGUI>().text = string.Format("{0}. {1}", i + 1, players[i].Name);

                pos = highScoreTime.transform.position + new Vector3(0, i * -40, 0);
                clone = Instantiate(highScoreTime, pos, Quaternion.identity, highScoreTime.transform.parent) as GameObject;
                clone.GetComponent<TextMeshProUGUI>().text = GameValues.MillisToString(players[i].StatValue);
            }
        }
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.LogError("PlayFab API call failed: ");
        Debug.LogError(error.GenerateErrorReport());
        descriptionText.text = "Failed to load high scores";
    }
}
