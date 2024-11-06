using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class Managers : MonoBehaviour
    {
        static Managers s_instance;
        static Managers Instance { get { Init(); return s_instance; } }

        private InputManager input = new InputManager();
        public static InputManager Input { get { return Instance.input; } }
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            input.OnUpdate();
        }

        static void Init()
        {
            if (s_instance != null)
                return;

            GameObject gameObject = GameObject.Find("@Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.AddComponent<Managers>();
        }
    }
}