using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;

public class Bot : GameUnit
{
    [SerializeField] internal Monster monster;
    [SerializeField] internal CapsuleCollider2D capsuleCollider2D;
    [SerializeField] internal Player player;
    [SerializeField] protected DropBall expBall;
    [SerializeField] protected HealthBarUI healthBarUI;

    [SerializeField] internal float speedBot;
    [SerializeField] internal float hpBot;
    [SerializeField] internal float damageBot;
    [SerializeField] protected float maxHpBot;

    [SerializeField] internal bool isAttack;
    [SerializeField] internal bool isDeath;


    #region State Machine and Update
    internal StateMachine<Bot> currentState;

    private void Awake()
    {
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        capsuleCollider2D = this.GetComponent<CapsuleCollider2D>();
        monster = this.GetComponent<Monster>();
        expBall = PrefabManager.Instance.GetBotExpBallPrefab();
        currentState = new StateMachine<Bot> ();
        currentState.SetOwner(this);
    }

    virtual protected void FixedUpdate()
    {
        this.UpdateCharacterState();
    }

    virtual protected void UpdateCharacterState()
    {
        currentState.UpdateState(this);
    }
    #endregion

    protected virtual void Start()
    {
        player = FindObjectOfType<Player> ();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        monster = GetComponent<Monster>();
        expBall = PrefabManager.Instance.GetBotExpBallPrefab();
        healthBarUI = GetComponentInChildren<HealthBarUI>();


        currentState.ChangeState(new Normal_IdleState());
        isAttack = false;
        isDeath = false;
        capsuleCollider2D.isTrigger = false;

        speedBot = Random.Range(1f, 3f);
        hpBot = maxHpBot = Random.Range(50f, 300f);
        damageBot = Random.Range(5f, 10f);
        healthBarUI.OnChangeHP(hpBot / maxHpBot);
    }

    public override void OnInit()
    {
        LevelManager.Instance.InitBot(this);
        currentState.ChangeState(new Normal_IdleState());
        isAttack = false;
        isDeath = false;
        capsuleCollider2D.isTrigger = false;

        speedBot = Random.Range(1f, 3f);
        hpBot = maxHpBot = Random.Range(50f, 300f);
        damageBot = Random.Range(5f, 10f);
        healthBarUI.OnChangeHP(hpBot / maxHpBot);
    }

    public override void OnDespawn()
    {
        if(hpBot <= 0)
            LevelManager.Instance.DespawnBot(this);
        SimplePool.Despawn(this);
    }

    public override void OnInit(Weapon t)
    {
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
    }

    public void IncreaseDamage(float increaseDamage)
    {
        damageBot += increaseDamage;
    }

    public void isDamaged(float damage)
    {
        hpBot -= damage;
        healthBarUI.OnChangeHP(hpBot / maxHpBot);
        monster.Spring();
        if(hpBot < 0)
        {
            //BUG: Từ Die Anim sang OnDespawn bị lỗi anim

            //currentState.ChangeState(new Normal_DieState());
            //BotSetState(MonsterState.Death);

            //boxCollider.isTrigger = true;
            //isDeath = true;
            //isAttack = false;

            DropExp();
            OnDespawn();
            //StartDieAnim();
        }
    }
    //public void StartDieAnim()
    //{
    //    currentState.ChangeState(new Normal_DieState());
    //    BotSetState(MonsterState.Death);

    //    boxCollider.isTrigger = true;
    //    isDeath = true;
    //    isAttack = false;
    //    StartCoroutine(CountdownDieAnim());
    //}

    //private IEnumerator CountdownDieAnim()
    //{
    //    float timer = 3f;

    //    while (timer > 0f)
    //    {
    //        timer -= Time.fixedDeltaTime;
    //        yield return null;
    //    }

    //    BotSetState(MonsterState.Idle);
    //    OnDespawn();
    //}

    public void DropExp()
    {
        SimplePool.Spawn<DropBall>(expBall, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Change Animation
    /// </summary>
    public void BotSetState(MonsterState state)
    {
        monster.SetState(state);
    }

    public void BotAttack()
    {
        monster.Attack();
    }

    /// <summary>
    /// Trigger2D
    /// </summary>

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isAttack = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isAttack = false;
        }
    }
}
