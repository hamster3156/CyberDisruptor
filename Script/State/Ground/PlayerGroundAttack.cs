using UnityEngine;
using Arbor;
using UniRx;
using System;

[AddComponentMenu("")]
public class PlayerGroundAttack : StateBehaviour
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
    PlayerCore playerCore;

    [Header("配列の0番目には何も設定しないように")]
    [Header("攻撃コライダー")]
    public Collider[] GroundAtkCols;

    [Header("攻撃エフェクト")]
    public ParticleSystem[] GroundAtkEffects;

    // 攻撃入力を判定するフラグ
    [SerializeField] 
    private bool isGroundAtkInput = false;

    // 攻撃番号
    [SerializeField]
    private int groundAtkNum = 0;

    // ステート遷移先
    public StateLink Ground;

    // Use this for enter state
    public override void OnStateBegin()
	{
        movement.IsMov = false;
        // 攻撃処理の実行
        GroundAtkStart();
    }

	// Use this for exit state
	public override void OnStateEnd() 
    {
        movement.IsMov = true;

        // 攻撃番号が初期化される前に攻撃コライダーを非表示にする
        GroundAtkCols[groundAtkNum].enabled = false;

        // ステート終了時に初期化
        groundAtkNum = 0;
        isGroundAtkInput = false;
    }
	
	// OnStateUpdate is called once per frame
	public override void OnStateUpdate()
    {
        // 攻撃入力ができる時に、入力があれば上と同じことを行う
        if (inputActions.Player.Attack.triggered && isGroundAtkInput == false)
        {
            GroundAtkStart();
        }
	}

    // 攻撃アニメーションの呼び出し、攻撃入力を出来ないようにする
    private void GroundAtkStart()
    {
        isGroundAtkInput = true;
        groundAtkNum++;
        animator.Play($"Attack_{groundAtkNum}");
    }

    public void GroundAtkInputInit()
    {
        isGroundAtkInput = false;
    }

    // 攻撃アニメーションの番号に応じて攻撃コライダーを有効にする
    public void GroundAtkColStart()
    {
        if (isGroundAtkInput == false) return;
        GroundAtkCols[groundAtkNum].enabled = true;
    }

    // 攻撃アニメーションの番号に応じて攻撃コライダーを無効にする
    public void GroundAtkColInit()
    {
        if (isGroundAtkInput == false) return;
        GroundAtkCols[groundAtkNum].enabled = false;
    }

    // 攻撃アニメーションの番号に応じて攻撃エフェクトを再生する
    public void GroundAtkEffect()
    {
        if (isGroundAtkInput == false) return;
        GroundAtkEffects[groundAtkNum].Play();
    }

    // 攻撃アニメーションの番号に応じてプレイヤーを前方に移動させる
    public void GroundAtkMov()
    {
        if (isGroundAtkInput == false) return;
        rb.AddForce(transform.forward * playerCore.GroundMovDis[groundAtkNum], ForceMode.Impulse);
        Observable.Timer(TimeSpan.FromSeconds(playerCore.GroundMovExitTime[groundAtkNum])).Subscribe(_ => movement.MovVelocityXZInit());
    }

    // 攻撃終了時にステートを地面状態に遷移する
    public void GroundAtkStateExit()
    {
        Transition(Ground);
    }
}
