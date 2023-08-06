using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UICanvas
{
    public void CloseInventory()
    {
        UIManager.Instance.OpenUI<MainMenuUI>();

        Close();
    }
}
