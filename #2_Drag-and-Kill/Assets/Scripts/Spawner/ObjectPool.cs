﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private readonly List<GameObject> _pool = new List<GameObject>();


    protected void InitializePool(GameObject unit)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(unit, _container.transform);

            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}

