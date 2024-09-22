using ButchersGames;
using UnityEngine;

public class Door : FlagZone
{
    [SerializeField] private int _lvl;
    [SerializeField] private AudioClip _finishClip;
    protected override void OnTriggerEnter(Collider other)
    {
        if (_zone == Zone.Door)
        {
            if (GameManager.Instance.Bank.CurrentLvl() >= _lvl)
            {
                _animator.SetTrigger("OpenDoor");
                _isPassed = true;
                SFXManager.Instance.PlayAudioClip.Invoke(_clip);
            }
            else
            {
                LevelManager.OnLevelEntedIsLoss?.Invoke(false);
                SFXManager.Instance.PlayAudioClip.Invoke(_finishClip);
            }

        }
    }
}
