using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon
{
    [SerializeField] private float _turnTime;

    private WaitForSeconds _sleepTime;

    private void Awake()
    {
        _sleepTime = new WaitForSeconds(_turnTime);
    }

    public override void Shoot(Transform shootPosition)
    {
        StartCoroutine(Turn(shootPosition));
    }

    private IEnumerator Turn(Transform shootPosition)
    {
        Instantiate(Bullet, shootPosition);

        yield return _sleepTime;

        Instantiate(Bullet, shootPosition);

        yield return _sleepTime;

        Instantiate(Bullet, shootPosition);
    }
}
