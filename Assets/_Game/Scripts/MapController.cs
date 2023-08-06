using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : GameUnit
{
    [SerializeField] internal bool isPlayerContaining;

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        isPlayerContaining = true;
    }

    public override void OnInit(Weapon t)
    {
        throw new NotImplementedException();
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new NotImplementedException();
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isPlayerContaining = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerContaining = false;
        }
    }
}
