using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private float offsetX = 0.0f;
        [SerializeField] private float offsetY = 10.0f;
        [SerializeField] private float offsetZ = -10.0f;

        [SerializeField] private float cameraSpeed = 10.0f;

        private Vector3 targetPos;

        private void FixedUpdate()
        {
            targetPos = new Vector3(target.transform.position.x + offsetX,
                target.transform.position.y + offsetY,
                target.transform.position.z + offsetZ);

            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        }
    }
}
