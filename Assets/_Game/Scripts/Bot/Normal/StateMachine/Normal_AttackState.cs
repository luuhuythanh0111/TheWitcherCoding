using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_AttackState : IState<Bot>
{
    private float timerAttackLoop;
    private float timerAttack;
    public void OnEnter(Bot t)
    {
        t.BotAttack();
        timerAttackLoop = 2f;
        timerAttack = 0.4f;
    }

    public void OnExecute(Bot t)
    {
        timerAttackLoop -= Time.fixedDeltaTime;
        timerAttack -= Time.fixedDeltaTime;
        if(timerAttack <0f)
        {
            t.player.isDamaged(t.damageBot);
            timerAttack = 100f;
        }
        if(timerAttackLoop < 0f)
        {
            t.currentState.ChangeState(new Normal_IdleState());
        }
    }

    public void OnExit(Bot t)
    {
    }
}
