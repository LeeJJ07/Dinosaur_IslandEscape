using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyeongJin
{
    public class ClientObjectPool : MonoBehaviour
    {
        private StoneObjectPool pool;

        private void Start()
        {
            pool = gameObject.AddComponent<StoneObjectPool>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Stones"))
                pool.Spawn();
        }
    }
}