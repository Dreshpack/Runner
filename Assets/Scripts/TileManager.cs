using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefabs = new GameObject[_numberOfTiles];
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private RevivalAds _revivalAds;
    [SerializeField] private PlayerCollision _playerCollision;

    private List<GameObject> _activeTiles = new List<GameObject>();

    private float _zCoodSpawn = 0;
    private float _tileLength = 30;
    private const int _numberOfTiles = 5;

    private void OnEnable()
    {
        _revivalAds.isPlaying += SetVoidTile;
    }

    public void SpawnTile(int tileIndex)
    {
        //GameObject currentTile = Instantiate(_tilePrefabs[tileIndex], transform.forward * _zCoodSpawn, transform.rotation);
        GameObject currentTile = PoolManager.Instance.Spawn(_tilePrefabs[tileIndex], transform.forward * _zCoodSpawn, transform.rotation);
        _activeTiles.Add(currentTile);
        _zCoodSpawn += _tileLength;
    }

    public void SetVoidTile()
    {
        DeleteSpecificTile();
        Debug.Log("tile set");
}

    private void ArrangmentTiles()
    {
        if(_activeTiles.Last().transform.position.z - _playerTransform.position.z < 100)
        {
            SpawnTile(Random.Range(1, _numberOfTiles));
        }
        if (_activeTiles.Count > 7)
            DeleteTile();
    }

    private void DeleteTile()
    {
        PoolManager.Instance.Despawn(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }

    private void DeleteSpecificTile()
    {
        Debug.Log(_playerCollision.CurrentBorder);
        Destroy(_playerCollision.CurrentBorder);
    }

    private void Start()
    {
        _zCoodSpawn = 0;
        _activeTiles.Clear();
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
