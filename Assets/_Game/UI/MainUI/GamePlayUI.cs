using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : UICanvas
{
    public GameObject bossWarning;

    private void Start()
    {
        UIManager.Instance.gamePlayUI = this;
    }

    public void Pause()
    {
        UIManager.Instance.OpenUI<PauseUI>();
        GameManager.Instance.ChangeState(GameState.Pause);
        Close();
    }

    public void BossWarningUI()
    {
        bossWarning.SetActive(true);
        StartCoroutine(WaitToEndBossSign());
    }

    IEnumerator WaitToEndBossSign()
    {
        yield return Cache.GetWFS(1f);
        bossWarning.SetActive(false);
    }
}
