using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider2D : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        var hitDamage = other.GetComponent<HitDamage>();
        if (hitDamage != null && other.gameObject != null)
        {
            if (this.gameObject.name == "PhotonMissileParticleWorld" || this.gameObject.name == "PhotonMissileParticleLocal")
            {
                hitDamage.ProcessDamage(1);
            }
            if (this.gameObject.name == "Boss1_Bolt")
            {
                hitDamage.ProcessDamage(1);
            }
            
        }
        
    }
}
