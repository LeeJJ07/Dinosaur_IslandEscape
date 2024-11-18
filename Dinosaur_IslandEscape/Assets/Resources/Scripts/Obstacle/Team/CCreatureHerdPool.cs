using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CCreatureHerdPool : MonoBehaviour
	{
		public int maxPoolSize = 10;
		public int stackDefaultCapacity = 10;

		private int creatureNum = 0;
		private int bigCreatureStack = 0;

		private string smallPteranodonName = "Prefabs/Obstacle/Team/SmallPteranodon";      // �������� �����ϴ� ���� ��ġ
		private string bigPteranodonName = "Prefabs/Obstacle/Team/BigPteranodon";      // �������� �����ϴ� ���� ��ġ
		private GameObject smallPteranodon;
		private GameObject bigPteranodon;

		// >>: �ͷ� �̵� ��
		private Transform[] controlPoints;
		// <<

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
        private void Start()
        {
            // TODO < ������ > - controlPoints�� �������ִ� Ŭ������ �����ؼ� ������ �� �ֵ��� �����غ� �� - 2024.11.18 14:40
            controlPoints = GameObject.FindGameObjectsWithTag("ControlPoint")
                              .Select(obj => obj.transform)
                              .ToArray();

            SortWithName();

			for (int i = 0; i < 10; i++)
			{
				CreatedPooledItem().ReturnToPool();
			}
        }
        private void SortWithName()
        {
            for (int i = 0; i < controlPoints.Length; i++)
            {
                for (int j = i + 1; j < controlPoints.Length; j++)
                {
                    if (string.Compare(controlPoints[i].gameObject.name, controlPoints[j].gameObject.name) == 1)
                    {
                        Transform temp = controlPoints[i];
                        controlPoints[i] = controlPoints[j];
                        controlPoints[j] = temp;
                    }
                }
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


			switch (creatureNum / 5)
			{
				case 0:
					var go = Instantiate(smallPteranodon);
					go.name = "SmallPteranodon";

					obstacle = go.AddComponent<CSmallPteranodon>();
					break;
				case 1:
					go = Instantiate(bigPteranodon);
					go.name = "BigPteranodon";

					obstacle = go.AddComponent<CBigPteranodon>();
					break;
				case 2:
					// TODO < ������ > - �Ǿ� ���� �߰�. - 2024.11.11 17:15
					obstacle = null;
					break;
			}
			creatureNum++;

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
		public bool SpawnPteranodon(int lineNum, Vector3 position)
		{
			// TODO < ������ > - space�� Line�� x���� �޾Ƽ� ����ؾ� ��. - 2024.11.11 17:30
			float space = 4f;

            CCreatureHerd obstacle = null;
            List<CCreatureHerd> temp = new List<CCreatureHerd>();

            if (bigCreatureStack % 5 != 0)
			{
				while(true)
				{
                    obstacle = Pool.Get();
					if (obstacle is CSmallPteranodon)
					{
						foreach(CCreatureHerd i in temp)
						{
							i.ReturnToPool();
                        }
						break;
					}
					else
					{
                        temp.Add(obstacle);
					}
                }
			}
			else
			{
                while (true)
                {
                    obstacle = Pool.Get();
                    if (obstacle is CBigPteranodon)
                    {
                        foreach (CCreatureHerd i in temp)
                        {
                            i.ReturnToPool();
                        }
                        break;
                    }
                    else
                    {
                        temp.Add(obstacle);
                    }
                }
            }
			bigCreatureStack++;

            // TODO < ������ > - ���� ��ġ�� �̼� �������� ��������� ��. - 2024.11.11 14:20
            // "30"�� "20"�� Line�� ���缭 �����ؾ� ��.
            if (obstacle  is CSmallPteranodon)
			{
				obstacle.transform.position = new Vector3(lineNum * space + position.x - 2, controlPoints[1].position.y, controlPoints[1].position.z);
			}
			else if (obstacle is CBigPteranodon)
			{
				obstacle.transform.position = new Vector3(space / 2 + position.x - 2, controlPoints[1].position.y, controlPoints[1].position.z);
			}
			return true;
		}
	}
}