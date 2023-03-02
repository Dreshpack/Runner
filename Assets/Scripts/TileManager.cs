using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefabs = new GameObject[_numberOfTiles];
    [SerializeField] private Transform _playerTransform;

    private List<GameObject> _activeTiles = new List<GameObject>();

    private float _zCoodSpawn = 0;
    private float _tileLength = 10;
    private const int _numberOfTiles = 6;

    public void SpawnTile(int tileIndex)
    {
        GameObject currentTile = Instantiate(_tilePrefabs[tileIndex], transform.forward * _zCoodSpawn, transform.rotation);
        _activeTiles.Add(currentTile);
        _zCoodSpawn += _tileLength;
    }

    private void ArrangmentTiles()
    {
        if (_playerTransform.position.z - _tileLength * 2 > _zCoodSpawn - (_numberOfTiles * _tileLength))
        {
            SpawnTile(Random.Range(1, _numberOfTiles));
            DeleteTile();
        }
    }

    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }

    private void Start()
    {
        SpawnTile(0);
        for (int i = 0; i < _numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
                SpawnTile(Random.Range(1, _numberOfTiles));
        }
    }

    private void FixedUpdate()
    {
        ArrangmentTiles();
    }
}
