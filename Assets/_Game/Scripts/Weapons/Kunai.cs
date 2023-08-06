using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Kunai : Weapon
{
    public override void OnInit()
    {
        base.OnInit();
        spriteRenderer.enabled = true;
        damageWeapon = 20f;
        speedWeapon = 10f;
        timeAttack = 1f;
        manaWeapon = 5f;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            WaitToShoot();

            if (timerWeapon > startTimeAttack)
            {
                if (isShotting) ChangeRotationByDirection();
                if (isAutoShotting) ChangeRotationByNearestEnemy();
                isAttack = true;
            }

            return;
        }

        CountdownTimeAttack();
        MovingLiner();
    }
}
