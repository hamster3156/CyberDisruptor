using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arbor;
using UnityEngine.InputSystem;

[AddComponentMenu("")]
public class PlayerGroundInputStateChange : StateBehaviour
{
	private PlayerControls inputActions;

	[SerializeField]
    private Animator animator;

    [Header("地上アクションを許可")]
	public bool IsGroundAct = true;

	[Header("ジャンプを許可")]
	public bool IsJump = true;

	[Header("回避を許可")]
	public bool IsDodge = true;

	[Header("攻撃を許可")]
	public bool IsAtk = true;

	[Header("シフトを許可")]
	public bool IsShift = true;

	[Header("ダメージを許可")]
	public bool IsDamage = true;

	private bool isDamage;

	// ステート遷移用のリンク
    public StateLink Jump;
	public StateLink Dodge;
	public StateLink Attack;
	public StateLink Shift;
	public StateLink Damage;

	// Use this for awake state
	public override void OnStateAwake() 
	{
		animator = GetComponent<Animator>();
    }

    // Use this for enter state
    public override void OnStateBegin() {
	}

	// Use this for exit state
	public override void OnStateEnd() {
	}
	
	// OnStateUpdate is called once per frame
	public override void OnStateUpdate() 
	{
		// 入力が許可されているか
		if (IsGroundAct == false) return;

		ChangeStateJump();
		ChangeStateDodge();
		ChangeStateAttack();
		ChangeStateShift();
		ChangeStateDamage();
	}

	// OnStateLateUpdate is called once per frame, after Update has finished.
	public override void OnStateLateUpdate() {
	}

    private void OnEnable()
    {
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void OnDisable()
    {
		inputActions.Disable();
    }

    private void ChangeStateJump() 
	{
		if (IsJump == false) return;
		if (inputActions.Player.Jump.triggered) Transition(Jump);
    }

	private void ChangeStateDodge()
	{
        if (IsDodge == false) return;
        if (inputActions.Player.Dodge.triggered) Transition(Dodge);
	}

	private void ChangeStateAttack()
	{
        if (IsJump == false) return;
		if (inputActions.Player.Attack.triggered) Transition(Attack);
    }

	private void ChangeStateShift()
	{
		if (IsShift == false) return;
		if (inputActions.Player.Shift.triggered) Transition(Shift);
	}

	private void ChangeStateDamage()
	{
		if (IsDamage == false) return;

        // Animatorのパラメータを取得
        isDamage = animator.GetBool("IsDamage");

        // ダメージフラグがtrueの時、ステートを遷移する
        if (isDamage == true) Transition(Damage);
    }
}
