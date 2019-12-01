using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField]
    private EnemyHealthBar _enemyHealthBarPrefab;

    private Dictionary<Enemy, EnemyHealthBar> _enemyHealthBars = new Dictionary<Enemy, EnemyHealthBar>();

    private void Awake()
    {
        Enemy.OnHealthAdded +=
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddHealthBar(Enemy health)
    {
        if (_enemyHealthBars.ContainsKey(health) == false)
        {
            var _enemyHealthBar = Instantiate(_enemyHealthBarPrefab, transform);
            _enemyHealthBars.Add(health, _enemyHealthBar);
            _enemyHealthBar.SetHealth(health);
        }
    }

    private void RemoveHealthBar(Enemy health)
    {
        if (_enemyHealthBars.ContainsKey(health))
        {
            //Destroy(_enemyHealthBars(health).gameObject);
            _enemyHealthBars.Remove(health);
        }
    }

}
