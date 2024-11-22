using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace HakSeung
{
    public class CUINote : MonoBehaviour
    {
        private enum ENoteImageObject
        {
            HITCHECKRING,
            SUCCESS,
            FAIL,

            END
        }

        //TODO<학승> const 대문자 변환해야됨 11/13
        [SerializeField] private bool isHit;
        [SerializeField] private float curTime;
        [SerializeField] private const float noteHitCheckTime = 3f;
        [SerializeField] private const float noteHitResultTime = 2f;
        [SerializeField] private const float hitCheckRingScale = 5f;
        [SerializeField] private const float distanceToPlayerPostion = 1f;
        [SerializeField] private Transform playerTransform;

        private const float noteFailTime = 0f;

        public GameObject[] noteObjects = new GameObject[(int)ENoteImageObject.END];

        Coroutine coCheckNoteHit; // 코루틴 명명법 모르겠어서 임시



        private void Init()
        {
            curTime = noteFailTime;
        }

        public void Show()
        {
            if (curTime == noteFailTime)
                this.gameObject.SetActive(true);
        }
        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            isHit = false;
            curTime = noteHitCheckTime;

            noteObjects[(int)ENoteImageObject.HITCHECKRING].SetActive(true);
            noteObjects[(int)ENoteImageObject.SUCCESS].SetActive(false);
            noteObjects[(int)ENoteImageObject.FAIL].SetActive(false);

            noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale *= hitCheckRingScale;

            coCheckNoteHit = StartCoroutine(IECheckNoteHitInSuccessTime());
        }

        private void OnDisable()
        {
            if (coCheckNoteHit != null)
                StopCoroutine(coCheckNoteHit);

            curTime = noteFailTime;
            noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale = Vector3.one;
        }

        private IEnumerator IECheckNoteHitInSuccessTime()
        {
            float hitNoteScale = transform.localScale.x;

            while (curTime > noteFailTime && !isHit)
            {
                curTime -= Time.deltaTime;
                noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale =
                    Vector3.Lerp(noteObjects[(int)ENoteImageObject.HITCHECKRING].transform.localScale, Vector3.one, Time.deltaTime);

                if (playerTransform != null)
                    SyncUIWithPlayerPosition(playerTransform.position);

                yield return null;
            }

            curTime = 0;

            if (isHit)
                gameObject.GetComponent<Image>().color = Color.green; //나중에 이미지로 받아오는거 변경 필요
            else
                gameObject.GetComponent<Image>().color = Color.red;

            noteObjects[(int)ENoteImageObject.HITCHECKRING].SetActive(false);


            while (curTime <= noteHitResultTime)
            {
                curTime += Time.deltaTime;

                if (playerTransform != null)
                    SyncUIWithPlayerPosition(playerTransform.position);

                yield return null;
            }

            gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.SetActive(false);

        }

        private void SyncUIWithPlayerPosition(Vector3 playerPosition)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(playerPosition + Vector3.up * distanceToPlayerPostion);
        }
        

            
    }

}
