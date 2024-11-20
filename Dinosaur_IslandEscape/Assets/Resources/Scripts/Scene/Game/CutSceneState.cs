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
            StartCoroutine(SetFinish());
        }
        public void UpdateState()
        {
        }

        public void ExitState()
        {
            fade.FadeInOut();
        }

        IEnumerator SetFinish()
        {
            yield return new WaitForSeconds((float)timelineDirector.duration - 1f);
            isFinished = true;
        }
        public bool IsFinishedCutScene()
        {
            return isFinished;
        }
    }
}
