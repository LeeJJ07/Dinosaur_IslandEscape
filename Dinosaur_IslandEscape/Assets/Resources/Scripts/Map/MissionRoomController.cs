using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JaeHoon
{
    public class MissionRoomController : MonoBehaviour
    {
        public enum NextPositionType        // 두 가지 옵션
        {
            InitPosition,
            SomePosition,
        };

        public NextPositionType nextPositionType;

        public Transform DestinationPoint;      // player가 이동할 transform을 받아온다.

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))        // 특정 구간에 Player가 닿았을 때
            {
                if (nextPositionType == NextPositionType.InitPosition)
                {
                    collision.transform.position = Vector3.zero;
                }
                else if (nextPositionType == NextPositionType.SomePosition)
                {
                    collision.transform.position = DestinationPoint.position;
                }
                else
                {
                }
            }
        }
    }
}
