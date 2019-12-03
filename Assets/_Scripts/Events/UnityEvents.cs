using UnityEngine;
using UnityEngine.Events;

namespace Events {
    [System.Serializable]
    public class TUnityEvent : UnityEvent<Transform> { }

    [System.Serializable]
    public class GUnityEvent : UnityEvent<GameObject> { }

    [System.Serializable]
    public class SUnityEvent : UnityEvent<ScriptableObject> { }

    [System.Serializable]
    public class MarketSlotEvent : UnityEvent<MarketSlot> { }
}