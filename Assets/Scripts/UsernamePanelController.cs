using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsernamePanelController : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public Button changeButton;

    // Start is called before the first frame update
    void Start()
    {
        changeButton.onClick.AddListener(ChangeButtonOnClick);
    }

    private void ChangeButtonOnClick()
    {
		PlayerPrefs.DeleteKey("PlayFabName");
        SceneManager.LoadScene("MainMenu");
	}

    public void SetUsername(string username)
    {
        usernameText.text = "Name: " + username;
    }
}
