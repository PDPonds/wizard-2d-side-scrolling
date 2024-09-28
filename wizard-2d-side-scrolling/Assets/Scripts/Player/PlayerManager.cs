using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Action
    public event Action onPlayerStartMove;
    public event Action onPlayerEndMove;
    public event Action onPlayerJump;
    #endregion

    #region Ref

    CapsuleCollider2D col;
    Rigidbody2D rb;
    SpriteRenderer spriteRen;
    Animator anim;
    InputManager inputManager;

    #endregion

    #region Movement
    [Header("===== Movement =====")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public Vector2 moveInput;
    #endregion

    #region Jump
    [Header("===== Jump =====")]
    [SerializeField] float jumpForce;
    [Header("- Jump Condition")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jump_checkAreaSize;
    bool canJump;
    #endregion

    private void OnEnable()
    {
        onPlayerStartMove += HandleStartMoveAnim;
        onPlayerEndMove += HandleEndMoveAnim;

        onPlayerJump += Jump;
    }

    private void OnDisable()
    {
        onPlayerStartMove -= HandleStartMoveAnim;
        onPlayerEndMove -= HandleEndMoveAnim;

        onPlayerJump -= Jump;
    }

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        MoveHandle();
        CheckJumpCondition();
    }

    #region Movement Controller

    public void StartMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        onPlayerStartMove?.Invoke();
    }

    public void EndMove(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        onPlayerEndMove?.Invoke();
    }

    void MoveHandle()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void HandleStartMoveAnim()
    {
        if (moveInput.x > 0) spriteRen.flipX = false;
        else if (moveInput.x < 0) spriteRen.flipX = true;
        Anim_SetBool("isMove", true);
    }

    void HandleEndMoveAnim()
    {
        Anim_SetBool("isMove", false);
    }

    #endregion

    #region Jump Controller

    void CheckJumpCondition()
    {
        Vector2 feetPos = transform.position + (-transform.up * (col.size.y / 2));
        canJump = Physics2D.OverlapCircle(feetPos, jump_checkAreaSize, groundMask);

        OnAirAnimation();
    }

    public void JumpPerformed()
    {
        if (canJump)
        {
            onPlayerJump?.Invoke();
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        Anim_Play("Jump");
    }

    void OnAirAnimation()
    {
        Anim_SetBool("onAir", !canJump);
    }

    #endregion

    #region Animation Controller

    public void Anim_SetBool(string variable, bool result)
    {
        anim.SetBool(variable, result);
    }

    public void Anim_SetFloat(string variable, float value)
    {
        anim.SetFloat(variable, value);
    }

    public void Anim_Play(string name)
    {
        anim.Play(name);
    }

    #endregion


}
