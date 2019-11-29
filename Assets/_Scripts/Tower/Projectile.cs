using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public int damage =1 ;
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
        };
        
        MoveTowardsEnemy();
    }
    private void MoveTowardsEnemy() {
//        float step =  speed * Time.deltaTime; // calculate distance to move
        float step = 0.1f;
        _lastEnemyPosition = _target.position;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
    }
    
    private void MoveTowardsPosition(Vector3 _lastEnemyPosition) {
        float step = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, _lastEnemyPosition, step);
    }

    
    private void OnTriggerEnter(Collider other) {
        if ( _enemy ) {
            _enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
