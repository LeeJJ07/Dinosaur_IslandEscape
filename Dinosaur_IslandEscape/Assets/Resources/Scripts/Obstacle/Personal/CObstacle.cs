using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CObstacle : MonoBehaviour
	{
		public IObjectPool<CObstacle> Pool { get; set; }

		private float timeToCheckPosition = 0.1f;
		private int maxRotateValue = 1;
		private int minRotateSpeed = 4;
		private int curRotateSpeed;

        //TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
        private GameObject rubberBand;
        private int destructPosition;
        // <<

        private void Start()
		{
            rubberBand = GameObject.Find("SpawnStones");
            //TODO < 문명진 > - CGenerator 클래스가 아닌 공룡의 위치를 가지고 있는 녀석의 클래스에서 가져와야함. - 2024.11.07 4:10
            destructPosition = rubberBand.GetComponent<CGenerator>().FirstPlayerPosition;
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

			destructPosition = rubberBand.GetComponent<CGenerator>().FirstPlayerPosition - 100;

            StartCoroutine(CheckPosition());

            if (this.transform.position.z < destructPosition)
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
			destructPosition = rubberBand.GetComponent<CGenerator>().FirstPlayerPosition - 100;
        }
		private void RotateObstacle()
		{
			Debug.Log("Rotate Obstacle!");

			this.transform.Rotate(curRotateSpeed, 0, 0);
        }
    }
}