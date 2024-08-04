using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerDodge : StateBehaviour 
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private PlayerGroundCheck groundCheck;

    [SerializeField]
    private PlayerCore PlayerCore;

    // ステート遷移先のリンク
	public StateLink Ground;
    public StateLink Air;

    // Use this for enter state
    public override void OnStateBegin() 
	{
        movement.IsMov = false;
    }
    
    // 回避時にプレイヤーを前方方向に移動させる
    public void DodgeMove()
    {
        rb.AddForce(transform.forward * PlayerCore.DodgeDis * Time.deltaTime, ForceMode.Impulse);
    }

    // 回避状態の終了時に、地面状態に応じてステート遷移を行う
	public void DodgeStateExit()
	{
        movement.IsMov = true;
        if (groundCheck.IsGround == true) Transition(Ground);
        else Transition(Air);
    }
}
