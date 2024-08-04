using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField]  Animator animator;

    // 地面判定のスフィアの半径
    private float GroundCheckRadius = 0.3f;

    // 地面判定のスフィアのY軸オフセット
    private float GroundCheckOffsetY = 0.4f;

    // 地面判定のスフィアの長さ
    private float GroundCheckDistance = 0.2f;

    // 地面に接地しているかどうか
    public bool IsGround = true;

    // Animatorのパラメータ名
    private readonly int hashGround = Animator.StringToHash("isGround");
    
    private LayerMask groundLayer;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        Ray();
        AnimatorParameterMatch();
    }

    // プレイヤーの足元にスフィアのレイを飛ばして地面に接地しているか判定する
    private void Ray()
    {
        RaycastHit hit;
        Nomnom.RaycastVisualization.VisualPhysics.SphereCast(transform.position + GroundCheckOffsetY * Vector3.up,
            GroundCheckRadius, Vector3.down, out hit, GroundCheckDistance, groundLayer);
        if (Physics.SphereCast(transform.position + GroundCheckOffsetY * Vector3.up,
            GroundCheckRadius, Vector3.down, out hit, GroundCheckDistance, groundLayer))
        {
            IsGround = true;
        }
        else IsGround = false;
    }

    // Animatorのパラメータと地面判定を同期させる
    private void AnimatorParameterMatch()
    {
        animator.SetBool(hashGround, IsGround);
    }
}
