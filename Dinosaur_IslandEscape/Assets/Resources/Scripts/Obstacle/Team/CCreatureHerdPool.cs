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

        private string smallPteranodonName = "Prefabs/Obstacle/Team/SmallPteranodon";      // 프리팹이 존재하는 폴더 위치
        private string bigPteranodonName = "Prefabs/Obstacle/Team/BigPteranodon";      // 프리팹이 존재하는 폴더 위치
        private GameObject smallPteranodon;
        private GameObject bigPteranodon;

        private void Awake()
        {
            smallPteranodon = Resources.Load<GameObject>(smallPteranodonName);
            bigPteranodon = Resources.Load<GameObject>(bigPteranodonName);

            if (smallPteranodon != null)
            {
                Debug.Log($"프리팹 '{smallPteranodonName}'을(를) Load 하였습니다.");
            }
            else
            {
                Debug.LogError($"프리팹 '{smallPteranodonName}'을(를) 찾을 수 없습니다.");
                // 예외처리 코드 추가
            }

            if (bigPteranodon != null)
            {
                Debug.Log($"프리팹 '{bigPteranodonName}'을(를) Load 하였습니다.");
            }
            else
            {
                Debug.LogError($"프리팹 '{bigPteranodonName}'을(를) 찾을 수 없습니다.");
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
            CCreatureHerd obstacle = null;

            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0:
                    var go = Instantiate(smallPteranodon);
                    go.name = "SmallPteranodon";

                    obstacle = go.AddComponent<CCreatureHerd>();
                    break;
                case 1:
                    go = Instantiate(bigPteranodon);
                    go.name = "BigPteranodon";

                    obstacle = go.AddComponent<CCreatureHerd>();
                    break;
                case 2:
                    // TODO < 문명진 > - 악어 생성 추가. - 2024.11.11 17:15
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
        public bool SpawnPteranodon(int lineNum)
        {
            // TODO < 문명진 > - space를 Line의 x값을 받아서 사용해야 함. - 2024.11.11 17:30
            float space = -1.745f;

            var obstacle = Pool.Get();

            // TODO < 문명진 > - 생성 위치를 미션 지점으로 지정해줘야 함. - 2024.11.11 14:20
            // "30"과 "20"을 Line에 맞춰서 생성해야 함.
            if (obstacle.name == "SmallPteranodon")
            {
                obstacle.transform.position = new Vector3(lineNum * space + 30, 0, 20);
                return true;
            }
            else if (obstacle.name == "BigPteranodon")
            {
                obstacle.transform.position = new Vector3(space / 2 + 30, 0, 20);
            }
            return false;
        }
    }
}