using System.Collections.Generic;
using System;

/// <summary>
/// Rarity level struct to list towers corresponding to tier & their price
/// </summary>
[Serializable]
public struct TowerTier
{
    public List<Tower> towers;
	public int price;

}