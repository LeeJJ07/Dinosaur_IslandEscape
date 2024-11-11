using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JaeHoon
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] tilePrefabs;
        [SerializeField] private float tileRespawnPoint = 0.0f;
        [SerializeField] private float tileLength = 16.0f;
        [SerializeField] private int numberOfTiles = 3;
        [SerializeField] private Transform firstPlayerTransform;
        [SerializeField] private Transform secondPlayerTransform;
        private List<GameObject> activeTiles = new List<GameObject>();
        private float preventSinkhole = 25.0f;

        private void Start()
        {
            for (int index = 0; index < numberOfTiles; index++)
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (firstPlayerTransform.position.z - preventSinkhole > tileRespawnPoint - (numberOfTiles * tileLength)
                || secondPlayerTransform.position.z - preventSinkhole > tileRespawnPoint - (numberOfTiles * tileLength))
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
                DeleteTile();
            }
        }

        private void SpawnTile(int tileIndex)
        {
            GameObject playerRun = Instantiate(tilePrefabs[tileIndex], transform.forward * tileRespawnPoint, transform.rotation);
            activeTiles.Add(playerRun);
            tileRespawnPoint += tileLength;
        }
        private void DeleteTile()
        {
            Destroy(activeTiles[0],20f);
            activeTiles.RemoveAt(0);
        }
    }
}
