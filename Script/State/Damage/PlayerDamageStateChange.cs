using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerDamageStateChange : StateBehaviour 
{
    [SerializeField]
    private PlayerCore playerCore;

    // プレイヤーの被ダメージ時の角度
    public float HitAngle;

    // ダメージ時の状態遷移リンク
    public StateLink Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            // プレイヤーの被ダメージ時の角度を取得
            var hitPos = Vector3.SignedAngle(transform.forward, other.transform.forward, Vector3.up);
            playerCore.HitAngle.Value = hitPos;

            // ダメージ時の状態遷移リンクを実行
            Transition(Damage);
        }
    }
}
