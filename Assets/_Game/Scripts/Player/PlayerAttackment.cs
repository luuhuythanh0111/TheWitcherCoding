using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackment : AttackMethods
{
    [SerializeField] internal float attackTimeLoop;
    [SerializeField] internal float timer;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        timer = 0;
        attackTimeLoop = 3f;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > attackTimeLoop - 0.5f)
        {
            player.SetMana(1000);
        }
        if (timer > attackTimeLoop)
        {
            timer = 0;
            
            Attack();
        }
    }

    IEnumerator WaitForResetMana(float time)
    {
        yield return new WaitForSeconds(time);
        player.SetMana(1000);
    }

    private void Attack()
    {
        /// CODE 1
        //KunaiAutoShotting(0, this);

        //for (int i = 1; i <= 4; i++)
        //{
        //    KunaiShotting(90 * i, 50);
        //}

        ///CODE 2
        //if (DetectMonster(100))
        //{
        //ShieldActive(0);
        //SparkShotting(0, 50);
        //SparkShotting(0, 55);
        //SparkShotting(180, 50);
        //SparkShotting(180, 55);
        //}
        //else
        //{
        //KunaiAutoShotting(50, this);
        //}    

        //SparkAutoShotting(0);
        //SparkCircleShotting(50);
        LazerAutoShotting(50);
    }

}
