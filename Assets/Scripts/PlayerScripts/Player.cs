using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnShieldDeactivate;
    public UnityEvent OnShieldActivate;
    public PlayerShoot playerShoot = null;
    public PlayerMovement playerMove = null;
    public SetBounds playerBounds = null;
    public bool shieldActive = false;
    public bool optionsActive = false;
    public bool movementActive = false;
    public HitDamage hitDamage = null;
    [SerializeField] Collider2D playerCollider = null;

    void Awake()
    {
        movementActive = true;
        shieldActive = false;
        optionsActive = false;
    }

    void FixedUpdate()
    {
        if (movementActive)
        {
            playerBounds.ObjectBounds();
            playerMove.CalculateMovement();
            ArmLaser();
        }
    }

    private void ArmLaser()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time > playerShoot._canFire)
            {
                playerShoot.FireLaser();
            }
        }    
    }

    public void DestroyPlayer()
    {
        movementActive = false;
        var movementScript = GetComponent<PlayerMovement>();
        playerCollider.enabled = false;
        Destroy(movementScript);
        EventsList.OnPlayerDeath?.Invoke();


        var sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            sprite.enabled = false;
        }
    }

    private void ColliderColission()
    {
        playerCollider.isTrigger = false;
    }

    private void OnEnable()
    {
        BossAnimEvent.onBossEntry += ColliderColission;
    }

    private void OnDisable()
    {
        BossAnimEvent.onBossEntry -= ColliderColission;
    }
}

