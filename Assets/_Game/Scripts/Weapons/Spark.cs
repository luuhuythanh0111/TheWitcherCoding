using UnityEngine;

public class Spark : Weapon
{
    public override void OnInit()
    {
        base.OnInit();
        spriteRenderer.enabled = true;
        damageWeapon = 50f;
        speedWeapon = 7f;
        timeAttack = 1.5f;
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

        if (isDestroy)
            return;

        CountdownTimeAttack();
        if (isCircleShotting) MovingCircle();
        else MovingLiner();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Explosive(2f);
        }
    }
}
