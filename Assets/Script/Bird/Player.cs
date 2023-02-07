using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _audioClipDeath;
    [SerializeField] private AudioClip _audioClipPickUpRing;
    [SerializeField] private AudioClip _audioClipLostRings;
    [SerializeField] private AudioClip _audioClipBackground;
    [SerializeField] private ParticleSystem _particleSystem;

    private AudioSource _audioSource;
    private BirdMover _mover;
    private int _score;
    private int _rings;

    public event UnityAction GameOver;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> RingsChanged;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _mover = GetComponent<BirdMover>();
    }

    public void ResetPlayer()
    {
        _rings = 0;
        RingsChanged?.Invoke(_rings);
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.ResetPlayer();
        _animator.Play("Fly");
        _audioSource.Stop();
        _audioSource.clip = _audioClipBackground;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void Die()
    {
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.PlayOneShot(_audioClipDeath);
        _particleSystem.Stop();
        _animator.Play("Dead");
        GameOver?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ScoreArea scorArea))
            IncreaseScore();
        else if (collision.gameObject.TryGetComponent(out Ring ring))
        {
            ring.gameObject.SetActive(false);
            _audioSource.PlayOneShot(_audioClipPickUpRing);
            IncreaseRings();
        }
        else
        {
            if (_rings == 0)
                Die();
            else
            {
                _particleSystem.maxParticles = _rings;
                if(_rings > 20)
                    _particleSystem.maxParticles = 20;
                _rings = 0;
                RingsChanged?.Invoke(_rings);
                _audioSource.PlayOneShot(_audioClipLostRings);
                _particleSystem.Play();
            }
        }
    }

    private void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
    private void IncreaseRings()
    {
        _rings++;
        RingsChanged?.Invoke(_rings);
    }
}
