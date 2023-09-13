using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] ChunkView[] _chunksPrefabs;
    [SerializeField] ChunkView _firstChunk;
    [SerializeField, Range(20f, 80f)] float _distanceToSpawn;

    private readonly List<ChunkView> _spawnedChunks = new();

    void Start() => _spawnedChunks.Add(_firstChunk);

    void Update()
    {
        if (Vector3.Distance(_player.position, _spawnedChunks[^1].End.position) < _distanceToSpawn)
            SpawnChunk();
    }

    private void SpawnChunk()
    {
        ChunkView newChunk = Instantiate(GetRandomChunk());
        //We subtract the local position of the beginning of the new chunk from the End position of the last filled chunk
        newChunk.transform.position = new Vector3(_spawnedChunks[^1].End.position.x - newChunk.Begin.localPosition.x, 0f, 0f);
        _spawnedChunks.Add(newChunk);

        if (_spawnedChunks.Count > 3)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private ChunkView GetRandomChunk()
    {
        List<float> chances = new();
        for (int i = 0; i < _chunksPrefabs.Length; i++)
            chances.Add(_chunksPrefabs[i].ChanceFromDistance.Evaluate(_player.transform.position.x));

        float value = Random.Range(0, chances.Sum());
        float sum = 0;
        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum) return _chunksPrefabs[i];
        }
        return _chunksPrefabs[^1];
    }
}
