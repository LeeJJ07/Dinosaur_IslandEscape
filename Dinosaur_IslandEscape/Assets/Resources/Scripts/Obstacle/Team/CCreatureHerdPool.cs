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

        private string smallPteranodonName = "Prefabs/Obstacle/Team/Pteranodon";      // �������� �����ϴ� ���� ��ġ
        private GameObject smallPteranodon;

        private void Awake()
        {
            smallPteranodon = Resources.Load<GameObject>(smallPteranodonName);

            if (smallPteranodon != null)
            {
                Debug.Log($"������ '{smallPteranodonName}'��(��) Load �Ͽ����ϴ�.");
            }
            else
            {
                Debug.LogError($"������ '{smallPteranodonName}'��(��) ã�� �� �����ϴ�.");
                // ����ó�� �ڵ� �߰�
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

            // TODO < ������ > - ���� ��ġ�� �̼� �������� ��������� ��.. - 2024.11.11 14:20

            obstacle.transform.position = new Vector3(lineNum * space, 0, 20);
        }
    }
}