using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float startDelaySeconds = 2f;
    private PlayerController playerController;
    private List<EnemyController> enemyControllers;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyControllers = new List<EnemyController>();
        foreach (var enemy in enemies)
        {
            enemyControllers.Add(enemy.GetComponent<EnemyController>());
        }
        SetActiveState(false);
        Invoke("StartLevel", 2.0f);
    }

    void StartLevel() {
        SetActiveState(true);
    }

    void SetActiveState(bool isActive)
    {
        playerController.enabled = isActive;
        foreach (var enemy in enemyControllers)
        {
            enemy.enabled = isActive;
        }
    }

    public void PlayerDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerWon()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
