using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] private int _capacity;
    private List<GameObject> _objects = new List<GameObject>();

    private Camera _camera;

    public void Initialize(GameObject _prefab)
    {
        _camera = Camera.main;
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(_prefab, _container.transform);
            spawned.SetActive(false);

            _objects.Add(spawned);
        }
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _objects.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0, 0.5f));
        foreach (GameObject obj in _objects)
        {
            if (obj.activeSelf == true)
            {
                if (obj.transform.position.x < disablePoint.x)
                    obj.SetActive(false);
            }
        }
    }

    public void ResetPool()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
    }
}
