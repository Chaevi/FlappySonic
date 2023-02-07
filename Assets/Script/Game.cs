using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PipeGenerator _pipeGenerator;
    [SerializeField] private RingGenerator _ringGenerator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    public event UnityAction<bool> StatusGameChanged;


    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StarGame();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        _ringGenerator.ResetPool();
        _pipeGenerator.ResetPool();
        StarGame();
    }

    private void StarGame()
    {
        Time.timeScale = 1;
        _player.ResetPlayer();
        StatusGameChanged?.Invoke(true);
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
        StatusGameChanged?.Invoke(false);
    }

}
