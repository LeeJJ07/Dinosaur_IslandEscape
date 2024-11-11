using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CStone : MonoBehaviour
	{
		public IObjectPool<CStone> Pool { get; set; }

		[SerializeField]
		private float timeToCheckPosition = 1f;

        //TODO < ������ > - startPosition Value�� ���� �ڱ��� ��ġ�� �����ؾ���. - 2024.11.07 4:10
        [SerializeField]
        private Vector3 startPosition = new Vector3(0, 0, 10);
        // <<

        //TODO < ������ > - destructPosition Value�� Dinosaur�� �� �κ����� �����ؾ���. - 2024.11.07 4:10
        [SerializeField]
		private Vector3 destructPosition = new Vector3(0, 0, -10);
        // <<

        private Vector3 curPosition;

        private void Start()
		{
			curPosition = startPosition;
        }
        private void OnEnable()
        {
            StartCoroutine(CheckPosition());
        }
		IEnumerator CheckPosition()
		{
			yield return new WaitForSeconds(timeToCheckPosition);

			// >> : Test Code
			destructPosition.z += 1;
			Debug.Log(destructPosition.z);
            // <<

            StartCoroutine(CheckPosition());

            if (curPosition.z < destructPosition.z)
				ReturnToPool();
		}

		private void OnDisable()
		{
			ResetStone();
		}

		private void ReturnToPool()
		{
			Pool.Release(this);
		}
		private void ResetStone()
		{
            //TODO < ������ > - �� rotation �ʱ�ȭ �־����. - 2024.11.07 4:20


            //TODO < ������ > - ���� ���� ��ġ�� ���� ��ġ�� �ʱ�ȭ �������. - 2024.11.07 4:20
            destructPosition.z = -10;
        }
		private void RotateStone()
		{
			Debug.Log("Rotate Stone!");
            //TODO < ������ > - �� Rotate �ڵ� �־����. - 2024.11.07 4:20
        }
    }
}