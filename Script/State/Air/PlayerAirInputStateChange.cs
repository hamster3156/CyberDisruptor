using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerAirInputStateChange : StateBehaviour
{
    private PlayerControls inputActions;

    [SerializeField]
    private Animator animator;

    [SlotType(typeof(PlayerGroundCheck))]
    private FlexibleComponent groundCheck = new FlexibleComponent(FlexibleHierarchyType.Self);

    [Header("空中アクションを許可")]
    public bool IsAirAct = true;

    [Header("空中ジャンプを許可")]
    public bool IsAirJump = true;

    [Header("空中攻撃を許可")]
    public bool IsAirAtk = true;

    [Header("空中シフトを許可")]
    public bool IsAirShift = true;

    [Header("ダメージを許可")]
    public bool IsDamage = true;

    // ダメージフラグ
    private bool isDamage;

    // 空中ジャンプの使用判定
    private bool isAirJump;

    // 状態遷移のリンク
    public StateLink AirJump;
    public StateLink AirAttack;
    public StateLink AirShift;
    public StateLink Damage;

	// Use this for awake state
	public override void OnStateAwake()
	{
        groundCheck = (FlexibleComponent)GameObject.Find("Player").GetComponent<PlayerGroundCheck>();
        animator = GetComponent<Animator>();
    }

	// Use this for exit state
	public override void OnStateEnd() 
    {
        // ステート終了時に地面にいる時、ダブルジャンプ判定フラグを初期化
        var ground = groundCheck.value as PlayerGroundCheck;
        if(ground.IsGround == true) isAirJump = false;
	}
	
	// OnStateUpdate is called once per frame
	public override void OnStateUpdate()
    {
        // 入力が許可されているか
        if (IsAirAct == false) return;
        ChangeStateAirJump();
        ChangeStateAirAttack();
        ChangeAirShift();
        ChangeStateDamage();
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

    private void ChangeStateAirJump()
    {
        // 空中ジャンプが許可されているか or 空中ジャンプを行ったか
        if (IsAirJump == false || isAirJump == true) return;

        if (inputActions.Player.Jump.triggered)
        {
            Transition(AirJump);
            isAirJump = true;
        }
    }

    private void ChangeStateAirAttack()
    {
        if(IsAirAtk == false) return;
        if (inputActions.Player.Attack.triggered) Transition(AirAttack);
    }

    private void ChangeAirShift()
    {
        if (IsAirShift == false) return;
        if(inputActions.Player.Shift.triggered) Transition(AirShift);
    }

    private void ChangeStateDamage()
    {
        if (IsDamage == false) return;

        // Animatorのパラメータを取得
        isDamage = animator.GetBool("IsDamage");

        // ダメージフラグがtrueの時、ステートを遷移する
        if (isDamage == true) Transition(Damage);
    }

    public void AirJumpInit()
    {
        isAirJump = false;
    }
}
