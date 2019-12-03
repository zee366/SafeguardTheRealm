using System;

/// <summary>
/// Struct sent from affectors to modify target tower stats at runtime
/// </summary>
[Serializable]
public struct TowerModifier {

    public TowerModifier(float speedModifier, float damageModifier) {
        this.speedModifier = speedModifier;
        this.damageModifier = damageModifier;
    }

    public float speedModifier;
    public float damageModifier;

}