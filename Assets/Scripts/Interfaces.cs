using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interfaces
{

}

public interface IDamageable
{
    void ProcessDamage(int damageAmount);
}

public interface IPowerup
{
    string PowerUpName { get; }
    int PowerUpID { get; }
    void ActivatePowerUp();
}


public interface IExplode
{
    void Explode();
}

public interface IScoreable
{
    void Score(int score);
}
