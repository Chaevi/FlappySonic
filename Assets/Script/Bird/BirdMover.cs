using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _tapForse = 10f;
    [SerializeField] private Game game;
    [SerializeField] private AudioClip _audioClipJump;

    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody;
    private bool gameIsPlaying;

    private void OnEnable()
    {
        game.StatusGameChanged += ChangeStateGame;
    }

    private void OnDisable()
    {
        game.StatusGameChanged -= ChangeStateGame;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        ResetPlayer();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameIsPlaying)
            {
                _rigidbody.velocity = new Vector2(_speed, 0);
                _rigidbody.AddForce(Vector2.up * _tapForse, ForceMode2D.Force);
                _audioSource.PlayOneShot(_audioClipJump);
            }
        }
    }

    public void ResetPlayer()
    {
        transform.position = _startPosition;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    private void ChangeStateGame(bool state)
    {
        gameIsPlaying = state;
    }
}
