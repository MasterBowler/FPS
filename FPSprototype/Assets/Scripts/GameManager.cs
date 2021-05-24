using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    //bool gameWon = false;
    public int numberOfEnemies = 0;

    public void IncreaseEnemyCount()
    {
        numberOfEnemies++;
    }

    public void DecreaseEnemyCount()
    {
        numberOfEnemies--;
        if (numberOfEnemies == 0)
        {
            //gameWon = true;
            LoadNextLevel();
        }
            
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            Debug.Log("Game Over");
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
