using JongJin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MyeongJin
{
    public class TimerManager : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private static TimerManager sInstance;
        private GameObject gameSceneController;
        EGameState curState;
        private float[] Timer;

        public static TimerManager Instance
        {
            get
            {
                if (sInstance == null)
                {
                    GameObject newManagersObject = new GameObject("@TimerManager");
                    sInstance = newManagersObject.AddComponent<TimerManager>();
                }
                return sInstance;
            }
        }
        private void Awake()
        {
            if (sInstance != null && sInstance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            sInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            gameSceneController = GameObject.Find("GameSceneController");

            Timer = new float[5];
            for (int i = 0; i < 5; i++)
                Timer[i] = 0.0f;
        }
        private void Update()
        {
            UpdateCurState();

            switch (curState)
            {
                case EGameState.RUNNING:
                    IncreaseTime(EGameState.RUNNING);
                    break;
                case EGameState.TAILMISSION:
                    DecreaseTime(EGameState.TAILMISSION);
                    break;
                case EGameState.FIRSTMISSION:
                    DecreaseTime(EGameState.FIRSTMISSION);
                    break;
                case EGameState.SECONDMISSION:
                    DecreaseTime(EGameState.SECONDMISSION);
                    break;
                case EGameState.THIRDMISSION:
                    DecreaseTime(EGameState.THIRDMISSION);
                    break;
            }
            text.text = Timer[(int)curState].ToString();
        }
        private void UpdateCurState()
        {
            EGameState gameState = gameSceneController.GetComponent<GameSceneController>().CurState;
            if(gameState != curState && gameState != EGameState.RUNNING)
            {
                InitTimer(gameState);
            }
            curState = gameState;
        }
        private void InitTimer(EGameState gameState)
        {
            switch (gameState)
            {
                case EGameState.TAILMISSION:
                    Timer[(int)gameState] = 10.0f;
                    break;
                case EGameState.FIRSTMISSION:
                    Timer[(int)gameState] = 20.0f;
                    break;
                case EGameState.SECONDMISSION:
                    Timer[(int)gameState] = 20.0f;
                    break;
                case EGameState.THIRDMISSION:
                    Timer[(int)gameState] = 20.0f;
                    break;
            }
        }
        private void IncreaseTime(EGameState gameState)
        {
            Timer[(int)gameState] += Time.deltaTime;
        }
        private void DecreaseTime(EGameState gameState)
        {
            Timer[(int)gameState] -= Time.deltaTime;
        }
    }
}