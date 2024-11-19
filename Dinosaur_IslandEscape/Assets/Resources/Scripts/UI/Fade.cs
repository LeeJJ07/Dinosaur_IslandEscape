using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JongJin
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private Image panel;
        private float time = 0.0f;
        private float fTime = 1.0f;
        public void FadeInOut()
        {
            StartCoroutine(FadeFlow());
        }

        IEnumerator FadeFlow()
        {
            panel.gameObject.SetActive(true);
            time = 0.0f;

            Color alpha = panel.color;
            while (alpha.a < 1.0f)
            {
                time += Time.deltaTime / fTime;
                alpha.a = Mathf.Lerp(0.0f, 1.0f, time);
                panel.color = alpha;
                yield return null;
            }

            time = 0.0f;
            yield return new WaitForSeconds(1.0f);

            while (alpha.a > 0.0f)
            {
                time += Time.deltaTime / fTime;
                alpha.a = Mathf.Lerp(1.0f, 0.0f, time);
                panel.color = alpha;
                yield return null;
            }
            panel.gameObject.SetActive(false);
            yield return null;
        }
    }
}