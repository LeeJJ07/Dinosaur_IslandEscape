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

        //TODO < ������ > - destructPosition Value�� Dinosaur�� �� �κ����� �����ؾ���. - 2024.11.07 4:10
        private GameObject rubberBand;
        private int destructPosition;
        // <<

        private void Start()
		{
            rubberBand = GameObject.Find("SpawnStones");
            //TODO < ������ > - CGenerator Ŭ������ �ƴ� ������ ��ġ�� ������ �ִ� �༮�� Ŭ�������� �����;���. - 2024.11.07 4:10
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
            //TODO < ������ > - ���� ���� ��ġ�� ���� ��ġ�� �ʱ�ȭ �������. - 2024.11.07 4:20
			destructPosition = rubberBand.GetComponent<CGenerator>().FirstPlayerPosition - 100;
        }
		private void RotateObstacle()
		{
			Debug.Log("Rotate Obstacle!");

			this.transform.Rotate(curRotateSpeed, 0, 0);
        }
    }
}