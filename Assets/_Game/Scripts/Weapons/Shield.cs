using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    public Vector3 offset;

    public override void OnInit()
    {
        base.OnInit();
        timeAttack = 1.5f;
        manaWeapon = 20f;
        player.isNoDamaged = true;
    }

    public override void OnDespawn()
    {
        player.isNoDamaged = false;
        base.OnDespawn();
    }

    private void FixedUpdate()
    {
        timerWeapon += Time.fixedDeltaTime;
        if (timerWeapon > startTimeAttack && !isAttack)
        {
            isAttack = true;
            spriteRenderer.enabled = true;
        }

        transform.position = player.transform.position + offset;

        CountdownTimeAttack();
    }


    public void ActiveTime(float ratioTime)
    {
        ratioTimeIndex = ratioTime;
        startTimeAttack = ratioTimeIndex / 100 * 3f;
    }
}
