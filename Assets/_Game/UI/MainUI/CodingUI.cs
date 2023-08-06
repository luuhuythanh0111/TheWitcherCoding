using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingUI : UICanvas
{
    public int NumberOfTab;

    public PlayerCodeInput playerCodeInput;

    public Text errorTxt;

    private void Start()
    {
        this.NumberOfTab = 0;
        playerCodeInput.gameObjectAttachCode = LevelManager.Instance.player.playerAttackment;
        UIManager.Instance.codingUI = this;
    }

    public void CloseThisUI()
    {
        UIManager.Instance.OpenUI<GamePlayUI>();
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }
}
