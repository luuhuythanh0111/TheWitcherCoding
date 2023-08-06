using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UICanvas
{
    public void Restart()
    {
        UIManager.Instance.OpenUI<GamePlayUI>();
        LevelManager.Instance.ResetGame();
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }

    public void Home()
    {
        UIManager.Instance.OpenUI<MainMenuUI>();
        LevelManager.Instance.ResetGame();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        Close();
    }

    public void Continue()
    {
        UIManager.Instance.OpenUI<GamePlayUI>();
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }
}
