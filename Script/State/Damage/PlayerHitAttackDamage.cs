using UnityEngine;

public class PlayerHitAttackDamage : MonoBehaviour
{
    [SerializeField]
    private PlayerCore playerCore;

    [Header("何番目の攻撃か設定")]
    public int AtkNum;

    [Header("攻撃力")]
    public int AtkPow;

    [Header("地上攻撃か空中攻撃か判定するフラグ")]
    public bool Ground;

    // 当たったオブジェクトがインターフェースを持っていた時
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

    // 現在の与えるダメージを計算
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
