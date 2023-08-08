using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Character character;
    [SerializeField] protected float speedPlayer;
    [SerializeField] protected float hpPlayer;
    [SerializeField] internal int levelPlayer;

    [SerializeField] internal bool isNoDamaged;

    public GameObject playerAttackment;
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private ManaBarUI manaBarUI;
    [SerializeField] private ExpBarUI expBarUI;

    public float expPLayer;
    public float maxExpPlayer;

    public float manaPlayer;
    public float maxManaPlayer;

    private float maxHp;
    private float maxMana;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        isNoDamaged = false;

        speedPlayer = 6f;
        maxHp = hpPlayer = 100;
        manaPlayer = maxManaPlayer = 100;

        levelPlayer = 0;
        expPLayer = 0;
        maxExpPlayer = 10;
        expBarUI.OnChangeExp(expPLayer / maxExpPlayer);
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(joystick.Vertical) > 0.001f || Mathf.Abs(joystick.Horizontal) > 0.001f)
        {
            Vector2 direction = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
            rb.velocity = direction * speedPlayer;
            character.SetState(CharacterState.Run);

            transform.rotation = Quaternion.Euler(new Vector3(0, joystick.Horizontal < 0 ? 180 : 0, 0));
        }
        else
        {
            rb.velocity = Vector2.zero;
            character.SetState(CharacterState.Idle);
        } 
    }

    internal void isDamaged(float damageBot)
    {
        if (isNoDamaged)
            return;     
        hpPlayer -= damageBot;
        if(hpPlayer < 0)
        {
            //TODO: Gameover
            GameManager.Instance.ChangeState(GameState.Lose);
        }
        healthBarUI.OnChangeHP(hpPlayer / maxHp);
    }

    public void IncreaseExp(float Value)
    {
        expPLayer += Value;
        expBarUI.OnChangeExp(expPLayer / maxExpPlayer);
        if(expPLayer >= maxExpPlayer)
        {
            expPLayer -= maxExpPlayer;
            if(levelPlayer >= 25)
            {
                return;
            }
            levelPlayer++;
            if(levelPlayer % 2 == 0)
            {
                GameManager.Instance.ChangeState(GameState.CodingUI);
                UIManager.Instance.codingUI.AddMoreLine();
                UIManager.Instance.CloseUI<GamePlayUI>();
                UIManager.Instance.OpenUI<CodingUI>();
            }
        }
    }

    public void SetMana(int value)
    {
        manaPlayer = Mathf.Max(MathF.Min(maxManaPlayer, value), 0);
        manaBarUI.OnChangeMana(manaPlayer / maxManaPlayer);
    }

    public bool CanDecreaseMana(float value)
    {
        if(manaPlayer >= value)
        {
            return true;
        }        
        return false;
    }

    public void DecreaseMana(float value)
    {
        manaPlayer -= value;
        manaBarUI.OnChangeMana(manaPlayer / maxManaPlayer);
    }
}
