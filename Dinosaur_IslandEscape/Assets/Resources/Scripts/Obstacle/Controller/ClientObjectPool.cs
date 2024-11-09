using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyeongJin
{
    public class ClientObjectPool : MonoBehaviour
    {
        private CObstacleObjectPool obstaclePool;
        private CCreatureHerdPool creatureHerdPool;

        private void Start()
        {
            obstaclePool = gameObject.AddComponent<CObstacleObjectPool>();
            creatureHerdPool = gameObject.AddComponent<CCreatureHerdPool>();
        }

        // TODO < ������ > - OnGUI �޼ҵ� ������ �ٸ� ȣ�� �޼ҵ�(Update or Observer)�� �����ؾ���. - 2024.11.09 17:05
        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Obstacle"))
                obstaclePool.SpawnObstacle();

            if (GUI.Button(new Rect(10, 10, 50, 50), "Spawn Pteranodon"))
                creatureHerdPool.SpawnPteranodon();
        }
    }
}