using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : UICanvas
{
    public void HomeButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.ResetGame();
        UIManager.Instance.OpenUI<MainMenuUI>();
        Close();
    }

    public void PlayAgainButton()
    {
        LevelManager.Instance.ResetGame();
        GameManager.Instance.ChangeState(GameState.MainMenu);

        UIManager.Instance.OpenUI<GamePlayUI>();
        Close();
    }
}
