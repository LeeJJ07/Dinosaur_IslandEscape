using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CObstacle : MonoBehaviour
	{
		public IObjectPool<CObstacle> Pool { get; set; }

		private float timeToCheckPosition = 1f;
		private int maxRotateValue = 1;
		private int minRotateSpeed = 4;
		private int curRotateSpeed;

        //TODO < 문명진 > - startPosition Value를 현재 자기의 위치로 변경해야함. - 2024.11.07 4:10
        [SerializeField]
        private Vector3 startPosition = new Vector3(0, 0, 10);
        // <<

        //TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
        [SerializeField]
		private Vector3 destructPosition = new Vector3(0, 0, -3);
        // <<

        private Vector3 curPosition;

        private void Start()
		{
			curPosition = startPosition;
			curRotateSpeed = Random.Range(minRotateSpeed, minRotateSpeed + maxRotateValue);
        }

        private void Update()
        {
			RotateObstacle();
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
			ResetObstacle();
		}

		private void ReturnToPool()
		{
			Pool.Release(this);
		}
		private void ResetObstacle()
		{
            //TODO < 문명진 > - 돌의 삭제 위치를 공룡 위치로 초기화 해줘야함. - 2024.11.07 4:20
            destructPosition.z = -3;
        }
		private void RotateObstacle()
		{
			Debug.Log("Rotate Obstacle!");

			this.transform.Rotate(curRotateSpeed, 0, 0);
        }
    }
}