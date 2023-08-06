using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Normal_DieState : IState<Bot>
{
    private float timer;
    public void OnEnter(Bot t)
    {
        t.BotSetState(Assets.FantasyMonsters.Scripts.MonsterState.Death);
        t.capsuleCollider2D.isTrigger = true;
        t.isDeath = true;
        t.isAttack = false;
        timer = 4f;
    }

    public void OnExecute(Bot t)
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            t.DropExp();
            t.OnDespawn();
        }
    }

    public void OnExit(Bot t)
    {
    }
}
