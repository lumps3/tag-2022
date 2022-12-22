using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public GameObject levelText;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI elapsedTimeText;

    private GameObject life1;
    private GameObject life2;
    private GameObject life3;

    // Start is called before the first frame update
    void Start()
    {
        life1 = GameObject.Find("Life1");
        life2 = GameObject.Find("Life2");
        life3 = GameObject.Find("Life3");
        SetLives(GameValues.lives);

        usernameText.text = GameValues.username;

        HideLevel();
    }

    // Update is called once per frame
    void Update()
    {
       elapsedTimeText.text = TimeSpan.FromMilliseconds(GameValues.elapsedTimeMillis).ToString(@"mm\:ss\.f");
    }

    public void ShowLevel()
    {
        levelText.SetActive(true);
        var levelTextMesh = levelText.GetComponent<TextMeshProUGUI>();
        levelTextMesh.text = "Level " + GameValues.level;
    }

    public void HideLevel()
    {
        levelText.SetActive(false);
    }

    public void SetLives(int lives) {
        if (lives == 3)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        else if (lives == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        else if (lives == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        else
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
    }
}
