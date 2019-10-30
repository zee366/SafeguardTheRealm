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
    List<Tower> towers = new List<Tower>();

    List<int> tiersIndex = new List<int>();

    System.Random rand = new System.Random();

    // used for testing
    private float _t = 0;
    List<GameObject> instances = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        AddTowers();
        //SpawnTowers();
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
    public List<Tower> GetTowers() {
        return this.towers;
    }
    public List<int> GetTowerTiers() {
        return this.tiersIndex;
    }
    // will add the towers 
    public void AddTowers()
    {
        /* used for testing
        foreach (var inst in instances)
            Destroy(inst);
            */

        tiersIndex = new List<int>();
        towers = new List<Tower>();
        for (int i = 0; i < 3; i++)
        {
            int index = GetTowerLevel();
            int tiersTowerRandom = Random.Range(0, tiers[index].towers.Count);
            towers.Add(tiers[index].towers[tiersTowerRandom]);
            tiersIndex.Add(index+1);
        }

    }


    // spawn the towers in the scene
    private void SpawnTowers() {
        int x = 2;
        int y = 2;
        foreach (Tower tower in towers) {
            Instantiate(tower, new Vector2(x, y), Quaternion.identity);
            //instances.Add(Instantiate(tower, new Vector2(x, y), Quaternion.identity));
            x++;
            y++;
        }
    }
    // tells which tower tier was weighted choosen
    private int GetTowerLevel() {
        int playaLevel = GetPlayerLevel();
        int wRandomNumber = RandomValue();
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

    private int RandomValue() {
        int x = rand.Next(101);
        return x;
    }
    //WIP
    private int GetPlayerLevel() {
        //Player.level;
        return 0;
    }
}
