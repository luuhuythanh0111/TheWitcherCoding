using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBall : GameUnit
{
    public Player player;
    public TypeOfBall typeOfBall;
    public float Value;

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit(Weapon t)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        player = FindObjectOfType<Player>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(typeOfBall == TypeOfBall.Exp)
                player.IncreaseExp(Value);
            OnDespawn();
        }
    }
}

public enum TypeOfBall
{
    Health,
    Mana,
    Exp,
}
