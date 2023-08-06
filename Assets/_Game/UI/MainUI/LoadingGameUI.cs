using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGameUI : UICanvas
{
    public void ChangeToGamePlay()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenuUI>();
        Close();
    }
}
