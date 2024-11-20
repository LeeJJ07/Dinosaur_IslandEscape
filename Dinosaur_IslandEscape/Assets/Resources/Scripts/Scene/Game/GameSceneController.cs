using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JongJin
{
    public class GameSceneController : MonoBehaviour
    {
        [Header("GameScene States")]
        [SerializeField] private RunningState runningState;
        [SerializeField] private TailMissionState tailMissionState;
        [SerializeField] private FirstMissionState firstMissionState;
        [SerializeField] private SecondMissionState secondMissionState;
        [SerializeField] private ThirdMissionState thirdMissionState;

        [SerializeField] private GameObject missionGround;
        [SerializeField] private GameObject startForestGround;

        [SerializeField] Material skyboxNormal;
        [SerializeField] Material skyboxVolcano;
        

        private GameStateContext gameStateContext;
        private EGameState curState;
        public EGameState CurState { get { return curState; } }

        private void Start()
        {
            runningState = GetComponent<RunningState>();
            tailMissionState = GetComponent<TailMissionState>();
            firstMissionState = GetComponent<FirstMissionState>();
            secondMissionState = GetComponent<SecondMissionState>();
            thirdMissionState = GetComponent<ThirdMissionState>();

            gameStateContext = new GameStateContext(this);
            gameStateContext.Transition(runningState);
            curState = EGameState.RUNNING;

            missionGround.SetActive(false);           // missionGround는 초기에는 꺼둔 상태
            startForestGround.SetActive(true);          // startForestGround는 초기에 켜둔 상태

            RenderSettings.skybox = skyboxNormal;       // 초기 skybox는 skyboxNormal
        }

        private void Update()
        {
            switch (curState)
            {
                case EGameState.RUNNING:
                    if (runningState.IsFirstMissionTriggered())
                    {
                        UpdateState(EGameState.FIRSTMISSION);
                        missionGround.SetActive(true);                // FirstMission에 돌입하면 missionGround가 켜짐
                        startForestGround.SetActive(false);             // FirstMission에 돌입하면 startForestGround가 꺼짐
                        RenderSettings.skybox = skyboxVolcano;
                    }
                    else if (runningState.IsSecondMissionTriggered())
                    {
                        UpdateState(EGameState.SECONDMISSION);
                        missionGround.SetActive(true);
                    }
                    else if (runningState.IsThirdMissionTriggered())
                    {
                        UpdateState(EGameState.THIRDMISSION);
                        missionGround.SetActive(true);
                    }
                    //else if (runningState.IsTailMissionTriggered())
                    //{
                    //    UpdateState(EGameState.TAILMISSION);
                    //    missionGround_1.SetActive(true);
                    //}
                    break;
                case EGameState.TAILMISSION:
                    break;
                case EGameState.FIRSTMISSION:           // 첫 번째 돌발 미션 상태에서
                    if (firstMissionState.test)         // 첫 번째 돌발 미션 끝나면
                    { 
                        UpdateState(EGameState.RUNNING);            // 끊긴 지점에서 다시 달림
                        missionGround.SetActive(false);             // missinoGround 다시 꺼짐

                    }
                    break;
                case EGameState.SECONDMISSION:          // missionGround.SetActive(false) 넣어야함
                    break;
                case EGameState.THIRDMISSION:           // missionGround.SetActive(false) 넣어야함
                    break;
            }
            gameStateContext.CurrentState.UpdateState();
        }

        private void UpdateState(EGameState nextState)
        {
            if (curState == nextState)
                return;
            curState = nextState;

            switch (curState)
            {
                case EGameState.RUNNING:
                    gameStateContext.Transition(runningState);
                    break;
                case EGameState.TAILMISSION:
                    gameStateContext.Transition(tailMissionState);
                    break;
                case EGameState.FIRSTMISSION:
                    gameStateContext.Transition(firstMissionState);
                    break;
                case EGameState.SECONDMISSION:
                    gameStateContext.Transition(secondMissionState);
                    break;
                case EGameState.THIRDMISSION:
                    gameStateContext.Transition(thirdMissionState);
                    break;
            }
        }
    }
}