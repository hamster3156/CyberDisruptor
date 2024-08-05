using UnityEngine;
using System;
using Arbor;
using UniRx;

[AddComponentMenu("")]
public class PlayerGroundShift : StateBehaviour 
{
	[SerializeField] 
    private Animator animator;

	[SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private PlayerCore playerCore;

    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private PlayerGroundCheck groundCheck;

    [SerializeField]
    private PlayerThrowWeapon throwWeapon;

    // シフト移動中かどうかの判定フラグ
    private bool isShiftMov = false;

    // 地面ステートに遷移するリンク
    public StateLink Ground;

    // 空中ステートに遷移するリンク
    public StateLink Air;

    // Use this for enter state
    public override void OnStateBegin() 
	{
        animator.Play("GroundShift_Start");		
        movement.IsMov = false;

        // ステート開始時に地面処理の判定によって初期化を行う
        movement.StateChangeInit();
        playerCore.Hp.Value -= playerCore.ShiftHpDecrease;
    }

    // Use this for exit state
    public override void OnStateEnd() 
	{
        movement.IsMov = true;
    }

    private void FixedUpdate()
    {
        if (isShiftMov == true)
        {
            // 指定した時間の間移動する
            rb.AddForce(transform.forward * playerCore.ShiftMovDis * Time.deltaTime, ForceMode.Impulse);
            Observable.Timer(TimeSpan.FromSeconds(playerCore.ShiftMovTime))
                .Subscribe(_ => ShiftMovInit())
                .AddTo(this);
        }
    }

    // Animationの再生速度を0にして停止させる
    public void ShiftMotionStop()
	{
		animator.speed = 0;
        Observable.Timer(TimeSpan.FromSeconds(playerCore.ShiftMovStartTime))
            .Subscribe(_ => isShiftMov = true)
            .AddTo(this);
    }

    // シフト移動の開始時に武器を投げる
    public void ShiftThrow()
    {
       throwWeapon.ThrowWeaponStart();
    }

    // シフト移動が終了した時にAnimatorの再生速度を元に戻す
    public void ShiftMovInit()
    {
        isShiftMov = false;
        animator.speed = 1.0f;
    }

    // シフト移動が終了した時に地面判定によってステートを遷移する
    public void ShiftStateExit()
	{
        if(groundCheck.IsGround == true) Transition(Ground);
        else Transition(Air);
	}
}
