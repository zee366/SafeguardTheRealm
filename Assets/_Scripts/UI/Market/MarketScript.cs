using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : MonoBehaviour {

    public Player playerReference;

    // It will define the probability to all the player levels and will be accessed by the inspector
    [SerializeField] List<Vector3> playerLevelTiersProbabilities = new List<Vector3>();

    // the tier struct with the price and the tower list
    [SerializeField] List<TowerTier> tiers = new List<TowerTier>();

    // The Objects currently in the market
    private List<MarketProduct> _products = new List<MarketProduct>();

    System.Random rand = new System.Random();


    // Start is called before the first frame update
    void Start() { GenerateTowersList(); }


    // will add the towers
    public void GenerateTowersList() {
        _products = new List<MarketProduct>();
        for ( int i = 0; i < 3; i++ ) {
            int index            = GetTowerLevel();
            int tiersTowerRandom = Random.Range(0, tiers[index].towers.Count);
            MarketProduct p = new MarketProduct(tiers[index].towers[tiersTowerRandom], index, tiers[index].price);
            _products.Add(p);
        }
    }

    // tells which tower tier was weighted choosen
    private int GetTowerLevel() {
        int playaLevel    = GetPlayerLevel() - 1;    // Starts at 1
        int wRandomNumber = randomValue();

        float tierOneEndPointProbability = playerLevelTiersProbabilities[playaLevel].x;
        float tierTwoEndPointProbability = tierOneEndPointProbability + playerLevelTiersProbabilities[playaLevel].y;


        if ( wRandomNumber <= tierOneEndPointProbability )
            return 0;

        if ( wRandomNumber > tierOneEndPointProbability && wRandomNumber <= tierTwoEndPointProbability )
            return 1;

        if ( wRandomNumber > tierTwoEndPointProbability )
            return 2;

        return 0;
    }


    private int randomValue() {
        int x = rand.Next(101);
        return x;
    }

    private int GetPlayerLevel() { return playerReference.playerLevel; }


    public List<MarketProduct> GetTowers() { return _products; }

}