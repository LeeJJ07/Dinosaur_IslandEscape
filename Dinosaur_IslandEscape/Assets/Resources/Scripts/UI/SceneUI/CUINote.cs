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

        [SerializeField] private float curTime;
        [SerializeField] private float noteSuccessTime = 3f;
        [SerializeField] private bool isHit;
        
        private const float noteFailTime = 0f;
        private const float hitCheckRingScale = 2f;

        public GameObject[] noteObjects = new GameObject[(int)NoteImageObject.END];
        public Vector3 PlayerPos{ get; set; } = Vector3.zero;


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
        /// curTime이 0이 될때까지 작동하는 코루틴 
        /// 선형보간을 통해 HitCheckRing이 Vector3.one까지 줄어들게 하고
        /// 후에 isHit의 여부에 따라 Success와 Fail의 이미지 오브젝트를 활성화 시킨다
        /// </summary>
        /// <returns></returns>
        private IEnumerator IECheckNoteHitInSuccessTime()
        {
            float hitNoteScale = transform.localScale.x; 

            while(curTime > noteFailTime && !isHit)
            {
                curTime -= Time.deltaTime;
                noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale =
                    Vector3.Lerp(noteObjects[(int)NoteImageObject.HITCHECKRING].transform.localScale, Vector3.one, Time.deltaTime);
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


    }

}
