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
        private enum NoteImageObject
        {
            HITCHECKRING,
            SUCCESS,
            FAIL,

            END
        }

        [SerializeField] private bool isHit;
        [SerializeField] private float curTime;
        [SerializeField] private float noteSuccessTime = 3f;
        [SerializeField]private const float hitCheckRingScale = 5f;
        [SerializeField]private const float distanceToPlayerPostion = 1f;

        private const float noteFailTime = 0f;

        public GameObject[] noteObjects = new GameObject[(int)NoteImageObject.END];
        
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
            curTime = noteSuccessTime;

            noteObjects[(int)NoteImageObject.HITCHECKRING].SetActive(true);
            noteObjects[(int)NoteImageObject.SUCCESS].SetActive(false);
            noteObjects[(int)NoteImageObject.FAIL].SetActive(false);

            noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale *= hitCheckRingScale;

            StartCoroutine(IECheckNoteHitInSuccessTime());
        }

        private void OnDisable()
        {
            curTime = noteFailTime;
            noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale = Vector3.one;
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
                noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale =
                    Vector3.Lerp(noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale, Vector3.one, Time.deltaTime);

                //�÷��̾� ��ġ �����ϴ� �ڵ� �ʿ� 
                if (TestPlayer != null) 
                    SyncUIWithPlayerPosition(TestPlayer.transform.position);


                yield return null;
            }

            if (isHit)
                noteObjects[(int)NoteImageObject.SUCCESS].SetActive(true);
            else
                noteObjects[(int)NoteImageObject.FAIL].SetActive(true);

            noteObjects[(int)NoteImageObject.HITCHECKRING].SetActive(false);

            yield return new WaitForSeconds(2f);

            Hide();
        }

        private void SyncUIWithPlayerPosition(Vector3 playerPosition)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(playerPosition + Vector3.up * distanceToPlayerPostion);
        }
        

            
    }

}
