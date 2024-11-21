using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JongJin
{
    public class GameSceneController : MonoBehaviour
    {
        [Header("GameScene States")]
        [SerializeField] private CutSceneState cutSceneState;
        [SerializeField] private RunningState runningState;
        [SerializeField] private TailMissionState tailMissionState;
        [SerializeField] private FirstMissionState firstMissionState;
        [SerializeField] private SecondMissionState secondMissionState;
        [SerializeField] private ThirdMissionState thirdMissionState;

        [SerializeField] private GameObject missionGround;
        [SerializeField] private GameObject startForestGround;

        [SerializeField] Material skyboxNormal;
        [SerializeField] Material skyboxVolcano;

        [SerializeField] private GameObject missionRoomVolcano;
        

        private GameStateContext gameStateContext;
        private EGameState curState;
        public EGameState CurState { get { return curState; } }

        private void Start()
        {
            cutSceneState = GetComponent<CutSceneState>();
            runningState = GetComponent<RunningState>();
            tailMissionState = GetComponent<TailMissionState>();
            firstMissionState = GetComponent<FirstMissionState>();
            secondMissionState = GetComponent<SecondMissionState>();
            thirdMissionState = GetComponent<ThirdMissionState>();

            gameStateContext = new GameStateContext(this);
<<<<<<< HEAD
            gameStateContext.Transition(cutSceneState);
            curState = EGameState.CUTSCENE;
=======
            gameStateContext.Transition(runningState);
            curState = EGameState.RUNNING;

            missionGround.SetActive(false);           // missionGround�� �ʱ⿡�� ���� ����
            startForestGround.SetActive(true);          // startForestGround�� �ʱ⿡ �ѵ� ����

            RenderSettings.skybox = skyboxNormal;       // �ʱ� skybox�� skyboxNormal

            missionRoomVolcano.SetActive(false);
>>>>>>> feature/map_v3.0.5
        }

        private void Update()
        {
            switch (curState)
            {
                case EGameState.CUTSCENE:
                    if (cutSceneState.IsFinishedCutScene())
                        UpdateState(EGameState.RUNNING);
                    break;
                case EGameState.RUNNING:
                    if (runningState.IsFirstMissionTriggered())
                    {
                        UpdateState(EGameState.FIRSTMISSION);
                        missionGround.SetActive(true);                // FirstMission�� �����ϸ� missionGround�� ����
                        startForestGround.SetActive(false);             // FirstMission�� �����ϸ� startForestGround�� ����
                        missionRoomVolcano.SetActive(true);
                    }
                    else if (runningState.IsSecondMissionTriggered())
                    {
                        UpdateState(EGameState.SECONDMISSION);
                        missionGround.SetActive(true);
                        missionRoomVolcano.SetActive(true);
                        RenderSettings.skybox = skyboxVolcano;          // SecondMission ���� �� skybox�� �Ӱ� ����
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
                case EGameState.FIRSTMISSION:           // ù ��° ���� �̼� ���¿���
                    if (firstMissionState.test)         // ù ��° ���� �̼� ������
                    { 
                        UpdateState(EGameState.RUNNING);            // ���� �������� �ٽ� �޸�
                        missionGround.SetActive(false);             // missinoGround �ٽ� ����
                        missionRoomVolcano.SetActive(false);
                    }
                    break;
                case EGameState.SECONDMISSION:          // missionGround.SetActive(false) �־����
                    break;
                case EGameState.THIRDMISSION:           // missionGround.SetActive(false) �־����
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