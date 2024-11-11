using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
    public class CCreatureHerd : MonoBehaviour
    {
        public IObjectPool<CCreatureHerd> Pool { get; set; }

        private float timeToCheckPosition = 1f;
        private float moveSpeed = 10f;

        //TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
        [SerializeField]
        private int destructPosition = -10;
        // <<

        private void Update()
        {
            FlyPteranodon();
        }
        private void OnEnable()
        {
            StartCoroutine(CheckPosition());
        }
        IEnumerator CheckPosition()
        {
            yield return new WaitForSeconds(timeToCheckPosition);

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

        }
        private void FlyPteranodon()
        {
            Debug.Log("Fly Pteranodon!");

            this.transform.Translate(Vector3.forward * Time.deltaTime * -moveSpeed);
        }
    }
}