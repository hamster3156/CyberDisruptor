using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField]  Animator animator;

    // �n�ʔ���̃X�t�B�A�̔��a
    private float GroundCheckRadius = 0.3f;

    // �n�ʔ���̃X�t�B�A��Y���I�t�Z�b�g
    private float GroundCheckOffsetY = 0.4f;

    // �n�ʔ���̃X�t�B�A�̒���
    private float GroundCheckDistance = 0.2f;

    // �n�ʂɐڒn���Ă��邩�ǂ���
    public bool IsGround = true;

    // Animator�̃p�����[�^��
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

    // �v���C���[�̑����ɃX�t�B�A�̃��C���΂��Ēn�ʂɐڒn���Ă��邩���肷��
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

    // Animator�̃p�����[�^�ƒn�ʔ���𓯊�������
    private void AnimatorParameterMatch()
    {
        animator.SetBool(hashGround, IsGround);
    }
}
