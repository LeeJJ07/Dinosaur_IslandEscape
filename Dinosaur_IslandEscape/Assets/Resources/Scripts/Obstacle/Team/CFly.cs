using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
    public class CFly : MonoBehaviour
    {
        public IObjectPool<CFly> Pool { get; set; }

        private Vector3 startPosition;

        private void Update()
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

        }
    }
}