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

    // �ǂ̃��C���[
    private LayerMask wallLayer;

    // �ǂɌ������Ĕ�΂����C�̃I�t�Z�b�g
    private float offset = 1.5f;

    // ���C�̒���
    private float dis = 2.0f;

    // �ǂɓ������Ă��邩�ǂ���
    private bool isWallCheck;

    private void Start()
    {
        wallLayer = LayerMask.GetMask("Wall");
    }

    private void FixedUpdate()
    {
        // �ǂɓ������Ă���Ȃ�ړ��֘A�̏������������s��
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

    // �v���C���[�̓����蔻��𖳌��ɂ��鏈��
    public void InActiveCollider()
    {
        col.enabled = false;
        isWallCheck = true;
    }

    // �v���C���[�̓����蔻���L���ɂ��鏈��
    public void ActiveCollider()
    {
        col.enabled = true;
        isWallCheck = false;
    }
}
