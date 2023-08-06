using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddCode : MonoBehaviour
{
    public PlayerInputType playerInputType;

    public TMP_InputField[] inputString = new TMP_InputField[5];

    public TMP_InputField playerCode;

    public Button addCodeBtn;

    public CodingUI codingUI;

    private void Start()
    {
        addCodeBtn.onClick.AddListener(AddToPLayerCode);
    }

    public void AddToPLayerCode()
    {
        if(playerInputType == PlayerInputType.EndCode)
        {
            codingUI.NumberOfTab = Mathf.Max(0, codingUI.NumberOfTab - 1);
        }
        for (int i = 0; i < codingUI.NumberOfTab; i++)
        {
            playerCode.text += "\t";
        }

        switch (playerInputType)
        {
            case PlayerInputType.Bien:
                playerCode.text = playerCode.text + inputString[0].text + " " + inputString[1].text + ";\n";
                return;
            case PlayerInputType.Mang:
                playerCode.text = playerCode.text + inputString[0].text + " " + inputString[1].text + "[" + inputString[2].text + "]" +  ";\n";
                return;
            case PlayerInputType.HamChoBien:
                playerCode.text = playerCode.text + inputString[0].text + "." + inputString[1].text + ";\n";
                return;
            case PlayerInputType.HamChoMang:
                playerCode.text = playerCode.text + inputString[0].text + "[" + inputString[1].text + "]" + "." + inputString[2].text + ";\n";
                return;
            case PlayerInputType.For:
                playerCode.text += "for(int i = " + inputString[0].text + "; i <= " + inputString[1].text + "; i++) \n{\n";

                codingUI.NumberOfTab++;
                return;
            case PlayerInputType.Ham:
                playerCode.text += inputString[0].text + ";\n";
                return;
            case PlayerInputType.EndCode:
                playerCode.text += "}\n";                
                return;
            case PlayerInputType.If:
                playerCode.text += "if(" + inputString[0].text + ")" + "\n";

                for (int i = 0; i < codingUI.NumberOfTab; i++)
                {
                    playerCode.text += "\t";
                }
                playerCode.text += "{\n";
                codingUI.NumberOfTab++;
                return;
        }
        
    }

}

public enum PlayerInputType
{
    Bien,
    Mang,
    HamChoBien,
    HamChoMang,
    Ham,
    For,
    EndCode,
    If,
}