using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, GAME, DEATH }

public delegate void OnStateChange();

public class GameManager : MonoBehaviour
{
    protected GameManager() { }
    private static GameManager gameInstance = null;
    public event OnStateChange OnStateChange;
    public GameState gameState { get; private set; }
    public GameManager gameManager { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

/*    public static GameManager Instance
    {
        get
        {
            if (GameManager.gameInstance == null)
            {
                DontDestroyOnLoad(GameManager.gameInstance);
                GameManager.gameInstance = new GameManager();
            }
            return GameManager.gameInstance;
        }

    }*/

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        //OnStateChange();
    }

    public void StartGame()
    {
        SetGameState(GameState.GAME);
        SceneManager.LoadScene(1);
    }

    public void PlayerDeath()
    {
        SetGameState(GameState.DEATH);
    }

    public void GoToMenu()
    {
        SetGameState(GameState.MENU);
    }


}
