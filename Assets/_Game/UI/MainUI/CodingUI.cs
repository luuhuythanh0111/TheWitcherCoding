using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodingUI : UICanvas
{
    public int NumberOfTab;

    public PlayerCodeInput playerCodeInput;

    public Text errorTxt;

    public TMP_InputField inputPLayerCode;

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

    public void ChangeToInventory()
    {
        UIManager.Instance.OpenUI<InventoryUI>();
        Close();
    }

    public void AddMoreLine()
    {
        inputPLayerCode.lineLimit += 2;
    }

}
