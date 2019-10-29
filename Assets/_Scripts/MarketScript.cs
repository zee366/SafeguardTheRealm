using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarketScript : MonoBehaviour
{
   
    Player player;
    
   

    [SerializeField]
    List<Vector3> playerLevelTiersProbabilities = new List<Vector3>();

    [SerializeField]
    List<TowerTier> tiers = new List<TowerTier>();

    [SerializeField]
    List<GameObject> towers = new List<GameObject>();

    System.Random rand = new System.Random();

    private float _t = 0;
    List<GameObject> instances = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        addTowers();
        spawnTowers();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_t > 3f)
        {
            addTowers();
            spawnTowers();
            _t = 0;
        }
        _t += Time.deltaTime;
        */
    }

    private void addTowers()
    {
        /*
        foreach (var inst in instances)
            Destroy(inst);
            */

        towers = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            int index = getTowerLevel();
            int tiersTowerRandom = Random.Range(0, tiers[index].towers.Count);
            towers.Add(tiers[index].towers[tiersTowerRandom]);
        }

    }

    private void spawnTowers() {
        int x = 2;
        int y = 2;
        foreach (GameObject tower in towers) {
            Instantiate(tower, new Vector2(x, y), Quaternion.identity);
            //instances.Add(Instantiate(tower, new Vector2(x, y), Quaternion.identity));
            x++;
            y++;
        }
    }

    private int getTowerLevel() {
        int playaLevel = getPlayerLevel();
        int wRandomNumber = randomValue();
        //Debug.Log(wRandomNumber);

        float tierOneEndPointProbability = playerLevelTiersProbabilities[playaLevel].x;
        float tierTwoEndPointProbability = tierOneEndPointProbability + playerLevelTiersProbabilities[playaLevel].y;
        

        if (wRandomNumber <= tierOneEndPointProbability)
            return 0;
        if (wRandomNumber > tierOneEndPointProbability && wRandomNumber <= tierTwoEndPointProbability) 
            return 1;
        if (wRandomNumber > tierTwoEndPointProbability)
            return 2;

        return 0;
    }

    private int randomValue() {
        int x = rand.Next(101);
        return x;
    }
    //WIP
    private int getPlayerLevel() {
        //Player.level;
        return 3;
    }
}
