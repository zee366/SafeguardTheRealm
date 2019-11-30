﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    [SerializeField] private Projectile projectile;
    public int damage = 1;
    [Range(0.1f, 4f)] public float projectileSpeed = 2;
    public float attackSpeed = 1;
    protected Enemy _currentEnemy;
    protected Coroutine _currentCoroutine;
    protected bool _projectileCoroutineStarted = false;
    protected TowerModifier modifier = new TowerModifier(1, 1);


    public void Attack(Enemy enemy) {
        if(_projectileCoroutineStarted && enemy != _currentEnemy) StopCoroutine(_currentCoroutine);
        _currentEnemy = enemy;
        _currentCoroutine = StartCoroutine(ProjectileCoroutine());
    }


    public void Stop(Enemy enemy) {
        if(enemy == _currentEnemy) {
            StopCoroutine(_currentCoroutine);
        }
    }


    protected virtual void SendProjectile() {
        if(_currentEnemy == null) {
            return;
        }

        Projectile p = Instantiate(projectile, transform.position, transform.rotation);
        p.SetEnemy(_currentEnemy);
        p.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        p.speed = projectileSpeed;
    }


    protected virtual IEnumerator ProjectileCoroutine() {
        _projectileCoroutineStarted = true;
        SendProjectile();
        while(true) {
            float speed = attackSpeed * modifier.speedModifier;
            if(Mathf.Approximately(speed, 0.0f))
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForSeconds(1 / speed);

            SendProjectile();
        }
    }


    /// <summary>
    /// Will be called by the supports towers
    /// </summary>
    /// <param name="modifier"></param>
    public void ApplyAttackerModifier(TowerModifier modifier) {
        this.modifier.damageModifier += modifier.damageModifier - 1;
        this.modifier.speedModifier += modifier.speedModifier - 1;
    }


    public TowerModifier GetModifier() { return modifier; }

}