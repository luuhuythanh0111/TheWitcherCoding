using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackMethods : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyMask;

    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        enemyMask = (1<<3); 
    }

    public void KunaiShotting(float degrees, float ratioTime)
    {
        Kunai kunai = SimplePool.Spawn<Kunai>(PoolType.Kunai, transform.position, transform.rotation);
        kunai.OnInit();
        if (CanDecreasePlayerMana(kunai.manaWeapon) == false)
        {
            kunai.OnDespawn();
            return;
        }
        kunai.Shotting(degrees, ratioTime);
    }

    public void KunaiAutoShotting(float ratioTime)
    {
        Kunai kunai = SimplePool.Spawn<Kunai>(PoolType.Kunai, transform.position, transform.rotation);
        kunai.OnInit();
        if (CanDecreasePlayerMana(kunai.manaWeapon) == false)
        {
            kunai.OnDespawn();
            return;
        }
        kunai.AutoShotting(ratioTime);
    }

    public void SparkShotting(float degrees, float ratioTime)
    {
        Spark spark = SimplePool.Spawn<Spark>(PoolType.Spark, transform.position, transform.rotation);
        spark.OnInit();
        if (CanDecreasePlayerMana(spark.manaWeapon) == false)
        {
            spark.OnDespawn();
            return;
        }
        spark.Shotting(degrees, ratioTime);
    }

    public void SparkAutoShotting(float ratioTime)
    {
        Spark spark = SimplePool.Spawn<Spark>(PoolType.Spark, transform.position, transform.rotation);
        spark.OnInit();

        if (CanDecreasePlayerMana(spark.manaWeapon) == false)
        {
            spark.OnDespawn();
            return;
        }

        spark.AutoShotting(ratioTime);
    }

    public void SparkCircleShotting(float ratioTime)
    {
        Spark spark = SimplePool.Spawn<Spark>(PoolType.Spark, transform.position, transform.rotation);
        spark.OnInit();
        if (CanDecreasePlayerMana(spark.manaWeapon) == false)
        {
            spark.OnDespawn();
            return;
        }
        spark.CircleShotting(ratioTime);
    }

    public void ShieldActive(float ratioTime)
    {
        Shield shield = SimplePool.Spawn<Shield>(PoolType.Shield, transform.position, transform.rotation);
        shield.OnInit();
        if (CanDecreasePlayerMana(shield.manaWeapon) == false)
        {
            shield.OnDespawn();
            return;
        }
        shield.ActiveTime(ratioTime);
    }

    public bool DetectMonster(float hpMonster)
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, 20f, enemyMask);

        for(int i=0;i<rangeCheck.Length;i++)
        {
            Bot bot = rangeCheck[i].GetComponent<Bot>();
            if (bot.hpBot > hpMonster)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanDecreasePlayerMana(float value)
    {
        if (player.CanDecreaseMana(value) == true)
        {
            player.DecreaseMana(value);
            return true;
        }
        return false;
    }

    public void LazerShotting(float degrees, float ratioTime)
    {
        Lazer lazer = SimplePool.Spawn<Lazer>(PoolType.Lazer, transform.position, transform.rotation);
        lazer.OnInit();
        if (CanDecreasePlayerMana(lazer.manaWeapon) == false)
        {
            lazer.OnDespawn();
            return;
        }
        lazer.Shotting(degrees, ratioTime);
    }

    public void LazerAutoShotting(float ratioTime)
    {
        Lazer lazer = SimplePool.Spawn<Lazer>(PoolType.Lazer, transform.position, transform.rotation);
        lazer.OnInit();
        if (CanDecreasePlayerMana(lazer.manaWeapon) == false)
        {
            lazer.OnDespawn();
            return;
        }
        lazer.AutoShotting(ratioTime);
    }
}
