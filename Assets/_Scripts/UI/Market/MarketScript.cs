using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle market refreshing mechanics and product data for UI to display.
/// Propability based and vary with player's level. (Values from inspector)
/// Dont display anything itself, container for current product in store.
/// </summary>
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


    /// <summary>
    /// Generate 3 new towers products that can be available.
    /// </summary>
    public void GenerateTowersList() {
        _products = new List<MarketProduct>();
        for ( int i = 0; i < 3; i++ ) {
            int index            = GetTowerLevel();
            int tiersTowerRandom = Random.Range(0, tiers[index].towers.Count);
            MarketProduct p = new MarketProduct(tiers[index].towers[tiersTowerRandom], index, tiers[index].price);
            _products.Add(p);
        }
    }

    /// <summary>
    /// Tells which tower tier was choosen base on weight
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Return the current product's data in the market
    /// </summary>
    /// <returns></returns>
    public List<MarketProduct> GetTowers() { return _products; }

}