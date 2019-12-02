using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Player script is holding informations regarding the player state, not behaviors.
/// Money, level, and events
/// </summary>
public class Player : MonoBehaviour {

    public int        maxLevel    = 5;
    public int        playerLevel = 1;
    public int        playerGold  = 0;
    public UnityEvent onLevelUp;    //On level Up event
    public UnityEvent onGoldChange; //On player's gold Changes event


    private void Start() {
        // Trigger some of of the events to update UI
        onLevelUp.Invoke();
        onGoldChange.Invoke();
    }


    /// <summary>
    /// Buy a level, and deduct money
    /// </summary>
    /// <param name="cost"></param>
    public void GainLevel(int cost) {
        //Check if Player's level is MAX
        if ( CheckLevel() ) {
            //Check if Player's gold is enough or not
            if ( CheckGold(cost) ) //If enough gold, take gold off from Player
            {
                RemoveGold(cost);
                AddOneLevel();
            } else {
                //TODO
                //An output that shows player's gold is not enough to buy level-up
                Debug.Log("Not enough gold");
            }
        } else {
            //TODO
            //An output saying player's level is max
            Debug.Log("Player's level has already reached max");
        }
    }


    /// <summary>
    /// Check if has enough gold
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public bool CheckGold(int gold) {
        if ( playerGold >= gold )
            return true;

        return false;
    }


    /// <summary>
    /// Add gold to the player
    /// </summary>
    /// <param name="gold"></param>
    public void GainGold(int gold) {
        playerGold += gold;
        onGoldChange.Invoke();
    }


    /// <summary>
    /// Deduct gold to player
    /// </summary>
    /// <param name="gold"></param>
    public void RemoveGold(int gold) {
        playerGold -= gold;
        onGoldChange.Invoke();
    }


    /// <summary>
    /// Check if reached max level
    /// </summary>
    /// <returns></returns>
    public bool CheckLevel() {
        if ( playerLevel < maxLevel )
            return true;

        return false;
    }

    public void AddOneLevel() {
        playerLevel++;
        onLevelUp.Invoke();
    }


    // Getter for Return Player's level
    public int GetPlayerLevel() { return playerLevel; }


    // Getter for Return Player's gold
    public int GetPlayerGold() { return playerGold; }

}