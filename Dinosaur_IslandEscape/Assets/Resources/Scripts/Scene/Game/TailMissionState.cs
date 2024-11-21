using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

namespace JongJin
{
    public class TailMissionState : MonoBehaviour, IGameState
    {
        // TODO<이종진> - 나중에 레벨 디자인 끝나면 SerializeField 지우기 필요 - 20241121
        [SerializeField] private readonly float dinoPosDiff = 5.0f;
        [SerializeField] private readonly int ATTACKCOUNT = 3;

        [SerializeField] private GameObject dinosaur;
        [SerializeField] private GameObject []warningEffect;

        [SerializeField] private float warningTime = 3.0f;
        [SerializeField] private float attackDelayTime = 3.0f;

        private float[] dinosaurPosY = { 0.6f, -3.8f };
        private float[] dinosaurRotX = { -16.0f, 12.0f  };

        private Animator dinosaurAnimator;
        private int attackCount;
        private float attackTime = 1.5f;

        private float flowTime = 0.0f;
        private float warningFlowTime = 0.0f;
        private float attackFlowTime = 0.0f;

        private int randomAttackPos = -1;
        private void Awake()
        {
            dinosaurAnimator = dinosaur.GetComponent<Animator>();
        }
        public void EnterState()
        {
            attackCount = ATTACKCOUNT;

            flowTime = 0.0f;
            warningFlowTime = 0.0f;
            attackFlowTime = 0.0f;

            randomAttackPos = -1;
        }
        public void UpdateState()
        {
            flowTime += Time.deltaTime;
            if (flowTime < attackDelayTime)
                return;

            if (randomAttackPos == -1)
            {
                randomAttackPos = Random.Range(0, 2);
                warningEffect[randomAttackPos].SetActive(true);
            }
            warningFlowTime += Time.deltaTime;
            if (warningFlowTime < warningTime)
                return;
            if (attackFlowTime <= 0.0f)
            {
                warningEffect[randomAttackPos].SetActive(false);
                dinosaur.SetActive(true);
                dinosaur.transform.eulerAngles = new Vector3(dinosaurRotX[randomAttackPos], dinosaur.transform.eulerAngles.y, dinosaur.transform.eulerAngles.z);
            }
            dinosaur.transform.position = new Vector3(Mathf.Lerp(147.0f, 157.0f + randomAttackPos * 2, attackFlowTime / attackTime), dinosaurPosY[randomAttackPos], dinosaur.transform.position.z);
            attackFlowTime += Time.deltaTime;
            if (attackFlowTime < attackTime)
                return;

            dinosaur.SetActive(false);
            attackCount--;

            flowTime = 0.0f;
            warningFlowTime = 0.0f;
            attackFlowTime = 0.0f;
            randomAttackPos = -1;
        }

        public void ExitState()
        {
            dinosaur.SetActive(false);
            attackCount--;

            flowTime = 0.0f;
            warningFlowTime = 0.0f;
            attackFlowTime = 0.0f;
            randomAttackPos = -1;
        }

        public bool IsFinishMission()
        {
            // TODO<이종진> - 실패시 꼬리미션 탈출 구현 필요 - 20241121
            // TODO<이종진> - 3번 다 성공했을 때 꼬리미션 탈출 - 20241121
            if (attackCount <= 0) 
                return true;
            return false;
        }
    }
}
