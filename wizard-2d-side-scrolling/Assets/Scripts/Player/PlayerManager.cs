using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerBehavior
{
    Normal, UIShowing
}

public class PlayerManager : MonoBehaviour
{
    public event Action onPlayerStartMove;
    public event Action onPlayerEndMove;
    public event Action onPlayerJump;
    public event Action onPlayerInteract;

    CapsuleCollider2D col;
    Rigidbody2D rb;
    SpriteRenderer spriteRen;
    Animator anim;
    InputManager inputManager;
    [HideInInspector] public PlayerUI playerUI;

    [HideInInspector] public Vector2 mousePos;

    [Header("===== Behavior =====")]
    [SerializeField] PlayerBehavior curBehavior;

    [Header("===== Movement =====")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public Vector2 moveInput;

    [Header("===== Jump =====")]
    [SerializeField] float jumpForce;
    [Header("- Jump Condition")]
    [SerializeField] LayerMask jumpMask;
    [SerializeField] float jump_checkAreaSize;
    bool canJump;

    [Header("===== Interact =====")]
    [SerializeField] float interactSize;
    [SerializeField] LayerMask interactMask;
    IInteractable curIInteract;

    [Header("===== Storage =====")]
    [HideInInspector] public Storage curSelectStorage;


    private void OnEnable()
    {
        onPlayerStartMove += HandleStartMoveAnim;
        onPlayerEndMove += HandleEndMoveAnim;

        onPlayerJump += Jump;

        onPlayerInteract += Interact;
    }

    private void OnDisable()
    {
        onPlayerStartMove -= HandleStartMoveAnim;
        onPlayerEndMove -= HandleEndMoveAnim;

        onPlayerJump -= Jump;

        onPlayerInteract -= Interact;
    }

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();
    }

    private void FixedUpdate()
    {
        UpdateBehavior();
        MoveHandle();
        CheckJumpCondition();
        CheckInteractCondition();
    }

    #region Player Behavior
    public void SwitchBehavior(PlayerBehavior behavior)
    {
        curBehavior = behavior;
        switch (curBehavior)
        {
            case PlayerBehavior.Normal:
                break;
            case PlayerBehavior.UIShowing:
                break;
        }
    }

    public void UpdateBehavior()
    {
        switch (curBehavior)
        {
            case PlayerBehavior.Normal:
                break;
            case PlayerBehavior.UIShowing:
                break;
        }
    }

    public bool IsBehavior(PlayerBehavior behavior)
    {
        return curBehavior == behavior;
    }

    #endregion

    #region Movement Controller

    public void StartMove(InputAction.CallbackContext context)
    {
        if (IsBehavior(PlayerBehavior.Normal))
        {
            moveInput = context.ReadValue<Vector2>();
            onPlayerStartMove?.Invoke();
        }
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

        if (moveInput.x != 0) Anim_SetBool("isMove", true);
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
        canJump = Physics2D.OverlapCircle(feetPos, jump_checkAreaSize, jumpMask);

        OnAirAnimation();
    }

    public void JumpPerformed()
    {
        if (canJump && IsBehavior(PlayerBehavior.Normal))
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

    #region Interact

    void CheckInteractCondition()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, interactSize, interactMask);
        if (col != null)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                playerUI.SetInteractText(interactable.GetInteractText());
                playerUI.ShowInteractText();
                curIInteract = interactable;
            }
            else
            {
                playerUI.HideInteractText();
                curIInteract = null;
            }
        }
        else
        {
            playerUI.HideInteractText();
            curIInteract = null;
        }
    }

    public void InteractPerformed()
    {
        if (curIInteract != null)
        {
            onPlayerInteract?.Invoke();
        }
    }

    void Interact()
    {
        curIInteract.Interact();
    }

    #endregion

    #region Storage

    public Transform GetStorageSlot(int index)
    {
        return playerUI.storageInventory.transform.GetChild(index);
    }

    public Transform GetInventorySlot(int index)
    {
        return playerUI.mainInventory.transform.GetChild(index);
    }

    public bool HasFreeSlotInInventory(out int slotIndex)
    {
        for (int i = 0; i < playerUI.mainInventory.transform.childCount; i++)
        {
            if (playerUI.mainInventory.transform.GetChild(i).childCount == 0)
            {
                slotIndex = i;
                return true;
            }
        }
        slotIndex = -1;
        return false;
    }

    public bool HasFreeSlotInStorage(out int slotIndex)
    {
        for (int i = 0; i < playerUI.storageInventory.transform.childCount; i++)
        {
            if (playerUI.storageInventory.transform.GetChild(i).childCount == 0)
            {
                slotIndex = i;
                return true;
            }
        }
        slotIndex = -1;
        return false;
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
