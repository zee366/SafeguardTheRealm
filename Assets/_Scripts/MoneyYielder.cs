using UnityEngine;

public class MoneyYielder : MonoBehaviour
{
    int _winStreak { get; set; } = 0;
    int _loseStreak { get; set; } = 0;

    GameObject _player;

    const int END_OF_ROUND_WINNINGS = 1;

    void Start()
    {
        _player = GameObject.Find("Player");
    }
    /*
    public void GiveGoldToPlayer(int value) {
        _player.GetComponent<Player>().gainGold(value);
    }
    */

    public void ResetWinStreak() {
        _winStreak = 0;
        _loseStreak++;
    }

    public void ResetLoseStreak() {
        _loseStreak = 0;
        _winStreak++;
    }
    /*
    public void EndOfRoundWinnings() {
        GiveGoldToPlayer((_winStreak + 1) * END_OF_ROUND_WINNINGS);
    }
    */
}
