using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { MENU, GAME }

public delegate void OnStateChange();

public class GameManager
{
    protected GameManager() { }
    private static GameManager gameInstance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameStates gameState { get; private set; }
    public GameManager gameManager { get; set; }

    public static GameManager Instance
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

    }

    public void SetGameState(GameManager state)
    {
        this.gameManager = state;
        OnStateChange();
    }


}
