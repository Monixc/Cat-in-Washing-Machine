using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    private Character _cc;
    
    private void Awake()
    {
        CurrentHealth = MaxHealth;
        _cc = GetComponent<Character>();
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        UnityEngine.Debug.Log(gameObject.name + damage + "의 데미지를 받았습니다");
        UnityEngine.Debug.Log(gameObject.name + "현재 체력" + CurrentHealth);

        CheckHealth();
    }

    private void CheckHealth()
    {
        if(CurrentHealth <= 0)
        {
            _cc.SwitchStateTo(Character.CharacterState.Dead);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
