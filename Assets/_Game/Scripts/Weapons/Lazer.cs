using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Weapon
{
    public override void OnInit()
    {
        base.OnInit();
        spriteRenderer.enabled = true;
        damageWeapon = 30f;
        speedWeapon = 10f;
        timeAttack = 1f;
        manaWeapon = 10f;
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Bot enemy = other.GetComponent<Bot>();
            if (!enemy.isDeath)
            {
                enemy.isDamaged(damageWeapon);
            }
        }
    }
}
