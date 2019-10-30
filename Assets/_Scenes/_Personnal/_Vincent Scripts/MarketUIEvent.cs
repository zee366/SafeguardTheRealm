﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIEvent : MonoBehaviour
{
    // Get associated player, inventory, and market objects
    public TestingPlayer mPlayer;           //TODO change to actual class
    public TestingInventory mInventory;     //TODO change to actual class
    public MarketScript mMarket;           //TODO change to actual class
    

    // Containers for chosen tower objects and their info
    List<Tower> mChosenTowers;
    List<int> mChosenTowersCosts;
    
    // Cost of a reroll is always 1 gold
    const int REROLL_COST = -1;
    public Text mRerollButtonText;

    // Get associated gain level button and its associated level cost text
    public Button mLevelUpButton;
    //public Button MLevelUpButton { get => mLevelUpButton; set => mLevelUpButton = value; }    //better?
    public Text mLevelUpAmountText;

    // Attributes associated with cost of gaining a level
    int mGainLevelCost = -2;


    // Attributes for storing the tower type and level
    string mTowerSlot1Info;
    string mTowerSlot2Info;
    string mTowerSlot3Info;

    // Attributes for displaying the tower type and level in the MarketUI
    public Text mTowerSlot1InfoText;
    public Text mTowerSlot2InfoText;
    public Text mTowerSlot3InfoText;
    
    // Attributes for storing the tower cost
    int mTowerSlot1Cost;
    int mTowerSlot2Cost;
    int mTowerSlot3Cost;

    // Attributes for displaying the tower cost in the MarketUI
    public Text mTowerSlot1CostText;
    public Text mTowerSlot2CostText;
    public Text mTowerSlot3CostText;

    // Start is called before the first frame update
    void Start()
    {
        /* Initialize the chosen towers and the associated cost texts of the buttons
        mChosenTowers = mMarket.GetTowers();
        mRerollButtonText.text = "REROLL: " + REROLL_COST.ToString();
        mLevelUpAmountText.text = mGainLevelCost.ToString();
        mChosenTowersCosts = mMarket.GetTowerTiers(); */
        
    }

    private void Update()
    {
        // Initialize the chosen towers and the associated cost texts of the buttons
        mChosenTowers = mMarket.GetTowers();
        mRerollButtonText.text = "REROLL: " + REROLL_COST.ToString();
        mLevelUpAmountText.text = mGainLevelCost.ToString();
        mChosenTowersCosts = mMarket.GetTowerTiers();
        DisplayTowerSlotsInfo();
        DisplayTowerCostForButtons(); 
    }

    // Shows the info of each chosen tower in format of type and level
    public void DisplayTowerSlotsInfo()
    {
        mTowerSlot1InfoText.text = mChosenTowers[0].towerName;
        mTowerSlot2InfoText.text = mChosenTowers[1].towerName;
        mTowerSlot3InfoText.text = mChosenTowers[2].towerName;
    }

    // Shows the info of each chosen tower's cost for their appropriate button
    public void DisplayTowerCostForButtons()
    {
        mTowerSlot1CostText.text = mChosenTowersCosts[0].ToString();
        mTowerSlot2CostText.text = mChosenTowersCosts[1].ToString();
        mTowerSlot3CostText.text = mChosenTowersCosts[2].ToString();
    }

    // Allows player to generate a new list of towers to choose from
    public void RerollTheMarket()
    {
        if (mPlayer.checkGold(REROLL_COST))     //check if player has enough gold to do this, if not, do nothing
        {
            mPlayer.removeGold(REROLL_COST);
            mMarket.AddTowers();
            mChosenTowers = mMarket.GetTowers();
            mChosenTowersCosts = mMarket.GetTowerTiers();
            DisplayTowerSlotsInfo();
            DisplayTowerCostForButtons();
        }
    }

    // Following 3 functions will be selected based on the 3 buttons of the market
    // They will add the appropriate tower to the inventory if the player has enough gold to purchase them
    public void AddTowerFromSlot1ToInventory()
    {
        if (mPlayer.checkGold(mChosenTowersCosts[0]))   //check if player has enough gold to do this, if not, do nothing
        {
            //mInventory.Add(mChosenTowers[0]);
            mPlayer.removeGold(mChosenTowersCosts[0]);
        }
    }
    public void AddTowerFromSlot2ToInventory()
    {
        if (mPlayer.checkGold(mChosenTowersCosts[1]))   //check if player has enough gold to do this, if not, do nothing
        {
            //mInventory.Add(mChosenTowers[1]);
            mPlayer.removeGold(mChosenTowersCosts[1]);
        }
    }
    public void AddTowerFromSlot3ToInventory()
    {
        if (mPlayer.checkGold(mChosenTowersCosts[2]))   //check if player has enough gold to do this, if not, do nothing
        {
            //mInventory.Add(mChosenTowers[2]);
            mPlayer.removeGold(mChosenTowersCosts[2]);
        }
    }

    // Allows player to buy a new level
    public void BuyPlayerLevel() //TODO change to actual player class name
    {
        if (mPlayer.checkGold(mGainLevelCost))    //check if player has enough gold to do this, if not, do nothing
        {
            mPlayer.gainLevel(mGainLevelCost);
            mGainLevelCost--;
        }
    }

    // TODO
    // Is following function to be handled here or in gamecontroller?
    // Function for when player is done using the market UI
    public void SelectedDone()
    {
        // Logic for when button done is pressed
        // Handle here or in game controller?
    }
}
