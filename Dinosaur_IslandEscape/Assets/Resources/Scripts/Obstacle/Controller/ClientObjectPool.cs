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

        // TODO < 문명진 > - OnGUI 메소드 내용을 다른 호출 메소드(Update or Observer)로 변경해야함. - 2024.11.09 17:05
        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Obstacle"))
                obstaclePool.SpawnObstacle();

            if (GUI.Button(new Rect(10, 10, 50, 50), "Spawn Pteranodon"))
                creatureHerdPool.SpawnPteranodon();
        }
    }
}