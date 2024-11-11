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
        public void SpawnPteranodon(int lineNum)
        {
            int space = 10;

            var obstacle = Pool.Get();

            // TODO < 문명진 > - 생성 위치를 미션 지점으로 지정해줘야 함.. - 2024.11.11 14:20

            obstacle.transform.position = new Vector3(lineNum * space, 0, 20);
        }
    }
}