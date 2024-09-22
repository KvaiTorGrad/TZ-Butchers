using ButchersGames;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bank", menuName = "Game/Bank")]
public class Bank : ScriptableObject
{
    [SerializeField] private int _maxCondition;
    [SerializeField] private int _currentCondition;
    private int _currentLvl;
    public event Action<int> ConditionLvl;
    public event Action<int, int> UpdateConditionUI;
    public void SetCondition(int newCondition)
    {
        var indexParticle = Mathf.Sign(newCondition) >= 0 ? 0 : 1;
        SFXManager.Instance.SpawnParticle(indexParticle);
        _currentCondition += newCondition;
        NextLvlCondition();
    }
    private void InitializingQuantity()
    {
        if (_currentCondition <= 0)
        {
            _currentCondition = 0;
            LevelManager.OnLevelEntedIsLoss.Invoke(true);
        }
        else if (_currentCondition >= _maxCondition)
            _currentCondition = _maxCondition;
    }
    private void NextLvlCondition()
    {
        InitializingQuantity();
        int lvl = 0;
        if (_currentCondition <= 10)
            lvl = 1;
        else if (_currentCondition > 10 && _currentCondition <= 50)
            lvl = 2;
        else if (_currentCondition > 50 && _currentCondition <= 70)
            lvl = 3;
        else if (_currentCondition > 70 && _currentCondition <= 90)
            lvl = 4;
        else if (_currentCondition > 90 && _currentCondition <= 100)
            lvl = 5;
        _currentLvl = lvl;
        ConditionLvl.Invoke(_currentLvl);
        UpdateConditionUI.Invoke(_currentCondition, _maxCondition);
    }
    public int CurrentLvl() => _currentLvl;
    public void Reset()
    {
        _maxCondition = 100;
        _currentCondition = 0;
    }
}
