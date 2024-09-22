using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bank", menuName = "Game/Bank")]
public class Bank : ScriptableObject
{
    [SerializeField] private int _maxCondition;
    [SerializeField] private int _currentCondition;
    public event Action<int> ConditionLvl;
    public void SetCondition(int newCondition)
    {
        _currentCondition += newCondition;
        NextLvlCondition();
    }
    private void InitializingQuantity()
    {
        if (_currentCondition <= 0)
            _currentCondition = 0;
        else if (_currentCondition >= _maxCondition)
            _currentCondition = _maxCondition;
    }
    private void NextLvlCondition()
    {
        int lvl = 0;
        if (_currentCondition <= 10)
            lvl = 1;
        else if (_currentCondition > 10 && _currentCondition <= 50)
            lvl = 2;
        else if (_currentCondition > 50 && _currentCondition <= 70)
            lvl = 3;
        else if (_currentCondition > 70 && _currentCondition <= 100)
            lvl = 4;
        InitializingQuantity();
        ConditionLvl.Invoke(lvl);
    }
    public void Reset()
    {
        _maxCondition = 100;
        _currentCondition = 0;
    }
}
