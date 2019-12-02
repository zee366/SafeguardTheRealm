using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays player information in the UI.
/// Triggered by external UnityEvents
/// </summary>
public class PlayerUIEvent : MonoBehaviour {

    // Associated game attribute value members
    int mGameRound;
    int mPlayerLevel;
    int mPlayerGold;

    // Associated game attribute text members in actual UI
    public Text mGameRoundText;
    public Text mPlayerLevelText;
    public Text mPlayerGoldText;


    // Changes game UI's round to match the GameController's attribute when it changes
    public void ChangeGameRound(WaveManager waveManagerRef) {
        mGameRound          = waveManagerRef.GetWaveNumber();
        mGameRoundText.text = mGameRound.ToString();
    }


    // Changes player UI's level to match the Player's attribute when it changes
    public void ChangePlayerLevel(Player playerRef) {
        mPlayerLevel          = playerRef.GetPlayerLevel();
        mPlayerLevelText.text = mPlayerLevel.ToString();
    }


    // Changes player UI's gold to match the Player's attribute when it changes
    public void ChangePlayerGold(Player playerRef) {
        mPlayerGold          = playerRef.GetPlayerGold();
        mPlayerGoldText.text = mPlayerGold.ToString();
    }

}