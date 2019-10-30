using UnityEngine;
using UnityEngine.Events;

public class TestingGameController : MonoBehaviour
{
    int mGameRound;
    public UnityEvent OnGameRoundChange; //On player's gold Changes event

    // Start is called before the first frame update
    void Start()
    {
        mGameRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void IncreaseGameRound()
    {
        //logic for changing game round...
        mGameRound++;
        OnGameRoundChange.Invoke();
    }

    public int GetGameRound()
    {
        return mGameRound;
    }
}
