using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Normal_IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.BotSetState(Assets.FantasyMonsters.Scripts.MonsterState.Idle);
    }

    public void OnExecute(Bot t)
    {
        t.currentState.ChangeState(new Normal_ChaseState());
    }

    public void OnExit(Bot t)
    {
    }
}
