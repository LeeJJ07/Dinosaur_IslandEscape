using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

namespace MyeongJin
{
	public class CFly : MonoBehaviour
	{
		public IObjectPool<CFly> Pool { get; set; }

		private Vector3 startPosition;
		private GameObject blood;
		bool isFirst = true;

		private void Awake()
		{
			blood = this.transform.GetChild(2).gameObject;
		}
		private void OnEnable()
		{
			if (isFirst)
				isFirst = false;
			else
				OnTriggerEnter(null);
		}
		private void OnDisable()
		{
			ResetObstacle();
		}
		public void ReturnToPool()
		{
			Pool.Release(this);
		}
		public void ResetObstacle()
		{
			this.GetComponent<BoxCollider>().enabled = true;
            blood.SetActive(false);
        }
		private void OnTriggerEnter(Collider other)
		{
			this.GetComponent<BoxCollider>().enabled = false;

            Debug.Log("Catch Bug");

            blood.SetActive(true);

            StartCoroutine("Catch");
		}
		private IEnumerator Catch()
		{
			yield return new WaitForSeconds(1.0f);

			ReturnToPool();
		}
	}
}