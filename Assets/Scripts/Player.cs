using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Action onPlayerDeathAction;

    [SerializeField] float _laserOffset = .08f;
    [SerializeField] float _fireRate = .05f;
    [SerializeField] int _powerUpTimer = 5;

    float _canFire = -1f;

    public GameObject laserPreFab;
    public GameObject tripleShotPreFab;

    [SerializeField] GameObject _explosionPreFab;

    UiHandler _uiHandler;
    public PlayerMovement playerMove;
    public SetBounds playerBounds;

    public static bool isTripleShotActive = false;
    public static bool optionsActive;

    void OnEnable()
    {

    }
    void Awake()
    { 
        optionsActive = false;
        AssignComponents();

    }

    void Update()
    {
        playerBounds.ObjectBounds();
        playerMove.CalculateMovement();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            FireLaser();
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (isTripleShotActive == true)
        {
            Instantiate(tripleShotPreFab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPreFab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
        }
    }

    

    private void AssignComponents()
    {
        _uiHandler = GameObject.Find("Canvas").GetComponent<UiHandler>();
    }
   

    public void TripleShotActive()
    {
        isTripleShotActive = true;
    }

}

