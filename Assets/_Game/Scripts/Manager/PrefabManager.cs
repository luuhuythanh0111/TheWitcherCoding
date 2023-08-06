using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    [SerializeField] protected DropBall botExpBallPrefab;
    [SerializeField] protected DropBall bossExpBallPrefab;

    public DropBall GetBotExpBallPrefab()
    { return botExpBallPrefab; }

    public DropBall GetBossExpBallPrefab() 
    { return bossExpBallPrefab; }
}
