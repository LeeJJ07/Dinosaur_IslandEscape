using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
    public class CCreatureHerdPool : MonoBehaviour
    {
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;
        static int distance = 0;

        private string smallPteranodonName = "Prefabs/Obstacle/Team/Pteranodon";      // 프리팹이 존재하는 폴더 위치
        private GameObject smallPteranodon;

        private void Awake()
        {
            smallPteranodon = Resources.Load<GameObject>(smallPteranodonName);

            if (smallPteranodon != null)
            {
                Debug.Log($"프리팹 '{smallPteranodonName}'을(를) Load 하였습니다.");
            }
            else
            {
                Debug.LogError($"프리팹 '{smallPteranodonName}'을(를) 찾을 수 없습니다.");
                // 예외처리 코드 추가
            }
        }

        public IObjectPool<CCreatureHerd> Pool
        {
            get
            {
                if (pool == null)
                    pool = new ObjectPool<CCreatureHerd>(
                                CreatedPooledItem,
                                OnTakeFromPool,
                                OnReturnedToPool,
                                OnDestroyPoolObject,
                                true,
                                stackDefaultCapacity,
                                maxPoolSize);

                return pool;
            }
        }
        private IObjectPool<CCreatureHerd> pool;

        private CCreatureHerd CreatedPooledItem()
        {
            CCreatureHerd obstacle;

            switch (UnityEngine.Random.Range(0, 1))
            {
                case 0:
                    var go = Instantiate(smallPteranodon);
                    go.name = "smallPteranodon";

                    obstacle = go.AddComponent<CCreatureHerd>();
                    break;
                default:
                    obstacle = null;
                    break;
            }

            obstacle.Pool = Pool;

            return obstacle;
        }
        private void OnReturnedToPool(CCreatureHerd obstacle)
        {
            obstacle.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(CCreatureHerd obstacle)
        {
            obstacle.gameObject.SetActive(true);
        }
        private void OnDestroyPoolObject(CCreatureHerd obstacle)
        {
            Destroy(obstacle.gameObject);
        }
        public void SpawnPteranodon()
        {
            // TODO < 문명진 > - 프테라노돈 생성. - 2024.11.09 17:10

            int playerCount = 2;
            int space = 10;

            for (int i = 0; i < playerCount; ++i)
            {
                var obstacle = Pool.Get();

                obstacle.transform.position = new Vector3(i * space, 0, distance);
            }

            distance += 10;
        }
    }
}