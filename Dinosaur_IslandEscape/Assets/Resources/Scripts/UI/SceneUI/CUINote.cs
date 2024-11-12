using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace HakSeung
{
    public class CUINote : CUIScene
    {
        private enum ENoteImageObject
        {
            HITCHECKRING,
            SUCCESS,
            FAIL,

            END
        }

        [SerializeField] private bool isHit;
        [SerializeField] private float curTime;
        [SerializeField] private const float noteHitCheckTime = 3f;
        [SerializeField] private const float noteHitResultTime = 2f;
        [SerializeField] private const float hitCheckRingScale = 5f;
        [SerializeField] private const float distanceToPlayerPostion = 1f;

        private const float noteFailTime = 0f;

        public GameObject[] noteObjects = new GameObject[(int)ENoteImageObject.END];
        
        //�׽�Ʈ ���̴ϱ� �÷��̾� �־�ߵ�
        public GameObject TestPlayer;

        protected override void InitUI()
        {
            curTime = noteFailTime;
        }

        public override void Show()
        {
            if (curTime == noteFailTime)
                base.Show();
        }

        private void OnEnable()
        {
            isHit = false;
            curTime = noteHitCheckTime;

            noteObjects[(int)ENoteImageObject.HITCHECKRING].SetActive(true);
            noteObjects[(int)ENoteImageObject.SUCCESS].SetActive(false);
            noteObjects[(int)ENoteImageObject.FAIL].SetActive(false);

            noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale *= hitCheckRingScale;

            StartCoroutine(IECheckNoteHitInSuccessTime());
        }

        private void OnDisable()
        {
            curTime = noteFailTime;
            noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale = Vector3.one;
        }

        /// <summary>
        /// curTime�� 0�� �ɶ����� �۵��ϴ� �ڷ�ƾ 
        /// ���������� ���� HitCheckRing�� Vector3.one���� �پ��� �ϰ�
        /// �Ŀ� isHit�� ���ο� ���� Success�� Fail�� �̹��� ������Ʈ�� Ȱ��ȭ ��Ų��
        /// </summary>
        /// <returns></returns>
        private IEnumerator IECheckNoteHitInSuccessTime()
        {
            float hitNoteScale = transform.localScale.x;

            while (curTime > noteFailTime && !isHit)
            {
                curTime -= Time.deltaTime;
                noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale =
                    Vector3.Lerp(noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale, Vector3.one, Time.deltaTime);

                if (TestPlayer != null) 
                    SyncUIWithPlayerPosition(TestPlayer.transform.position);

                yield return null;
            }

            if (isHit)
                noteObjects[(int)ENoteImageObject.SUCCESS].SetActive(true);
            else
                noteObjects[(int)ENoteImageObject.FAIL].SetActive(true);

            noteObjects[(int)ENoteImageObject.HITCHECKRING].SetActive(false);

            while (curTime <= noteHitResultTime)
            {
                curTime += Time.deltaTime;

                if (TestPlayer != null)
                    SyncUIWithPlayerPosition(TestPlayer.transform.position);

                yield return null;
            }


            Hide();
        }

        private void SyncUIWithPlayerPosition(Vector3 playerPosition)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(playerPosition + Vector3.up * distanceToPlayerPostion);
        }
        

            
    }

}
