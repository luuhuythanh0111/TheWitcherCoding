using System.Collections;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected Player player;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider2D[] rangeCheck;
    [SerializeField] protected LayerMask enemyMask;

    [SerializeField] protected float damageWeapon;
    [SerializeField] protected float speedWeapon;
    [SerializeField] internal float manaWeapon;

    [SerializeField] protected Vector3 directionWeapon;
    [SerializeField] protected Vector3 offsetWeaponApear;
    [SerializeField] protected Vector3 scaleWeapon;
    [SerializeField] protected float timeAttack;
    [SerializeField] protected float startTimeAttack;
    [SerializeField] protected float degreesIndex;
    [SerializeField] protected float ratioTimeIndex;
    [SerializeField] protected float timerWeapon;

    [SerializeField] protected bool isAttack;
    [SerializeField] protected bool isAutoShotting;
    [SerializeField] protected bool isShotting;
    [SerializeField] protected bool isCircleShotting;
    [SerializeField] protected bool isSummon;
    [SerializeField] protected bool isDestroy;

    public Transform nearestEnemy;

    private float radius;
    private float angle;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        offsetWeaponApear = new Vector3(2, 2, 0);   
        enemyMask = (1<<3);
    }

    public override void OnInit()
    {
        startTimeAttack = 1;
        scaleWeapon = transform.localScale;

        spriteRenderer.enabled = false;
        isAttack = false;
        isAutoShotting = false;
        isShotting = false;
        isCircleShotting = false;
        isSummon = false;
        isDestroy = false;

        radius = 2f;
        angle = 0f;
        timerWeapon = 0f;
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit(Weapon t)
    {
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
    }

    public void IncreaseDamage(float increaseDam)
    {
        damageWeapon += increaseDam;
    }

    public bool FindTheNeathestEnemy()
    {
        rangeCheck = Physics2D.OverlapCircleAll(player.transform.position, 20f, enemyMask);

        if (rangeCheck.Length < 1) 
        {
            return false;
        }

        float distance = 1000f;
        float temporaryDistance;

        for (int i = 0; i < rangeCheck.Length; i++)
        {
            temporaryDistance = Vector3.Distance(player.transform.position, rangeCheck[i].transform.position);

            if (distance > temporaryDistance)
            {
                distance = temporaryDistance;
                nearestEnemy = rangeCheck[i].transform;
            }
        }

        return true;
    }

    public void WaitToShoot()
    {
        timerWeapon += Time.fixedDeltaTime;
        Vector3 newPos = Vector3.Lerp(transform.position, 
                                      player.transform.position + new Vector3(player.transform.rotation.y == 0? -1:1, 1, 0), 
                                      0.1f);
        transform.position = newPos;
    }

    public void ChangeRotationByDirection()
    {
        float radians = Mathf.Deg2Rad * degreesIndex;

        directionWeapon = new Vector3((float)Mathf.Cos(radians), (float)Mathf.Sin(radians), 0);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z + degreesIndex - 90));
    }

    public void ChangeRotationByNearestEnemy()
    {
        directionWeapon = nearestEnemy.position - transform.position;

        Vector2 direction = nearestEnemy.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    public void CountdownTimeAttack()
    {
        timeAttack -= Time.fixedDeltaTime;
        if (timeAttack < 0)
        {
            OnDespawn();
        }
    }

    public void MovingLiner()
    {
        transform.Translate(directionWeapon.normalized * speedWeapon * Time.fixedDeltaTime, Space.World);
    }

    public void MovingCircle()
    {
        // Tính toán góc mới dựa trên thời gian đã trôi qua và tốc độ quay
        angle += speedWeapon * Time.fixedDeltaTime;

        // Tính toán vị trí mới xung quanh điểm trung tâm
        float x = player.transform.position.x + radius * Mathf.Cos(angle);
        float y = player.transform.position.y + radius * Mathf.Sin(angle);

        // Gán vị trí mới cho GameObject
        transform.position = new Vector3(x, y, 0);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    #region Explosive

    public void Explosive(float range)
    {
        isDestroy = true;

        rangeCheck = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        for (int i = 0; i < rangeCheck.Length; i++)
        {
            Bot enemy = rangeCheck[i].GetComponent<Bot>();
            if (!enemy.isDeath)
            {
                enemy.isDamaged(damageWeapon);
            }
        }

        StartChangeScale();
    }

    public void StartChangeScale()
    {
        this.transform.localScale = new Vector3(scaleWeapon.x * 5, scaleWeapon.y * 5, 1);
        StartCoroutine(CountdownScaleTime());
    }

    private IEnumerator CountdownScaleTime()
    {
        float timer = 0f;

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        this.transform.localScale = scaleWeapon;
        OnDespawn();
    }

    #endregion

    #region Shotting & Summon

    public void Shotting(float degrees, float ratioTime)        /// Bắn theo hướng 
    {
        degreesIndex = degrees;
        ratioTimeIndex = ratioTime;
        startTimeAttack = ratioTimeIndex / 100 * 3f;

        isShotting = true;
    }

    public void AutoShotting(float ratioTime)                    /// Bắn thằng Enemy gần nhất
    {
        if (!FindTheNeathestEnemy())
        {
            OnDespawn();
        }

        ratioTimeIndex = ratioTime;
        startTimeAttack = ratioTimeIndex / 100 * 3f;

        isAutoShotting = true;
    }

    public void CircleShotting(float ratioTime)
    {
        ratioTimeIndex = ratioTime;
        startTimeAttack = ratioTimeIndex / 100 * 3f;

        isCircleShotting = true;
    }

    public void Summon(float ratioTime)
    {
        if (!FindTheNeathestEnemy())
        {
            OnDespawn();
        }

        ratioTimeIndex = ratioTime;
        startTimeAttack = ratioTimeIndex / 100 * 3f;

        isSummon = true;
    }    
    #endregion

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Bot enemy = other.GetComponent<Bot>();
            if (!enemy.isDeath)
            {
                isDestroy = true;
                enemy.isDamaged(damageWeapon);
                OnDespawn();
            }
        }
    }
}
