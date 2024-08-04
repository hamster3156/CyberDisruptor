using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField] 
    private PlayerGroundShift groundShift;

    [SerializeField] 
    private PlayerAirShift airShift;

    [SerializeField]
    private Collider col;

    // 壁のレイヤー
    private LayerMask wallLayer;

    // 壁に向かって飛ばすレイのオフセット
    private float offset = 1.5f;

    // レイの長さ
    private float dis = 2.0f;

    // 壁に当たっているかどうか
    private bool isWallCheck;

    private void Start()
    {
        wallLayer = LayerMask.GetMask("Wall");
    }

    private void FixedUpdate()
    {
        // 壁に当たっているなら移動関連の初期化処理を行う
        if (isWallCheck == true)
        {
            RaycastHit hit;
            Nomnom.RaycastVisualization.VisualPhysics.Raycast(transform.position + offset * Vector3.up, transform.forward, out hit, dis, wallLayer);

            if (Physics.Raycast(transform.position + offset * Vector3.up, transform.forward, out hit, dis, wallLayer))
            {
                movement.MovVelocityXZInit();
                groundShift.ShiftMovInit();
                airShift.AirShiftMovInit();
            }
        }
    }

    // プレイヤーの当たり判定を無効にする処理
    public void InActiveCollider()
    {
        col.enabled = false;
        isWallCheck = true;
    }

    // プレイヤーの当たり判定を有効にする処理
    public void ActiveCollider()
    {
        col.enabled = true;
        isWallCheck = false;
    }
}
