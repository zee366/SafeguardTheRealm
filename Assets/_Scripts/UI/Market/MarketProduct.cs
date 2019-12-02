using System;

/// <summary>
/// Struct to store information of a product that will be present in the store
/// All meta data assigned to product
/// </summary>
[Serializable]
public struct MarketProduct {

    public MarketProduct(Tower product, int rarity, int price) {
        this.product = product;
        this.rarity = rarity;
        this.price = price;
    }

    public Tower product;
    public int        rarity;
    public int        price;

}