using UnityEngine;

public class MoneyYielder : MonoBehaviour
{
    const float INTEREST_RATE = 0.25f;
    int castle_MAX_HP;
    GameObject castle;
    Castle c;

    int _winStreak { get; set; } = 0;
    int _loseStreak { get; set; } = 0;

    // get castle's hp at beginning of the round
    int old_castleHP { get; set; } = 0;

    Player _player;

    const int END_OF_ROUND_WINNINGS = 1;

    void Start()
    {
        _player = GetComponent<Player>();
        castle = GameObject.Find("Castle");
        c = castle.GetComponent<Castle>();
        castle_MAX_HP = c.GetHealth();
    }

    // Set castleHP at begging of the round
    // Call this method on "On Start Wave" Unity Event
    public void SetCastleHP()
    {
        old_castleHP = c.GetHealth();
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

    // Determine WinStreak/LoseStreak
    public void CheckStreak()
    {
        int new_castleHP = c.GetHealth();
        int x = old_castleHP - new_castleHP;

        // If at the end of the round, castle's hp does not lose more than 10% of total hp, increments win streak and reset lose streak
        if ((x * 1.0) / (castle_MAX_HP * 1.0) < 0.1f)
        {
            ResetLoseStreak();
        }
        // If at the end of the round, castle's hp loses 10% or more of total hp, increments lose streak and reset win streak
        else if ((x * 1.0) / (castle_MAX_HP * 1.0) >= 0.1)
        {
            ResetWinStreak();
        }
    }

    public void EndOfRoundWinnings() {
        // Check streak first
        CheckStreak();

        // Calculate the interest then give the total to the player
        int bonus = Mathf.Abs(_winStreak - _loseStreak) * END_OF_ROUND_WINNINGS;
        int total = _player.GetPlayerGold() + bonus;
        int interest = Mathf.RoundToInt(total * INTEREST_RATE);

        GiveGoldToPlayer(interest + bonus);
    }
}
