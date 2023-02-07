using UnityEngine;

public class PipeGenerator : ObjectPool
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Vector2 _rangeSpawn;

    private float _elapsedTime;
    public float ElapsedTime => _elapsedTime;

    private void Start()
    {
        Initialize(_prefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
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
    }
}
