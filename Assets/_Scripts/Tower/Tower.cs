using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour {

    [Range(2f, 20f)]
    public float radius;
    public Sprite thumbnail;

    private List<Attacker> _attackers;
    private Enemy _enemyBeingTargetted;
    private Enemy _nextTarget;
    private GameObject _radiusObjet;
    private Dictionary< int , Enemy> _enemyInRange;
    private List<Transform> _towerWeaponVisuals;

    private Timer _timer;


    void Awake() {
        _attackers = new List<Attacker>(GetComponentsInChildren<Attacker>());
        _radiusObjet                      = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
        _enemyInRange                     = new Dictionary<int, Enemy>();
        
        //Find all the tower's weapon
        _towerWeaponVisuals = new List<Transform>();
        foreach ( Transform child in GetComponentsInChildren<Transform>() ) {
            if ( child.transform.CompareTag("weapon") ) {
                _towerWeaponVisuals.Add(child);
            }
        }
    }
    

    // Update is called once per frame
    void Update() {
        if ( _enemyInRange.Count <= 0 ) return; // if there is no enemy

        FindClosestEnemy();
        if ( _nextTarget != _enemyBeingTargetted ) {
            AlertAttacker(_nextTarget);
            _enemyBeingTargetted = _nextTarget;
        }
        LookAtEnemy();
    }
    
    public void AddEnemyToMemory(Transform t) {
        _enemyInRange.Add(t.gameObject.GetInstanceID(), t.gameObject.GetComponentInParent<Enemy>());
    }


    public void RemoveEnemyFromMemory(Transform t) {
        StopAttacker(t.gameObject.GetComponentInParent<Enemy>());
        _enemyInRange.Remove(t.gameObject.GetInstanceID()); 
    }


    void FindClosestEnemy() {
        Enemy closest_target = null;
        bool firstPass = true;
        foreach ( KeyValuePair<int, Enemy> idEnemyPair in _enemyInRange ) {
            Enemy target = idEnemyPair.Value;

            if (target == null) // this is a hack for the moment that I know better
            {
                _enemyInRange.Remove(idEnemyPair.Key); 
                FindClosestEnemy();
                return;
            }
            
            if ( firstPass ) { 
                closest_target = target;
                firstPass = false;
            }

            if ( target.GetProgress() < closest_target.GetProgress() ) {
                closest_target = target;
            }
        }

        _nextTarget = closest_target;
    }

    private void AlertAttacker(Enemy enemy) {
        foreach (Attacker attacker in _attackers ) {
            attacker.Attack(enemy);
        }
    }


    private void StopAttacker(Enemy enemy) {
        foreach (Attacker attacker in _attackers ) {
            attacker.Stop(enemy);
        }
    }
    
    private void LookAtEnemy() {
        Vector3 enemyPosition = _enemyBeingTargetted.transform.position;
        foreach ( Transform weapon in _towerWeaponVisuals ) {
            Vector3 targetPostition = new Vector3( enemyPosition.x, 
                                                   weapon.position.y, 
                                                   enemyPosition.z ) ;
            weapon.LookAt( targetPostition ) ;
        }
    }
}
