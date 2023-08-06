using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_ChaseState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.BotSetState(Assets.FantasyMonsters.Scripts.MonsterState.Run);
    }

    public void OnExecute(Bot t)
    {
        if(t.isAttack)
        {
            t.currentState.ChangeState(new Normal_AttackState());
        }
        t.transform.rotation = Quaternion.Euler(new Vector3(0, t.transform.position.x > t.player.transform.position.x ? 0 : 180, 0));
        t.transform.Translate((t.player.transform.position - t.transform.position).normalized * t.speedBot * Time.fixedDeltaTime, Space.World);
    }

    public void OnExit(Bot t)
    {
    }
}
