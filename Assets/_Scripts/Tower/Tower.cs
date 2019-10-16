using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour {

    [Range(2f, 20f)]
    public float radius;
    [Range(0.1f, 4f)]
    public float attackSpeed;
    public Projectile projectile;
    public List<Transform> _towerWeaponPosition;

    private GameObject _enemyBeingTargetted;
    private GameObject _radiusObjet;
    private Dictionary< int , GameObject> _enemyInRange;
    private List<Transform> _towerWeaponVisuals;
    
    private bool projectileCoroutineStarted = false;
    private Timer _timer;

    void Start() {
        _radiusObjet = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
        _enemyInRange = new Dictionary<int, GameObject>();
        
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
        if ( _enemyInRange.Count > 0 ) {
            GameObject enemy = findClosestEnemy();
           _enemyBeingTargetted = enemy;
           LookAtEnemy();
           if (!projectileCoroutineStarted) StartCoroutine(ProjectileCoroutine());
        } else {
            StopAllCoroutines();
        }
    }


    IEnumerator ProjectileCoroutine() {
        projectileCoroutineStarted = true;
        sendProjectile(_enemyBeingTargetted);
        while ( true ) {
            yield return new WaitForSeconds(attackSpeed);
            sendProjectile(_enemyBeingTargetted);
        }
    }
    public void AddEnemyToMemory(Transform t) {
        _enemyInRange.Add(t.gameObject.GetInstanceID(), t.gameObject);
    }


    public void RemoveEnemyFromMemory(Transform t) {
        _enemyInRange.Remove(t.gameObject.GetInstanceID()); 
    }


    GameObject findClosestEnemy() {
        GameObject target = null;
        
        foreach ( KeyValuePair<int, GameObject> idEnemyPair in _enemyInRange ) {
            target = idEnemyPair.Value;
            break;

            //TODO: FIND CLOSEST ON SPLINE
        }
        
        return target;
    }
    
    private void sendProjectile(GameObject enemy) {
        foreach ( Transform weaponPosition in _towerWeaponPosition ) {
            Projectile _projectile = Instantiate(projectile, weaponPosition.position, weaponPosition.rotation);
            _projectile.setEnemy(enemy);
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
