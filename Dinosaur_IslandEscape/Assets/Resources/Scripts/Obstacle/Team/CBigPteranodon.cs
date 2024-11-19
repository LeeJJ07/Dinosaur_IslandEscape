using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyeongJin
{
	public class CBigPteranodon : CCreatureHerd
	{
		private Vector3 startPosition;
		// >>: Stoop
		private Transform[] controlPoints;  // ������ (�ּ� 4�� �ʿ�)

		private float moveSpeed = 12f;       // �̵� �ӵ�
		private float t = 0f;               // Catmull-Rom ��� �ð� ����
		private int currentSegment = 0;     // ���� �̵� ���� � ����
		// <<

		private void Start()
        {
            controlPoints = GameObject.Find("SkyControlPoints").GetComponent<CSkyControlPoint>().controlPoints;
		}
		private void Update()
		{
			StoopAndClimb();
        }
        private void OnEnable()
        {
            this.transform.Rotate(45, 0, 0);
        }
        private void OnDisable()
		{
			ResetObstacle();
		}
		public new void ResetObstacle()
		{
			SetStartPosition();
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
		private void SetStartPosition()
		{
			if(controlPoints != null)
			{
				startPosition = this.transform.position;
				startPosition.y = controlPoints[0].position.y;
				startPosition.z = controlPoints[0].position.z;
				this.transform.position = startPosition;
			}
		}
		private void StoopAndClimb()
		{
			Debug.Log("DownFall Pteranodon!");

			if (controlPoints.Length < 4) return;

			Transform p0 = controlPoints[currentSegment];
			Transform p1 = controlPoints[currentSegment + 1];
			Transform p2 = controlPoints[currentSegment + 2];
			Transform p3 = controlPoints[currentSegment + 3];

			// Catmull-Rom � ���� ���
			Vector3 newPosition = CatmullRom(p0.position, p1.position, p2.position, p3.position, t);
			newPosition.x = transform.position.x;
			transform.position = newPosition;

			// ��� t ���� ���������� ������Ʈ (speed�� � �̵� �ӵ� ����)
			t += Time.deltaTime * moveSpeed / Vector3.Distance(p1.position, p2.position);

			// t�� 1�� �����ϸ� ���� �������� ��ȯ
			if (t >= 1f)
			{
				t = 0f; // � �ð� �ʱ�ȭ
				currentSegment++;

				// ������ ������ ������ ��ũ��Ʈ ����
				if(currentSegment == 1)
				{
					this.GetComponentInChildren<Animator>().SetBool("isTouch", true);
                    this.transform.Rotate(-45, 0, 0);
                }

				if (currentSegment >= controlPoints.Length - 3)
				{
					currentSegment = 0;
					Debug.Log("Catmull-Rom Spline ��� ������ �����߽��ϴ�!");
					ReturnToPool();
				}
			}
		}
		private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return 0.5f * (
				(2f * p1) +
				(-p0 + p2) * t +
				(2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
				(-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
		}
	}
}