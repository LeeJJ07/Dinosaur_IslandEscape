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

		private ParticleSystem flyParticleSystem;
		private ParticleSystem lightBugParticleSystem;
        private GameObject blood;

        private void Awake()
		{
            flyParticleSystem = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            lightBugParticleSystem = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
			blood = this.transform.GetChild(2).gameObject;
		}
        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.Space))
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

            var mainModule = flyParticleSystem.main;
            mainModule.gravityModifier = 0f;

            var mainModule2 = lightBugParticleSystem.main;
            mainModule2.gravityModifier = 0f;
        }
		private void OnTriggerEnter(Collider other)
		{
			this.GetComponent<BoxCollider>().enabled = false;

            Debug.Log("Catch Bug");

			blood.SetActive(true);

			var mainModule = flyParticleSystem.main;
            mainModule.gravityModifier = 0.6f;

            var mainModule2 = lightBugParticleSystem.main;
            mainModule2.gravityModifier = 0.6f;

            StartCoroutine("Catch");
		}
		private IEnumerator Catch()
		{
			yield return new WaitForSeconds(1.0f);

			ReturnToPool();
		}
	}
}