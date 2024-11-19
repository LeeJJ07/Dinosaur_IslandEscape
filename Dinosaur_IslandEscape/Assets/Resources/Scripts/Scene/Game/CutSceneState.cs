using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JongJin
{
    public class CutSceneState : MonoBehaviour, IGameState
    {
        [SerializeField] private PlayableDirector timelineDirector;
        [SerializeField] private Fade fade;

        private bool isFinished = false;
        public void EnterState()
        {
            Invoke("SetFinish", (float)timelineDirector.duration - 1f);
        }
        public void UpdateState()
        {
        }

        public void ExitState()
        {
            fade.FadeInOut();
        }
        private void SetFinish() { isFinished = true; }
        public bool IsFinishedCutScene()
        {
            return isFinished;
        }
    }
}
