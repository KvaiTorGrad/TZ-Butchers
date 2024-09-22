using SingleTon;
using System;
using UnityEngine;

public class SFXManager : SingletonBase<SFXManager>
{
     private AudioSource _audioSource;
    [SerializeField] private ParticleSystem[] _particleSystems;

    public Action<AudioClip> PlayAudioClip;
    public Action<int> SpawnParticle;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        PlayAudioClip += PlayAudio;
        SpawnParticle += CreateParticle;
    }
    private void PlayAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
    private void CreateParticle(int indexParticle)
    {
        Instantiate(_particleSystems[indexParticle], Controller.Instance.GetControlleble().Animator.transform.position, Quaternion.identity);
    }
}
