using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _target;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _timeAfterLastSpawn;
    private float _timeAfterWave;
    private int _spawned;
    private int _aliveEnemy;

    private void Start()
    {
        SetWave(_currentWaveIndex);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
            _currentWave = null;
    }

    public void NextWave()
    {
        SetWave(++_currentWaveIndex);
        _spawned = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_waves[_currentWaveIndex].Enemy, _spawnPoint.position, Quaternion.identity, _spawnPoint);
        enemy.Init(_target);
        _aliveEnemy++;
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _aliveEnemy--;
        _target.AddMoney(enemy.Reward);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _timeAfterWave = _currentWave.TimeToNextWave;
    }
}

[Serializable]
class Wave
{
    public Enemy Enemy;
    public float Delay;
    public float TimeToNextWave;
    public int Count;
}
