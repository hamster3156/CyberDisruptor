using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerGroundJump : StateBehaviour 
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    PlayerCore playerCore;

    // ジャンプ中の状態に遷移するためのリンク
    public StateLink Air;

    // プレイヤーをY軸方向に座標移動させる
    public void GroundJumpUp()
    {
        rb.AddForce(Vector3.up * playerCore.JumpPow, ForceMode.Impulse);
    }

    // ジャンプ状態から空中状態に遷移する
	public void GroundJumpStateExit()
	{
        Transition(Air);
    }
}
