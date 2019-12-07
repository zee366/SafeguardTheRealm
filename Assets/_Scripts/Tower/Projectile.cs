using UnityEngine;

/// <summary>
/// Hovering projectile behaviour that with go towards his target.
/// On hit try to deal damage
/// </summary>
public class Projectile : MonoBehaviour {

    public float speed;
    public int damage = 1;
    private Enemy _enemy;
    private Vector3 _lastEnemyPosition;
    private Transform _target;


    public void SetEnemy(Enemy go) {
        _enemy = go;
        _target = go.transform;
        foreach( Transform t in go.transform)
        {
            if(t.tag == "EnemyCollider")
            {
                _target = t;
                break;
            }
        }
    }

    void FixedUpdate() {
        if ( _target == null ) {
            MoveTowardsPosition(_lastEnemyPosition);
            if ( transform.position == _lastEnemyPosition ) {
                Destroy(gameObject);
            }

            return;
        }
        
        MoveTowardsEnemy();
    }

    private void MoveTowardsEnemy() {
        float step = 0.1f;
      
        _lastEnemyPosition = _target.position;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
    }

    private void MoveTowardsPosition(Vector3 _lastEnemyPosition) {
        float step = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, _lastEnemyPosition, step);
    }

    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "EnemyCollider") {
            if(_enemy) {
                _enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
