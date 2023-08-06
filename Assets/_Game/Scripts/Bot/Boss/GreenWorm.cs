using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWorm : Bot
{
    protected override void Start()
    {
        base.Start();

        speedBot = Random.Range(2f, 3f);
        hpBot = maxHpBot = Random.Range(1000f, 1600f);
        damageBot = Random.Range(30f, 40f);
        healthBarUI.OnChangeHP(hpBot / maxHpBot);
    }
}
