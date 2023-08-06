using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeleteBtnCode : MonoBehaviour
{
    public TMP_InputField playerCode;

    public CodingUI codingUI;

    public void DeletePLayerCode()
    {
        if (playerCode.text.Length > 0)
        {
            if (playerCode.text[playerCode.text.Length - 1] == '{')
                codingUI.NumberOfTab--;
            playerCode.text = playerCode.text.Remove(playerCode.text.Length - 1);
        }
    }
}
