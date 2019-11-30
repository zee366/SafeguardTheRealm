using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIEvent : MonoBehaviour {

    // Get associated player, inventory, and market objects
    public Player       player;
    public Inventory    inventory;
    public MarketScript market;

    // Cost of a reroll is always 1 gold
    const  int  REROLL_COST = 1;
    public Text mRerollButtonText;
    public Text mLevelUpAmountText;

    // Attributes associated with cost of gaining a level
    int _gainLevelCost = 2;

    private MarketSlot[] _slots;


    // Start is called before the first frame update
    private void Awake() {
        // References to slots + events
        _slots = GetComponentsInChildren<MarketSlot>();
        foreach ( MarketSlot slot in _slots ) {
            slot.onTryBuying.AddListener(OnSlotClicked);
        }

        mRerollButtonText.text  = REROLL_COST.ToString();
        mLevelUpAmountText.text = _gainLevelCost.ToString();

        market.GenerateTowersList();
        UpdateMarketSlots(market.GetTowers());
    }


    /// <summary>
    /// Send product data to UI market slot
    /// </summary>
    /// <param name="products"></param>
    private void UpdateMarketSlots(List<MarketProduct> products) {
        for ( int i = 0; i < products.Count || i < _slots.Length; i++ ) {
            _slots[i].Enable();
            _slots[i].SetProduct(products[i]);
        }
    }


    // Allows player to generate a new list of towers to choose from
    // Called from button event
    public void RerollTheMarket() {
        if ( player.CheckGold(REROLL_COST) ) // check if player has enough gold to do this, if not, do nothing
        {
            player.RemoveGold(REROLL_COST);
            market.GenerateTowersList();
            UpdateMarketSlots(market.GetTowers());
        } else {
            // TODO: Cant reroll
        }
    }
     
    // Renew the market when each round ends
    public void RenewMarket()
    {
            market.GenerateTowersList();
            UpdateMarketSlots(market.GetTowers());     
    }


    /// <summary>
    /// When a market product button is clicked
    /// </summary>
    /// <param name="clicked"></param>
    private void OnSlotClicked(MarketSlot clicked) {
        if ( player.CheckGold(clicked.GetProduct().price) ) {
            clicked.Disable();
            player.RemoveGold(clicked.GetProduct().price);

            // Need to Instantiate the product first (Not Inventory's job)
            GameObject yield = Instantiate(clicked.GetProduct().product.gameObject, Vector3.zero, Quaternion.identity);
            inventory.Add(yield);
        }
    }


    // Allows player to buy a new level
    public void BuyPlayerLevel() {
        if ( player.CheckGold(_gainLevelCost) ) //check if player has enough gold to do this, if not, do nothing
        {
            player.GainLevel(_gainLevelCost);
            _gainLevelCost++;
        }
    }

}