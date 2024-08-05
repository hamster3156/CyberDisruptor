using UnityEngine;
using System;
using Arbor;
using UniRx;

[AddComponentMenu("")]
public class PlayerAirShift : StateBehaviour 
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

    // シフト移動のフラグ
    private bool isShiftMov = false;

    // 状態遷移のリンク
    public StateLink Air;
    public StateLink Ground;

	// Use this for awake state
	public override void OnStateAwake(){
    }

    // Use this for enter state
    public override void OnStateBegin() 
	{
        // ステートの開始時にアニメーションを再生する
        animator.Play("AirShift_Start");
        movement.IsMov = false;

        // ステート開始時の地面状態に応じて初期化処理を行う
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
        // シフト移動の処理
        if(isShiftMov == true)
        {
            rb.AddForce(transform.forward * playerCore.ShiftMovDis * Time.deltaTime, ForceMode.Impulse);
            Observable.Timer(TimeSpan.FromSeconds(playerCore.ShiftMovTime))
                .Subscribe(_ => AirShiftMovInit())
                .AddTo(this);
        }
    }

    public void AirShiftMotionStop()
	{
        // シフト移動のアニメーションを停止する
        animator.speed = 0;
		Observable.Timer(TimeSpan.FromSeconds(playerCore.ShiftMovStartTime))
            .Subscribe(_ => isShiftMov = true)
            .AddTo(this);
    }

    public void AirShiftThrow()
    {
        // シフト移動開始時にに武器を投げる
        throwWeapon.ThrowWeaponStart();
    }

    public void AirShiftMovInit()
    {
        // シフト移動が終了する時にアニメーションの再生速度を元に戻す
        isShiftMov = false;
        animator.speed = 1.0f;
    }

    public void AirShiftStateExit()
	{
        // シフト移動が終了する時に地面の状態に応じてステートを遷移する
        if (groundCheck.IsGround == true) Transition(Ground);
        else Transition(Air);
    }
}
