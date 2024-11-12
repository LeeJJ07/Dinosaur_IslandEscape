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
        /// Instance�� �޾ƿ� ���Ƿ� ��𼱰����� ȣ�����־���Ѵ�.
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
        ///  ���� �̸��� ��ȯ�ϴ� �ڵ� �����ʿ�
        ///  Scene���� �̸��� �̸� �������� ����� �𸣰ھ �ϴ� �� �ڵ带 ���� type�� ������ LoadScene�� �̿��� �� �ֵ��� �س�����
        ///  Scene�� �̸� ����� ���� ���� Ű�� ����ΰǰ�??
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
