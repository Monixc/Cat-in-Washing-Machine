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
        UnityEngine.Debug.Log(gameObject.name + damage + "�� �������� �޾ҽ��ϴ�");
        UnityEngine.Debug.Log(gameObject.name + "���� ü��" + CurrentHealth);

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
