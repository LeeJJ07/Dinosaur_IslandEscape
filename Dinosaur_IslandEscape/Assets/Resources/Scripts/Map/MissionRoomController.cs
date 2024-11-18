using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JaeHoon
{
    public class MissionRoomController : MonoBehaviour
    {
        public enum NextPositionType        // �� ���� �ɼ�
        {
            InitPosition,
            SomePosition,
        };

        public NextPositionType nextPositionType;

        public Transform DestinationPoint;      // player�� �̵��� transform�� �޾ƿ´�.

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))        // Ư�� ������ Player�� ����� ��
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
