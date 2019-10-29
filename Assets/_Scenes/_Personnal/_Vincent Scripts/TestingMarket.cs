using System.Collections.Generic;
using UnityEngine;

public class TestingMarket : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    //mocking the player level
    int playerLevel = 1;
    //mocking the tower tier
    char towerLevel;

    //probability to show the towers
    List<int> level1 = new List<int> { 75, 25, 0 };
    List<int> level2 = new List<int> { 50, 45, 5 };
    List<int> level3 = new List<int> { 30, 55, 15 };
    List<int> level4 = new List<int> { 25, 45, 30 };
    List<int> level5 = new List<int> { 15, 40, 45 };


    List<GameObject> towersA;
    List<GameObject> towersB;
    List<GameObject> towersC;

    // Don't know how these look in final Market script, but needed for UI
    List<GameObject> mChosenTowers;
    List<string> mChosenNames;
    List<int> mChosenTowersCost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getTowerLevel()
    {
        int playaLevel = getPlayerLevel();
        int wRandomNumber = randomValue();

        switch (playaLevel)
        {
            case 1:
                if (wRandomNumber <= level1[0]) { towerLevel = 'A'; }
                if (wRandomNumber > level1[0]) { towerLevel = 'B'; }
                break;
            case 2:
                if (wRandomNumber <= level2[0]) { towerLevel = 'A'; }
                if ((wRandomNumber > level2[0]) && (randomValue() <= level2[1])) { towerLevel = 'B'; }
                if (wRandomNumber > level2[1]) { towerLevel = 'C'; }
                break;
            case 3:
                if (wRandomNumber <= level3[0]) { towerLevel = 'A'; }
                if ((wRandomNumber > level3[0]) && (randomValue() <= level3[1])) { towerLevel = 'B'; }
                if (wRandomNumber > level3[1]) { towerLevel = 'C'; }
                break;
            case 4:
                if (wRandomNumber <= level4[0]) { towerLevel = 'A'; }
                if ((wRandomNumber > level4[0]) && (randomValue() <= level4[1])) { towerLevel = 'B'; }
                if (wRandomNumber > level4[1]) { towerLevel = 'C'; }
                break;
            case 5:
                if (wRandomNumber <= level5[0]) { towerLevel = 'A'; }
                if ((wRandomNumber > level5[0]) && (randomValue() <= level5[1])) { towerLevel = 'B'; }
                if (wRandomNumber > level5[1]) { towerLevel = 'C'; }
                break;
        }
    }

    private int whichTower()
    {
        System.Random rand = new System.Random();
        int x = rand.Next(1);
        return x;
    }

    private int randomValue()
    {
        System.Random random = new System.Random();
        int x = random.Next(101);
        return x;
    }

    private int getPlayerLevel()
    {
        return playerLevel;
    }

    //Don't know how this will look in final version of Market script, but should be here
    public List<GameObject> GetThreeChosenTowers()
    {
        return mChosenTowers;
    }
    
    //Don't know how this will look in final version of Market script, but should be here
    public List<string> GetThreeChosenTowersNames()
    {
        return mChosenNames;
    }

    //Don't know how this will look in final version of Market script, but should be here
    public List<int> GetThreeChosenTowersCostInfo()
    {
        return mChosenTowersCost;
    }

    //Don't know how this will look in final version of Market script, but should be here
    public void GenerateNewListOfTowers(ref List<GameObject> listOfTowers)
    {
        // Generate a new randomized list of towers logic and apply it to the list passed by reference...
        //listOfTowers = ...;
    }

}
