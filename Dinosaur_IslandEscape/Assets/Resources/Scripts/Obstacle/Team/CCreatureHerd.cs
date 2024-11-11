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
        private int maxRotateValue = 1;
        private int minRotateSpeed = 4;
        private int curRotateSpeed;

        //TODO < ������ > - startPosition Value�� ���� �ڱ��� ��ġ�� �����ؾ���. - 2024.11.07 4:10
        [SerializeField]
        private Vector3 startPosition = new Vector3(0, 10, 30);
        // <<

        //TODO < ������ > - destructPosition Value�� Dinosaur�� �� �κ����� �����ؾ���. - 2024.11.07 4:10
        [SerializeField]
        private int destructPosition = -10;
        // <<

        private Vector3 curPosition;

        private void Start()
        {
            curPosition = startPosition;
            curRotateSpeed = Random.Range(minRotateSpeed, minRotateSpeed + maxRotateValue);
        }

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

            if (curPosition.z < destructPosition)
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
            //TODO < ������ > - �� rotation �ʱ�ȭ �־����. - 2024.11.07 4:20

            //TODO < ������ > - ���� ���� ��ġ�� ���� ��ġ�� �ʱ�ȭ �������. - 2024.11.07 4:20
        }
        private void FlyPteranodon()
        {
            Debug.Log("Fly Pteranodon!");

            this.transform.Translate(Vector3.forward * Time.deltaTime * -1);
        }
    }
}