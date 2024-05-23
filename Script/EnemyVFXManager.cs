using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class EnemyVFXManager : MonoBehaviour
{
    public ParticleSystem AttackVFX;
    public ParticleSystem BeingHitVFX;
    public ParticleSystem DeathVFX;

    public void PlayAttackVFX()
    {
        AttackVFX.Play();
    }
    public void PlayBeingHitVFX()
    {
        BeingHitVFX.Play();

    }
    public void PlayDeathVFX()
    {
        DeathVFX.Play();
    }
}
