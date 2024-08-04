using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerAirJump
    : StateBehaviour 
{
    [SerializeField] 
    private Rigidbody rb;

    [SerializeField]
    PlayerCore playerCore;

    // 状態遷移のリンク
    public StateLink Air;

    // プレイヤーをY座標方向にジャンプさせる
    public void AirJumpUp()
    {
        rb.AddForce(Vector3.up * playerCore.JumpPow, ForceMode.Impulse);
    }

    // ジャンプステートの終了時に空中状態に遷移する
	public void AirJumpStateExit()
	{
        Transition(Air);
    }
}
