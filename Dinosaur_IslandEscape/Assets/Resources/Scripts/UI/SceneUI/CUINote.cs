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
        
        //테스트 용이니까 플레이어 넣어야됨
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
        /// curTime이 0이 될때까지 작동하는 코루틴 
        /// 선형보간을 통해 HitCheckRing이 Vector3.one까지 줄어들게 하고
        /// 후에 isHit의 여부에 따라 Success와 Fail의 이미지 오브젝트를 활성화 시킨다
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
