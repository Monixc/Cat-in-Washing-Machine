using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class PlayerVFXManager : MonoBehaviour
{
    public ParticleSystem Weapon;
    public ParticleSystem BeingHit;

    public void PlayBeingHitVFX()
    {
        BeingHit.Play();
    }

    public void PlayWeapon()
    {
        Weapon.Play();
    }
}
