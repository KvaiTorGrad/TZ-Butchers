using ButchersGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagZone : Item
{
    private enum Zone
    {
        Flag,
        Door,
        Finish
    }

    private Animator _animator;
    [SerializeField] private Zone _zone;
    [SerializeField]private bool _isPassed;
    public bool IsPassed => _isPassed;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IControlleble player))
        {
            if (_zone == Zone.Flag)
            {
                _animator.SetTrigger("UpFlage");
                _isPassed = true;
            }
            else if(_zone == Zone.Door)
            {
                _animator.SetTrigger("OpenDoor");
                _isPassed = true;
            }
            else
            {
                LevelManager.OnLevelEnted?.Invoke();
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (_zone == Zone.Flag || _zone == Zone.Door)
        {
            base.OnTriggerExit(other);
            Level.Instance.TurnOffItems();
        }
    }
}
