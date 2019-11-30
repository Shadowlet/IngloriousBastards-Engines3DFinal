using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, GAME, DEATH }

public delegate void OnStateChange();

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public AbilityUI abilityUI;

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
        Cursor.visible = false;
        abilityUI.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
    }

    public void PlayerDeath()
    {
        SetGameState(GameState.DEATH);
        Cursor.visible = true;
        scoreManager.CalculateScore();
        scoreManager.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        abilityUI.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }

    public void GoToMenu()
    {
        SetGameState(GameState.MENU);

        SceneManager.LoadScene(0);
    }


}
