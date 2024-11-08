using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class StoneObjectPool : MonoBehaviour
	{
		public int maxPoolSize = 10;
		public int stackDefaultCapacity = 10;

		public IObjectPool<CStone> Pool
		{
			get
			{
				if (pool == null)
					pool = new ObjectPool<CStone>(
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
		private IObjectPool<CStone> pool;

		private CStone CreatedPooledItem()
		{
			var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

			CStone stone = go.AddComponent<CStone>();

			go.name = "Stone";
			stone.Pool = Pool;

			return stone;
		}
		private void OnReturnedToPool(CStone stone)
		{
			stone.gameObject.SetActive(false);
		}

		private void OnTakeFromPool(CStone stone)
		{
			stone.gameObject.SetActive(true);
		}
        private void OnDestroyPoolObject(CStone stone)
        {
            Destroy(stone.gameObject);
        }
		public void Spawn()
		{
			var amount = Random.Range(1, 10);

			for (int i = 0; i < amount; ++i)
			{
				var stone = Pool.Get();

                //TODO < 문명진 > - 1등 플레이어의 일정거리 앞에 생성하도록 position 변경해야함. - 2024.11.07 4:10

                stone.transform.position = Random.insideUnitSphere * 10;
			}
		}
    }
}