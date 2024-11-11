using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] public GameObject target;

        [SerializeField] private float offsetX = 0.0f;
        [SerializeField] private float offsetY = 10.0f;
        [SerializeField] private float offsetZ = -10.0f;

        [SerializeField] private float cameraSpeed = 10.0f;

        private Vector3 targetPos;

        private void OnEnable()
        {
            // TODO<이종진> - 카메라 회전값 수정 필요 - 20241112
            transform.rotation = Quaternion.Euler(20.0f, 180.0f, 0.0f);
        }
        private void FixedUpdate()
        {
            targetPos = new Vector3(target.transform.position.x + offsetX,
                target.transform.position.y + offsetY,
                target.transform.position.z + offsetZ);

            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        }
        private void OnDisable()
        {
            // TODO<이종진> - 카메라 위치와 회전값 수정 필요 - 20241112
            transform.position = new Vector3(30.0f, 3.0f, -10.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
}
