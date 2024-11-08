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

        //TODO < 문명진 > - startPosition Value를 현재 자기의 위치로 변경해야함. - 2024.11.07 4:10
        [SerializeField]
        private Vector3 startPosition = new Vector3(0, 0, 10);
        // <<

        //TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
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
            //TODO < 문명진 > - 돌 rotation 초기화 넣어야함. - 2024.11.07 4:20


            //TODO < 문명진 > - 돌의 삭제 위치를 공룡 위치로 초기화 해줘야함. - 2024.11.07 4:20
            destructPosition.z = -10;
        }
		private void RotateStone()
		{
			Debug.Log("Rotate Stone!");
            //TODO < 문명진 > - 돌 Rotate 코드 넣어야함. - 2024.11.07 4:20
        }
    }
}