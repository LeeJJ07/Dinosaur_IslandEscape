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
            /*gameStateContext.Transition(cutSceneState);
            curState = EGameState.CUTSCENE;*/
            gameStateContext.Transition(runningState);
            curState = EGameState.RUNNING;
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
                        UpdateState(EGameState.SECONDMISSION);
                    //else if (runningState.IsSecondMissionTriggered())
                    //    UpdateState(EGameState.SECONDMISSION);
                    //else if (runningState.IsThirdMissionTriggered())
                    //    UpdateState(EGameState.THIRDMISSION);
                    //else if (runningState.IsTailMissionTriggered())
                    //    UpdateState(EGameState.TAILMISSION);
                    break;
                case EGameState.TAILMISSION:
                    break;
                case EGameState.FIRSTMISSION:
                    if(firstMissionState.test)
                        UpdateState(EGameState.RUNNING);
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