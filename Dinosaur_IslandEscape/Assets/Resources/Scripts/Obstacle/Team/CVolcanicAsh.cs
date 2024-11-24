using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

namespace MyeongJin
{
    public class CVolcanicAsh : MonoBehaviour
    {
        public IObjectPool<CVolcanicAsh> Pool { get; set; }

        private SpriteRenderer sprite;
        private Vector3 startPosition;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            int alphaValue = Random.Range(1, 10);
            var color = sprite.color;
            if (color.a < 1)
            {
                color.a += alphaValue * 0.0001f;
                sprite.color = color;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //OnTriggerEnter(null);
            }
        }
        private void OnEnable()
        {
            
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
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    this.GetComponent<BoxCollider>().enabled = false;

        //    Debug.Log("Clear Fog");

        //    StartCoroutine("Clear");
        //}
        //private IEnumerator Clear()
        //{
        //    yield return new WaitForSeconds(1.0f);

        //    ReturnToPool();
        //}
    }
}