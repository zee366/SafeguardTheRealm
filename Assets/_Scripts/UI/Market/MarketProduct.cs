using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MarketProduct {

    public MarketProduct(GameObject product, int rarity, int price) {
        this.product = product;
        this.rarity = rarity;
        this.price = price;
    }

    public GameObject product;
    public int        rarity;
    public int        price;

}