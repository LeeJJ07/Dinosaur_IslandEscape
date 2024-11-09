using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JongJin
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instance;
        public static Managers Instance { get { Init(); return s_instance; } }

        private GameManager game = new GameManager();
        public static GameManager Game { get { return Instance.game; } }

        private InputManager input = new InputManager();
        public static InputManager Input { get { return Instance.input; } }
        private void Awake()
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
            s_instance = gameObject.AddComponent<Managers>();
            DontDestroyOnLoad(gameObject);
        }
    }
}