using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JongJin
{
    public class Managers : MonoBehaviour
    {
        private static Managers sInstance;
        public static Managers Instance 
        {
            get
            {
                if (sInstance == null)
                {
                    GameObject newManagersObject = new GameObject("Managers");
                    sInstance = newManagersObject.AddComponent<Managers>();
                }
                return sInstance; 
            } 
        }

        private GameManager game = new GameManager();
        public static GameManager Game { get { return Instance.game; } }

        private InputManager input = new InputManager();
        public static InputManager Input { get { return Instance.input; } }
        private void Awake()
        {
            if(sInstance!=null && sInstance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            sInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            input.OnUpdate();
        }
    }
}