using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int maxLevel = 5;
    public int playerLevel = 1;
    public int playerGold = 0;
    public UnityEvent OnLevelUp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gain level
    public void gainLevel(int cost)
    {
        //Check if Player's level is MAX
        if (checkLevel())
        {
            //Check if Player's gold is enough or not
            if (checkGold(cost)) //If enough gold, take gold off from Player
            {
                removeGold(cost);
                addOneLevel();            
                OnLevelUp.Invoke();
            }
            else
            {
                //TODO
                //An output that shows player's gold is not enough to buy level-up
                Debug.Log("Not enough gold");
            }
        }
        else
        {
            //TODO
            //An output saying player's level is max
            Debug.Log("Player's level has already reached max");
        }          
    }

    //method: Gain gold
    public void gainGold(int gold)
    {
        playerGold += gold;
    }

    //method: check if gold is enough 
    public bool checkGold(int gold)
    {
        if (playerGold >= gold)
            return true;

        return false;
    }

    //method: remove gold 
    public void removeGold(int gold)
    {
        playerGold -= gold;
    }
    //method: check is player level is max
    public bool checkLevel()
    {
        if (playerLevel < maxLevel)
            return true;

        return false;
    }
    //method: increase level by 1
    public void addOneLevel()
    {
        playerLevel++;
    }
    //gainLevel event
    void levelUp()
    {
        Debug.Log("Player levels up");
    }

    //gainGold event
    void goldChange()
    {
        Debug.Log("Gold changes");
    }

    //Getter for Return Player's level
    public int getPlayer_Level()
    {
        return playerLevel;
    }
}
