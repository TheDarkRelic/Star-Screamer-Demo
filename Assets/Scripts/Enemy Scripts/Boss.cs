using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, IDamageable
{
    public GameObject[] expSpawnPoints = new GameObject[5];
    [SerializeField] private InstantiateExplosion explosion = null;
    [SerializeField] private GameObject _hitParticles = null;
    [SerializeField] private AudioSource aSource;
    [SerializeField] private BossAnimEvent bossAnim = null;
    [SerializeField] private SpriteRenderer bossSprite = null;
    [SerializeField] private float hitParticleOffset = 0.35f;
    [SerializeField] private float shieldTimer = 4f;
    [SerializeField] private float timeBetweenExplosion = 0f;
    [SerializeField] private int scoreAmount = 1500;
    [SerializeField] private int health = 300;
    [SerializeField] private float destroyTimer;
    public int Health { get => health; set => health = value; }
    public bool isDamageable = false;
    public static bool isAlive = false;
    private bool isScorable = false;

    void Start()
    {
        isAlive = true;
        isDamageable = false;
        isScorable = true;
    } 


    private void OnTriggerEnter2D(Collider2D other)
    {
        var x = other.transform.position.x;
        var y = other.transform.position.y + hitParticleOffset;

        if (other.CompareTag("Laser"))
        {
            var laser = other.gameObject.GetComponent<Laser>();
            Instantiate(_hitParticles, new Vector2(x, y), Quaternion.identity);
            var capCollider = laser.GetComponent<CapsuleCollider2D>();
            var sprites = laser.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            capCollider.enabled = false;
            Destroy(other.gameObject, .3f);
            if (isDamageable) ProcessDamage(laser.damageAmount);
        }
        
        if (other.CompareTag("Player"))
        {
            var damage = other.gameObject.GetComponent<HitDamage>();
            if (damage != null) damage.ProcessDamage(damage.health);
        }
    }

    public void ProcessDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            var anim = GetComponent<Animator>();
            Destroy(anim);
            bossAnim._photonLaserParticles.Stop();
            if(isAlive)StartCoroutine(LoopExplosions());
            CallForScore();
            Destroy(this.gameObject, destroyTimer);
        }

    }

    private void CallForScore()
    {
        if (isScorable)
        {
            EventsList.OnScoreAction?.Invoke(scoreAmount);
            isScorable = false;
        }
        
    }

    private IEnumerator LoopExplosions()
    {
        isAlive = false;
        while (true)
        {
            foreach (var point in expSpawnPoints)
            {
                explosion.InitExplosion(point);
                if(Time.timeScale > 0.2) Time.timeScale -= .05f;
                yield return new WaitForSeconds(timeBetweenExplosion);
            }
        }
        
    }


    private void SetDamageable() => isDamageable = true;
    private void SetNotDamageable() => isDamageable = false;
    private IEnumerator BossShieldCoolDown()
    {
        SetNotDamageable();
        yield return new WaitForSeconds(shieldTimer);
        SetDamageable();
    }

    private void BossSprite()
    {
        bossSprite.enabled = false;
    }

    public void LaserCanonSfx() => aSource.Play();

}
