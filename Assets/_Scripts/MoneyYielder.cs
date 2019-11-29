using UnityEngine;

public class MoneyYielder : MonoBehaviour
{
    const float INTEREST = 0.25f;

    int _winStreak { get; set; } = 0;
    int _loseStreak { get; set; } = 0;

    Player _player;

    const int END_OF_ROUND_WINNINGS = 1;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    public void GiveGoldToPlayer(int value) {
        _player.GainGold(value);
    }

    public void ResetWinStreak() {
        _winStreak = 0;
        _loseStreak++;
    }

    public void ResetLoseStreak() {
        _loseStreak = 0;
        _winStreak++;
    }

    public void EndOfRoundWinnings() {
        //GiveGoldToPlayer((++_winStreak) * END_OF_ROUND_WINNINGS);

        // Calculate the interest then give the total to the player
        int gold = _player.GetPlayerGold() + ((++_winStreak) * END_OF_ROUND_WINNINGS);
        GiveGoldToPlayer(Mathf.RoundToInt(gold * INTEREST) + ((_winStreak) * END_OF_ROUND_WINNINGS));
    }
}
