using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : UICanvas
{
    public void ChangeToGamePlay()
    {
        UIManager.Instance.OpenUI<CodingUI>();
        GameManager.Instance.ChangeState(GameState.CodingUI);
        Close();
    }

    public void ChangeToInventory()
    {
        UIManager.Instance.OpenUI<InventoryUI>();
        Close();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
