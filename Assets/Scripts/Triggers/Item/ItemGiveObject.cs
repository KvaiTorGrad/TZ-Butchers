using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiveObject : Item
{
    private enum ItemVariable
    {
        Money,
        Alcohol
    }
    [SerializeField] private ItemVariable _variable;
    [SerializeField] private int _value;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private ParticleSystem _particleSystem;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IControlleble player))
        {
                switch (_variable)
                {
                    case ItemVariable.Money:
                    GameManager.Instance.Bank.SetCondition(_value);
                    SFXManager.Instance.PlayAudioClip.Invoke(_clip);
                    break;
                    case ItemVariable.Alcohol:
                    GameManager.Instance.Bank.SetCondition(-_value);
                    SFXManager.Instance.PlayAudioClip.Invoke(_clip);
                    break;
                }
            Include(false);
        }
    }
}
