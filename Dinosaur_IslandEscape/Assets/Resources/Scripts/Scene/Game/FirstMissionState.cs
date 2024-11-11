using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class FirstMissionState : MonoBehaviour, IGameState
    {
        public bool test = false;
        public void EnterState()
        {
            StartCoroutine(Test());
        }
        public void UpdateState()
        {

        }

        public void ExitState()
        {

        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(30f);
            test = true;
        }
    }
}