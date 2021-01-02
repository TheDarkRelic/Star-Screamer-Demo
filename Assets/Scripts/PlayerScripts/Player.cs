using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnShieldDeactivate;
    public UnityEvent OnShieldActivate;
    public PlayerShoot playerShoot;
    public PlayerMovement playerMove;
    public SetBounds playerBounds;
    public bool shieldActive;
    public bool optionsActive;
    public bool movementActive;
    public HitDamage hitDamage;
    [SerializeField] Collider2D playerCollider;

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
        if (Input.GetKey(KeyCode.Space) && Time.time > playerShoot._canFire)
            playerShoot.FireLaser();
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

