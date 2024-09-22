using ButchersGames;
using UnityEngine;

public class FlagZone : Item
{
    protected enum Zone
    {
        Flag,
        Door
    }

    protected Animator _animator;
    [SerializeField] protected Zone _zone;
    protected bool _isPassed;
    [SerializeField] protected AudioClip _clip;
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
                SFXManager.Instance.PlayAudioClip(_clip);
                _isPassed = true;
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Level.Instance.TurnOffItems();
    }
}
