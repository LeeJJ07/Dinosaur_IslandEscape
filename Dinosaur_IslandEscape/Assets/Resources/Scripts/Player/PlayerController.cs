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
        private readonly string paramSpeed = "speed";
        private readonly string paramJump = "isJump";
        private readonly string jumpAniName = "Jump";

        enum EPlayer { PLAYER1, PLAYER2, PLAYER3, PLAYER4 }

        // TODO<이종진> - 테스트용 작성 수정필요 - 20241110
        [SerializeField] private GameSceneController gameSceneController;

        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float jumpForce = 5.0f;

        [SerializeField] private float increaseSpeed = 0.1f;
        [SerializeField] private float decreaseSpeed = 0.5f;

        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField] private float maxSpeed = 10.0f;

        private RunningState runningController;

        private bool isGrounded = true;

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

            runningController = gameSceneController.GetComponent<RunningState>();

            animator.SetFloat(paramSpeed, speed);
        }

        private void Update() 
        {
            if(gameSceneController.CurState == EGameState.RUNNING)
                Move();
        }

        private void OnKeyBoard() 
        {
            if (!isGrounded)
                return;

            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.W))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.UpArrow))) 
            {
                IncreaseSpeed();
            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.S))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.DownArrow))) 
            {

            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.R))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.Keypad4))) 
            {
                Jump();
            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.T))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.Keypad5))) 
            {

            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.F))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.Keypad1))) 
            {

            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.G))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.Keypad2))) 
            {

            }
        }
        private void OnCollisionEnter(Collision collision) 
        {
            if (collision == null)
                return;
            if (!collision.gameObject.CompareTag(groundTag))
                return;
            animator.SetBool(paramJump, false);
            isGrounded = true;
        }
        private void OnCollisionExit(Collision collision) 
        {
            if (collision == null)
                return;
            if (collision.gameObject.CompareTag(groundTag))
                isGrounded = false;
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
            animator.Play(jumpAniName, -1, 0f);
            animator.SetBool(paramJump, true);
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void DecreaseSpeed() 
        {
            if (speed < minSpeed)
                return;
            speed -= Time.deltaTime * decreaseSpeed;
            animator.SetFloat(paramSpeed, speed);
        }
        private void IncreaseSpeed() 
        {
            if (speed > maxSpeed)
                return;
            speed += increaseSpeed;
            animator.SetFloat(paramSpeed, speed);
        }
    }
}