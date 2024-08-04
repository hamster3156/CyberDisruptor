using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerDamage : StateBehaviour
{
	[SerializeField]
	private Animator animator;

	[SerializeField] 
	private Rigidbody rb;

	[SerializeField] 
	private PlayerMovement movement;

	[SerializeField]
	private PlayerGroundCheck groundCheck;

    [SerializeField]
	private PlayerCore playerCore;

	[SerializeField]
	private PlayerAirInputStateChange airInputStateChange;

	[SerializeField]
	private bool isDamage;

    // 攻撃があたったかどうか
    private bool isHit;

    // 吹き飛び判定
    public bool IsBackBlownAway;

    // Animatorのパラメータ名を設定
    private readonly int IS_DAMAGE = Animator.StringToHash("IsDamage");

    // ダメージを受けた回数
    [SerializeField]
    private int hitCnt = 1;

    [Header("前怯み距離")]
    public float[] GroundForwardDis = { 0, 1, 3, 1 };

    [Header("後怯み距離")]
    public float[] GroundBackDis = { 0, 0.5f, 2, 3 };

    [Header("左怯み距離")]
    public float[] GroundLeftDis = { 0, 1, 3, 4 };

    [Header("右怯み距離")]
    public float[] GroundRightDis = { 0, 1, 3, 4 };

    // 状態遷移のリンク
    public StateLink Ground;
	public StateLink Air;
	public StateLink Death;

    // ダメージを受けた角度
    private float damageAngle;

    public override void OnStateBegin()
    {
        // ステート開始時にダメージアニメーションを再生する
        animator.SetBool(IS_DAMAGE, true);
        movement.IsMov = false;
        isHit = true;
        hitCnt = 1;
        playerCore.HitAngle.Value = damageAngle;
        DamageMove();
    }

    // OnStateUpdate is called once per frame
    public override void OnStateUpdate()
	{
		if(playerCore.Hp.Value <= 0)
		{
            Transition(Death);
			return;
		}

		// Animatorのパラメータを取得
        isDamage = animator.GetBool("IsDamage");

        // ダメージフラグがfalseの時、ステートを遷移する
        if (groundCheck.IsGround == true && isDamage == false) 
		{
            Transition(Ground);
            airInputStateChange.AirJumpInit();
        }
		else if(groundCheck.IsGround == false && isDamage == false)
		{
            Transition(Air);
        }
    }

    private void FixedUpdate()
    {
        if (IsBackBlownAway == true) rb.AddForce(transform.forward * GroundForwardDis[hitCnt], ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerCore.Hp.Value <= 0 || isHit == true) return;

        if (other.CompareTag("EnemyAttack"))
        {
            // animatorのパラメータを変更、自動で次の遷移を行わないようにする
            animator.SetBool(IS_DAMAGE, true);
            isHit = true;
            hitCnt++;
            movement.IsMov = false;

            // 受けた攻撃の回数でダメージの怯みの強さを変更する
            // ダメージを与えてきたオブジェクトの正面座標と、プレイヤーの後方座標を計算する
            damageAngle = Vector3.SignedAngle(transform.forward, other.transform.forward, Vector3.up);
            DamageMove();
        }
    }

    private void DamageMove()
    {
        // 正面（正面と後方の呼び出しアニメーションを反対にすると、プレイヤーの前後で移動するようになる）
        if (damageAngle >= -30.0f && damageAngle <= 30.0f)
        {
            if (hitCnt == 3) IsBackBlownAway = true;
            else rb.AddForce(transform.forward * GroundForwardDis[hitCnt] * Time.deltaTime, ForceMode.Impulse);
            animator.Play($"DamageBack_{hitCnt}");
        }
        // 後方
        else if (damageAngle > 150.0f && damageAngle <= 180.0f ||
            damageAngle < -150.0f && damageAngle >= -180.0f)
        {
            rb.AddForce(-transform.forward * GroundBackDis[hitCnt] * Time.deltaTime, ForceMode.Impulse);
            animator.Play($"DamageFront_{hitCnt}");
        }
        // 左
        else if (damageAngle >= -150.0f && damageAngle < -30.0f)
        {
            rb.AddForce(-transform.right * GroundLeftDis[hitCnt] * Time.deltaTime, ForceMode.Impulse);
            animator.Play($"DamageLeft_{hitCnt}");
        }
        // 右
        else if (damageAngle <= 150.0f && damageAngle > 30.0f)
        {
            rb.AddForce(transform.right * GroundRightDis[hitCnt] * Time.deltaTime, ForceMode.Impulse);
            animator.Play($"DamageLeft_{hitCnt}");
        }
    }

    public void DamageStateExit()
    {
        animator.SetBool(IS_DAMAGE, false);
        movement.IsMov = true;
        hitCnt = 0;
        isDamage = false;
    }

    public void HitInit()
    {
        isHit = false;
    }

    public void BlownAwayInit()
    {
        IsBackBlownAway = false;
    }
}
