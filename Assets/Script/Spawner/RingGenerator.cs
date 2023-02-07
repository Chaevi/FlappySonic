using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : ObjectPool
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Vector2 _rangeSpawn;
    [SerializeField] private PipeGenerator _pipeGenerator;

    private float _elapsedTime;

    private void Start()
    {
        _elapsedTime = _pipeGenerator.ElapsedTime - 1.5f;
        Initialize(_prefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            int value = Random.Range(0, 2);
            if (value == 1)
            {
                if (TryGetObject(out GameObject gameObject))
                {
                    _elapsedTime = 0;

                    float spawnPositionY = Random.Range(_rangeSpawn.x, _rangeSpawn.y);
                    Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
                    gameObject.SetActive(true);
                    gameObject.transform.position = spawnPoint;
                    DisableObjectAbroadScreen();
                }
            }
            else
                _elapsedTime = 0;
        }
    }
}
