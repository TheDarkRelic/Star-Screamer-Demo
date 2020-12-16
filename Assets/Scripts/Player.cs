using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    void Awake()
    {
        movementActive = true;
        shieldActive = false;
        optionsActive = false;
    }

    void Update()
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
        var playerCollider = GetComponent<Collider2D>();
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

}

