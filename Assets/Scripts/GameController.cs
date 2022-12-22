using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float startDelaySeconds = 2f;

    private PlayerController playerController;
    private List<EnemyController> enemyControllers;
    private HudController hudController;
    private float lastTime;
    private bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        hudController = GameObject.Find("HUD").GetComponent<HudController>();

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyControllers = new List<EnemyController>();
        foreach (var enemy in enemies)
        {
            enemyControllers.Add(enemy.GetComponent<EnemyController>());
        }
        SetActiveState(false);
        hudController.ShowLevel();
        Invoke("StartLevel", 2.0f);
    }

    void Update()
    {
        if (isGameActive) {
            GameValues.AddToElapsedTime(lastTime, Time.time);
            lastTime = Time.time;
        }
    }

    void StartLevel() {
        hudController.HideLevel();
        SetActiveState(true);
        lastTime = Time.time;
        isGameActive = true;
    }

    void SetActiveState(bool isActive)
    {
        playerController.enabled = isActive;
        foreach (var enemy in enemyControllers)
        {
            Debug.Log("activitating enemy: " + enemy);
            enemy.enabled = isActive;
        }
    }

    public void PlayerDead()
    {
        GameValues.lives = GameValues.lives - 1;
        if (GameValues.lives == 0) 
        {
            GameValues.lastResult = LastResult.Lost;
            SceneManager.LoadScene("End");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PlayerWon()
    {
        if (GameValues.level == 5)
        {
            GameValues.lastResult = LastResult.Won;
            SceneManager.LoadScene("End");
        }
        else
        {
            GameValues.level = GameValues.level + 1;
            SceneManager.LoadScene("Level" + GameValues.level);
        }
    }
}
