using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRings : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _rings;

    private void OnEnable()
    {
        _player.RingsChanged += OnRingsChanged;
    }

    private void OnDisable()
    {
        _player.RingsChanged -= OnRingsChanged;
    }

    private void OnRingsChanged(int rings)
    {
        _rings.text = rings.ToString();
    }
}
