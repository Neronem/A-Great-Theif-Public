using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 20f;
    public int maxJumps = 2;
   
    public Collider2D playerCollider;
    public Collider2D slideCollider;

    public Collider2D groundDetector;
    public LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    public Animator animator;
    private int jumpCount;
    private bool isGrounded;
    private bool isSliding;
    private bool isJumping = false; 
    private bool isDoubleJumping = false;
    
    public AudioClip jumpAudio; // 점프 시 재생할 효과음
    public AudioClip doubleJumpAudio; // 더블점프 시 재생할 효과음
    public AudioClip slideAudio; // 슬라이드 시 재생할 효과음
    
    // 아이템 중첩 획득 시 지속시간 갱신하기 위한 값 
    public bool isSpeedBuffRunning = false; // 스피드아이템 적용여부
    public Coroutine speedBuffCoroutine = null; // 스피드아이템 로직 코루틴
    public float speedBuffAmount = 0f; // 스피드아이템 적용값
    public void SetJumpForce(float newJump)
    {
        jumpForce = newJump;
    }
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerCollider.enabled = true;
        slideCollider.enabled = false;
    }
    public void CheckGround()
    {
        isGrounded = groundDetector.IsTouchingLayers(groundLayer);
        if (isGrounded)
        {
            isJumping = false;
            isDoubleJumping = false;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsGround", true);
            jumpCount = 0;
        }
        else
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsGround", false);
        }
    }

    public void HandleJump()
    {
        if (jumpCount == 0 && isGrounded && Input.GetKeyDown(PlayerInputSettings.jumpKey)) 
        {
            jumpCount = 1;
            if(jumpAudio != null) 
                SoundManager.PlayClip(jumpAudio);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("IsJump", true);
        }
        else if (!isGrounded && jumpCount < maxJumps && Input.GetKeyDown(PlayerInputSettings.jumpKey))
        {
            jumpCount = 2;
            if(doubleJumpAudio != null) 
                SoundManager.PlayClip(doubleJumpAudio);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isDoubleJumping = true;
            animator.SetTrigger("IsDoubleJump");
        }
    }

    public void HandleSlide()
    {
        if (isGrounded && Input.GetKey(PlayerInputSettings.slideKey) && !isSliding)
        {
            if(slideAudio != null) 
                SoundManager.PlayClip(slideAudio);
            isSliding = true;
            animator.SetBool("IsSliding", true);
            playerCollider.enabled = false;
            slideCollider.enabled = true;
        }
        else if (isSliding && Input.GetKeyUp(PlayerInputSettings.slideKey))
        {
            isSliding = false;
            animator.SetBool("IsSliding", false);
            playerCollider.enabled = true;
            slideCollider.enabled = false;
        }
    }

}
