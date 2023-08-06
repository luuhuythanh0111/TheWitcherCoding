using System.Windows.Forms;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    private void Start()
    {
        ChangeState(GameState.LoadingGame);
        UIManager.Instance.OpenUI<LoadingGameUI>();
    }

    public void ChangeState(GameState gameState)
    {
        HandleGameState(gameState);
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }

    private void HandleGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                SoundManager.Instance.ChangeMusicBackGround(false);
                return;
            case GameState.CodingUI:
                Time.timeScale = 0;
                return;
            case GameState.Gameplay:
                Time.timeScale = 1;
                LevelManager.Instance.OnInit();
                SoundManager.Instance.ChangeMusicBackGround(true);
                return;
            case GameState.Pause:
                Time.timeScale = 0;
                return;
            case GameState.Win:
                //UIManager.Instance.OpenUI<loseeUI>();

                LevelManager.Instance.ResetGame();
                return;
            case GameState.Lose:
                UIManager.Instance.CloseUI<CodingUI>();
                UIManager.Instance.CloseUI<GamePlayUI>();
                UIManager.Instance.OpenUI<LoseUI>();
                LevelManager.Instance.ResetGame();
                return;
        }
    }
}

public enum GameState {LoadingGame, MainMenu, CodingUI, Gameplay, Pause, Win, Lose }