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

        private string smallPteranodonName = "Prefabs/Obstacle/Team/SmallPteranodon";      // �������� �����ϴ� ���� ��ġ
        private string bigPteranodonName = "Prefabs/Obstacle/Team/BigPteranodon";      // �������� �����ϴ� ���� ��ġ
        private GameObject smallPteranodon;
        private GameObject bigPteranodon;

        private void Awake()
        {
            smallPteranodon = Resources.Load<GameObject>(smallPteranodonName);
            bigPteranodon = Resources.Load<GameObject>(bigPteranodonName);

            if (smallPteranodon != null)
            {
                Debug.Log($"������ '{smallPteranodonName}'��(��) Load �Ͽ����ϴ�.");
            }
            else
            {
                Debug.LogError($"������ '{smallPteranodonName}'��(��) ã�� �� �����ϴ�.");
                // ����ó�� �ڵ� �߰�
            }

            if (bigPteranodon != null)
            {
                Debug.Log($"������ '{bigPteranodonName}'��(��) Load �Ͽ����ϴ�.");
            }
            else
            {
                Debug.LogError($"������ '{bigPteranodonName}'��(��) ã�� �� �����ϴ�.");
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
                    // TODO < ������ > - �Ǿ� ���� �߰�. - 2024.11.11 17:15
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
            // TODO < ������ > - space�� Line�� x���� �޾Ƽ� ����ؾ� ��. - 2024.11.11 17:30
            float space = -1.745f;

            var obstacle = Pool.Get();

            // TODO < ������ > - ���� ��ġ�� �̼� �������� ��������� ��. - 2024.11.11 14:20
            // "30"�� "20"�� Line�� ���缭 �����ؾ� ��.
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