using ButchersGames;
using SingleTon;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private Bank _bank;
    public Bank Bank => _bank;

    private void Start()
    {
        LevelManager.OnLevelStarted += StartGame;
        Bank.Reset();
    }
    private void StartGame()
    {
        Bank.SetCondition(40);

    }

    private void OnDestroy()
    {
        LevelManager.OnLevelStarted -= StartGame;
    }
}
