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
