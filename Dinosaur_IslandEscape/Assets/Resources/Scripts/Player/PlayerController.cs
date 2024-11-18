using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace JongJin
{
    public class PlayerController : MonoBehaviour
    {
        private readonly string[] playerTag = { "Player1", "Player2", "Player3", "Player4" };
        private readonly string groundTag = "Ground";
        private readonly string paramMission = "isMission";
        private readonly string paramSpeed = "speed";
        private readonly string paramJump = "isJump";
        private readonly string jumpAniName = "Jump";
        private readonly string paramCrouch = "isCrouch";
        private readonly string crouchAniName = "Crouch";
        private readonly string idleAniName = "Idle";


        enum EPlayer { PLAYER1, PLAYER2, PLAYER3, PLAYER4 }
        enum EPlayerState { RUNNING, MISSION }

        // TODO<이종진> - 테스트용 작성 수정필요 - 20241110
        [SerializeField] private GameSceneController gameSceneController;

        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float jumpForce = 5.0f;

        [SerializeField] private float increaseSpeed = 0.1f;
        [SerializeField] private float decreaseSpeed = 0.5f;

        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField] private float maxSpeed = 10.0f;

        private EPlayer playerId;
        private RunningState runningController;
        private EPlayerState curState;

        private int isGrounded = 0;
        private bool isActivated = false;

        private Rigidbody rigid;
        private Animator animator;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            InputManager.Instance.KeyAction -= OnKeyBoard;
            InputManager.Instance.KeyAction += OnKeyBoard;

            for(int playerNum = 0; playerNum < playerTag.Length; playerNum++)
            {
                if (this.tag != playerTag[playerNum]) continue;
                playerId = (EPlayer)playerNum;
                break;
            }

            runningController = gameSceneController.GetComponent<RunningState>();
            curState = EPlayerState.RUNNING;

            animator.SetFloat(paramSpeed, speed);
        }

        private void Update()
        {
            UpdateState();
            if (curState == EPlayerState.RUNNING)
                Move();
        }

        private void OnKeyBoard()
        {
            if (isGrounded <= 0 || isActivated)
                return;

            if (curState == EPlayerState.RUNNING
                && (playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.S))
                || (playerId == EPlayer.PLAYER2 && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                IncreaseSpeed();
            }
            if ((playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.W))
                || (playerId == EPlayer.PLAYER2 &&  Input.GetKeyDown(KeyCode.UpArrow)))
            {
                Jump();
            }

            if (curState == EPlayerState.RUNNING)
                return;

            if ((playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.A))
                || (playerId == EPlayer.PLAYER2 && Input.GetKeyDown(KeyCode.LeftArrow)))
            {

            }
            if ((playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.D))
                || (playerId == EPlayer.PLAYER2 && Input.GetKeyDown(KeyCode.RightArrow)))
            {

            }
            if ((playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.LeftControl))
                || (playerId == EPlayer.PLAYER2 && Input.GetKeyDown(KeyCode.RightControl)))
            {
                Crouch();
            }
            if ((playerId == EPlayer.PLAYER1 && Input.GetKeyDown(KeyCode.LeftShift))
                || (playerId == EPlayer.PLAYER2 && Input.GetKeyDown(KeyCode.RightShift)))
            {

            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(groundTag))
                return;
            animator.SetBool(paramJump, false);
            isGrounded++;
        }
        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag(groundTag))
                return;
            isGrounded--;
        }
        private void UpdateState()
        {
            if (curState != EPlayerState.RUNNING
                && gameSceneController.CurState == EGameState.RUNNING)
                SetRunningState();
            else if (curState != EPlayerState.MISSION
                  && gameSceneController.CurState != EGameState.RUNNING)
                SetMissionState();
        }
        private void SetRunningState()
        {
            curState = EPlayerState.RUNNING;
            animator.SetBool(paramMission, false);
            transform.position = runningController.GetPlayerPrevPosition((int)playerId);
        }
        private void SetMissionState()
        {
            curState = EPlayerState.MISSION;
            animator.SetBool(paramMission, true);
            // TODO<이종진> - 상태 전환시 임시 플레이어 위치 수정 필요 - 20241112
            transform.position = new Vector3(148f + (int)playerId * 4f, 2.0f, 0.0f);
        }
        
        private void Move()
        {
            DecreaseSpeed();

            if (!runningController.IsBeyondMaxDistance(this.transform.position))
                return;
            if (runningController.IsUnderMinDistance(this.transform.position, out Vector3 curPos)
                && speed < runningController.DinosaurSpeed)
            {
                this.transform.position = curPos;
                return;
            }

            transform.Translate(transform.forward * Time.deltaTime * speed);
        }
        private void Jump()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(jumpAniName))
                animator.Play(jumpAniName, -1, 0f);
            animator.SetBool(paramJump, true);
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        private void Crouch()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(crouchAniName))
                return;
            StartCoroutine(CrouchActive());
        }
        private void IncreaseSpeed()
        {
            if (speed > maxSpeed)
                return;
            speed += increaseSpeed;
            animator.SetFloat(paramSpeed, speed);
        }
        private void DecreaseSpeed()
        {
            if (speed < minSpeed)
                return;
            speed -= Time.deltaTime * decreaseSpeed;
            animator.SetFloat(paramSpeed, speed);
        }

        IEnumerator CrouchActive()
        {
            animator.SetBool(paramCrouch, true);
            isActivated = true;
            yield return new WaitForSeconds(0.01f);
            float curAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(curAnimationTime);
            isActivated = false;
            animator.SetBool(paramCrouch, false);
        }
    }
}