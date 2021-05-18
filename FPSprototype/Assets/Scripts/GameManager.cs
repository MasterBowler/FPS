using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    //what level the game is currently in
    //load and unload game levels
    //keep track of game state
    //generate other persistent system


    bool gameOver = false;

    //private static GameManager instance;

    private string _currentLevelName = string.Empty;

    List<AsyncOperation> _loadOperations;
    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            Debug.LogError("There should be only one game manager instance.");
        }
    }
    */
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        //_loadOperations = new List<AsyncOperation>();

        //LoadLevel("Main");
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

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            // dispatch message
            // transition between scenes
        }
        Debug.Log("Load Complete.");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level" + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }
}
