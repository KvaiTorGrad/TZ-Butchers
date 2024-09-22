using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerParametrs", menuName = "Game/PlayerParametrs")]
public class PlayerParametrs : ScriptableObject
{
    [SerializeField] private float _speedForward;
    [SerializeField] private float _speedSide;
    [SerializeField] private float _speedRotate;
    [Range(0, 3), SerializeField] private float _rangePosition;

    public float SpeedForward => _speedForward;
    public float SpeedSide => _speedSide;
    public float SpeedRotate => _speedRotate;
    public float RangePosition => _rangePosition;
}
