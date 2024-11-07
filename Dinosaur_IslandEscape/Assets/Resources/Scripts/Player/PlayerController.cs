using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace JongJin
{
    public class PlayerController : MonoBehaviour
    {
        enum EPlayer { PLAYER1, PLAYER2, PLAYER3, PLAYER4 }
        private readonly string[] playerTag = { "Player1", "Player2", "Player3", "Player4" };
        private readonly string groundTag = "Ground";
        private readonly string paramSpeed = "speed";
        private readonly string paramJump = "isJump";
        private readonly string jumpAniName = "Jump";

        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float jumpForce = 5.0f;

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
            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.KeyAction += OnKeyBoard;

            animator.SetFloat(paramSpeed, speed);
        }
        
        private void Update()
        {
            Move();

            if (transform.CompareTag(playerTag[(int)EPlayer.PLAYER1])) BandController.SetX((int)EPlayer.PLAYER1, transform.position.z);
            else BandController.SetX((int)EPlayer.PLAYER2, transform.position.z);
        }

        private void OnKeyBoard()
        {
            if (!isGrounded)
                return;

            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.W))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.UpArrow)))
            {
                speed += 0.5f;
                animator.SetFloat(paramSpeed, speed);
            }
            if ((transform.CompareTag(playerTag[(int)EPlayer.PLAYER1]) && Input.GetKeyDown(KeyCode.S))
                || (transform.CompareTag(playerTag[(int)EPlayer.PLAYER2]) && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                //ToDo_이종진 : 임시 테스트용 코드 작성 
                speed -= 1f;
                animator.SetFloat(paramSpeed, speed);
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
            if (collision.gameObject.CompareTag(groundTag))
            { 
                animator.SetBool(paramJump, false);
                isGrounded = true;
            }
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
            transform.Translate(transform.forward * Time.deltaTime * speed);
        }
        private void Jump()
        {
            animator.Play(jumpAniName, -1, 0f);
            animator.SetBool(paramJump, true);
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}