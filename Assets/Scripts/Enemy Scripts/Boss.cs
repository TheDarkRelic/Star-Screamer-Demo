using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, IDamageable
{
    public GameObject[] expSpawnPoints = new GameObject[5];
    [SerializeField] private int _health = 500;
    public int Health { get => _health; set => _health = value; }
    [SerializeField] InstantiateExplosion explosion;
    [SerializeField] GameObject _hitParticles;
    [SerializeField] float _hitParticleOffset;
    [SerializeField] AudioClip _laserSfx;
    [SerializeField] float _laserSfxVolume = 0.5f;
    [SerializeField] float _shieldTimer = 4;
    public bool isDamageable;
    public static bool isAlive;
    [SerializeField] float _timeBetweenExplosion;
    [SerializeField] SpriteRenderer bossSprite;
    private int _pointToSpawnExplosion;
    [SerializeField] int scoreAmount;
    private bool isScorable = true;
    [SerializeField] BossAnimEvent bossAnim;
    void Start()
    {
        isAlive = true;
        _pointToSpawnExplosion = Random.Range(0, expSpawnPoints.Length);
        isDamageable = false;
    } 


    private void OnTriggerEnter2D(Collider2D other)
    {
        var x = other.transform.position.x;
        var y = other.transform.position.y + _hitParticleOffset;

        if (other.CompareTag("Laser"))
        {
            Instantiate(_hitParticles, new Vector2(x, y), Quaternion.identity);
            Destroy(other.gameObject);
            if (isDamageable) ProcessDamage(1);
        }
        
        if (other.CompareTag("Player"))
        {
            var damage = other.gameObject.GetComponent<HitDamage>();
            if (damage != null) damage.ProcessDamage(3);
        }

        /*if (!other.CompareTag("Missile"))
        return;
    Destroy(other.gameObject);
    if (isDamageable) ProcessDamage(2);*/
    }

    public void ProcessDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            var anim = GetComponent<Animator>();
            Destroy(anim);
            bossAnim._photonLaserParticles.Stop();
            _laserSfxVolume = 0;
            if (isAlive) StartCoroutine(LoopExplosions());
            CallForScore();
            Destroy(this.gameObject, 2.1f);
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
        foreach (var point in expSpawnPoints)
        { 
            explosion.InitExplosion(point);
            Time.timeScale -= .08f;
            yield return new WaitForSeconds(_timeBetweenExplosion);
        }
    }


    private void SetDamageable() => isDamageable = true;
    private void SetNotDamageable() => isDamageable = false;
    private IEnumerator BossShieldCoolDown()
    {
        SetNotDamageable();
        yield return new WaitForSeconds(_shieldTimer);
        SetDamageable();
    }

    private void BossSprite()
    {
        bossSprite.enabled = false;
    }

    public void LaserCanonSfx() => AudioSource.PlayClipAtPoint(_laserSfx, Camera.main.transform.position, _laserSfxVolume);

}
