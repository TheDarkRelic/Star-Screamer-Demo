using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interfaces
{

}

public interface IDamageable
{
    int Health { get; set; }

    void ProcessDamage(int damageAmount);


}
