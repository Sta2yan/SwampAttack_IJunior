using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    
    private Player _player;

    public Player Target => _player;
    public int Reward => _reward;
    
    public event UnityAction<Enemy> Dying;

    public void Init(Player target)
    {
        _player = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Dying?.Invoke(this);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
