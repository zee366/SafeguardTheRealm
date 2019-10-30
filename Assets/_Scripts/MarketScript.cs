using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarketScript : MonoBehaviour
{
   
    Player player;
    
   
    // It will define the probability to all the player levels and will be accessed by the inspector
    [SerializeField]
    List<Vector3> playerLevelTiersProbabilities = new List<Vector3>();
    // the tier struct with the price and the tower list
    [SerializeField]
    List<TowerTier> tiers = new List<TowerTier>();
    // the towers game objects
    [SerializeField]
    List<GameObject> towers = new List<GameObject>();

    System.Random rand = new System.Random();

    // used for testing
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
        /* used for testing
        if (_t > 3f)
        {
            addTowers();
            spawnTowers();
            _t = 0;
        }
        _t += Time.deltaTime;
        */
    }

    // will add the towers 
    private void addTowers()
    {
        /* used for testing
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
    // spawn the towers in the scene
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
    // tells which tower tier was weighted choosen
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
