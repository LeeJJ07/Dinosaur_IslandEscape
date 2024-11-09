using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class DinosaurController : MonoBehaviour
    {

        [SerializeField] private float speed = 2.0f;
        private void Start()
        {

        }

        private void Update()
        {
            Move();
            Managers.Game.DinosaurMoveDistance = transform.position.z;
        }

        private void Move()
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }
    }
}