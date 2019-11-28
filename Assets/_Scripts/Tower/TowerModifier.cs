using System;

[Serializable]
public struct TowerModifier {

    public TowerModifier(float speedModifier, float damageModifier) {
        this.speedModifier = speedModifier;
        this.damageModifier = damageModifier;
    }

    public float speedModifier;
    public float damageModifier;

}