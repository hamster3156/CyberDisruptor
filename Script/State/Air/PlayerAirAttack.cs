using UnityEngine;
using System;
using Arbor;
using UniRx;

[AddComponentMenu("")]
public class PlayerAirAttack : StateBehaviour 
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private PlayerControls inputActions;

    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private PlayerGroundCheck groundCheck;

    [SerializeField]
    private PlayerCore playerCore;

    [Header("配列の0番目には何も設定しないように")]
    [Header("空中弱攻撃コライダー")]
    public Collider[] AirAtkCols;

    [Header("空中弱攻撃エフェクト")]
    public ParticleSystem[] AirAtkEffects;

    // 攻撃入力判定
    [SerializeField]
    private bool isAirAtkInput = false;

    // 攻撃番号
    [SerializeField]
    private int airAtkNum = 0;

    // ステート遷移先
    public StateLink Air;
    public StateLink Ground;

    // Use this for enter state
    public override void OnStateBegin()
    {
        // プレイヤーを移動できないようにする
        movement.IsMov = false;

        // 攻撃処理の実行
        AirAtkStart();
    }

    // Use this for exit state
    public override void OnStateEnd()
    {
        movement.IsMov = true;

        // 攻撃番号が初期化される前に攻撃コライダーを非表示にする
        AirAtkCols[airAtkNum].enabled = false;

        // 攻撃番号が初期化される前に攻撃コライダーを非表示にする
        AirAtkCols[airAtkNum].enabled = false;

        // ステート終了時に初期化
        airAtkNum = 0;
        isAirAtkInput = false;
    }

    // OnStateUpdate is called once per frame
    public override void OnStateUpdate()
    {
        // 攻撃入力ができる時に、入力があれば攻撃処理を実行
        if (inputActions.Player.Attack.triggered && isAirAtkInput == false)
        {
            AirAtkStart();
        }

        // 地面に着地していて、空中攻撃の入力がない場合地上状態に遷移する
        if (groundCheck.IsGround == true && isAirAtkInput == false) Transition(Ground);
    }

    // 攻撃アニメーションの呼び出し、攻撃入力を出来ないようにする
    private void AirAtkStart()
    {
        isAirAtkInput = true;
        airAtkNum++;
        animator.Play($"AirAttack_{airAtkNum}");
    }

    public void AirAtkInputInit()
    {
        isAirAtkInput = false;
    }

    public void AirAtkColStart()
    {
        if (isAirAtkInput == false) return;
        AirAtkCols[airAtkNum].enabled = true;
    }

    // 攻撃番号に応じてコライダーを非表示にする
    public void AirAtkColInit()
    {
        if (isAirAtkInput == false) return;
        AirAtkCols[airAtkNum].enabled = false;
    }

    // 空中攻撃のエフェクトを再生する
    public void AirAtkEffect()
    {
        if (isAirAtkInput == false) return;
        AirAtkEffects[airAtkNum].Play();
    }

    // 空中攻撃時に前方方向にどうさせる
    public void AirAtkMov()
    {
        if (isAirAtkInput == false) return;
        rb.AddForce(transform.forward * playerCore.GroundMovDis[airAtkNum], ForceMode.Impulse);
        Observable
            .Timer(TimeSpan.FromSeconds(playerCore.GroundMovExitTime[airAtkNum]))
            .Subscribe(_ => movement.MovVelocityXZInit())
            .AddTo(this);
    }

    // 空中攻撃ステートの終了時に地面状態に応じて状態を遷移する
    public void AirAtkStateExit()
    {
        if(groundCheck.IsGround == true) Transition(Ground);
        else Transition(Air);
    }
}