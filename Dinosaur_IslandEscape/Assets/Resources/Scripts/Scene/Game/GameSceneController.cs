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
 
        [SerializeField] private GameObject missionGround_1;
        [SerializeField] private GameObject missionGround_2;
        [SerializeField] private GameObject missionGround_3;


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

            missionGround_1.SetActive(false);           // missionGround_1�� �ʱ⿡�� ���� ����
            missionGround_2.SetActive(false);           // missionGround_2�� �ʱ⿡�� ���� ����
            missionGround_3.SetActive(false);           // missionGround_3�� �ʱ⿡�� ���� ����
        }

        private void Update()
        {
            switch (curState)
            {
                case EGameState.RUNNING:
                    if (runningState.IsFirstMissionTriggered())
                    {
                        UpdateState(EGameState.FIRSTMISSION);
                        missionGround_1.SetActive(true);                // FirstMission�� �����ϸ� missionGroun_1�� ����
                    }
                    else if (runningState.IsSecondMissionTriggered())
                    {
                        UpdateState(EGameState.SECONDMISSION);
                        missionGround_1.SetActive(true);
                    }
                    else if (runningState.IsThirdMissionTriggered())
                    {
                        UpdateState(EGameState.THIRDMISSION);
                        missionGround_1.SetActive(true);
                    }
                    //else if (runningState.IsTailMissionTriggered())
                    //{
                    //    UpdateState(EGameState.TAILMISSION);
                    //    missionGround_1.SetActive(true);
                    //}
                    break;
                case EGameState.TAILMISSION:
                    break;
                case EGameState.FIRSTMISSION:           // ù ��° �̼� ���¿���
                    if (firstMissionState.test)         // 
                    { 
                        UpdateState(EGameState.RUNNING);            // ��� �޸�
                        missionGround_1.SetActive(false);               
                    }
                    break;
                case EGameState.SECONDMISSION:
                    break;
                case EGameState.THIRDMISSION:
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