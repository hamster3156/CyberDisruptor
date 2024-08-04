using UnityEngine;

public class PlayerHitAttackDamage : MonoBehaviour
{
    [SerializeField]
    private PlayerCore playerCore;

    [Header("���Ԗڂ̍U�����ݒ�")]
    public int AtkNum;

    [Header("�U����")]
    public int AtkPow;

    [Header("�n��U�����󒆍U�������肷��t���O")]
    public bool Ground;

    // ���������I�u�W�F�N�g���C���^�[�t�F�[�X�������Ă�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageAble damage = other.GetComponent<IDamageAble>();
            if (damage != null)
            {
                TotalDamage();
                damage.TakeDamage(AtkPow);
            }
        }
    }

    // ���݂̗^����_���[�W���v�Z
    private void TotalDamage()
    {
        if(Ground == true)
        {
            var cal = playerCore.Offense * playerCore.GroundAtkPow[AtkNum];
            AtkPow = (int)cal;
        }
        else
        {
            var cal = playerCore.Offense * playerCore.AirAtkPow[AtkNum];
            AtkPow = (int)cal;
        }
    }
}
