using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HakSeung
{
    public enum ESceneType
    {
        INTRO,
        GAME,
        SCORE,

        END
    }

    public class SceneManagerExtended: MonoBehaviour
    {
        public BaseScene CurrentScene { get; }

        private static SceneManagerExtended s_Instance;

        private const string sceneManagerObjectName = "_SceneManager";


        /// <summary>
        /// Instance를 받아와 쓰므로 어디선가에서 호출해주어야한다.
        /// </summary>
        public static SceneManagerExtended Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    GameObject newSceneManagerObject = new GameObject(sceneManagerObjectName);
                    s_Instance = newSceneManagerObject.AddComponent<SceneManagerExtended>();
                }

                return s_Instance;
            }
        }

        /// <summary>
        ///  씬별 이름을 변환하는 코드 수정필요
        ///  Scene들의 이름을 미리 가져오는 방법을 모르겠어서 일단 이 코드를 통해 type을 넣으면 LoadScene에 이용할 수 있도록 해놓았음
        ///  Scene을 미리 만들어 놓고 껏다 키는 방식인건가??
        /// </summary>
        public string SceneTypeToString(ESceneType type)
        {
            string sceneName = string.Empty;
            switch(type)
            {
                case ESceneType.INTRO:
                    sceneName = "INTRO";
                    break;
                case ESceneType.GAME:
                    sceneName = "GAME";
                    break;
                case ESceneType.SCORE:
                    sceneName = "SCORE";
                    break;
                default:
                    sceneName = "NOTING";
                    Debug.Log($"this scene does not exist");
                    break;
            }

            return sceneName;
        }

        public void LoadScene(ESceneType type)
        {
            CurrentScene.Clear();
            SceneManager.LoadScene(SceneTypeToString(type));
        }


    }

}
