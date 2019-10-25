using System.Collections;
using System.Collections.Generic;
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
    public void gainLevel(int gold)
    {
        //Check if Player's level is MAX
        if (playerLevel < maxLevel)
        {
            //Check if Player's gold is enough or not
            if (playerGold >= gold) //If enough gold, take gold off from Player
            {
                playerGold -= gold;
                
                playerLevel++;
             
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

    //Gain gold
    public void gainGold(int gold)
    {
        playerGold += gold;
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

    //Return Player's level
    private int getPlayer_Level()
    {
        return playerLevel;
    }
}
