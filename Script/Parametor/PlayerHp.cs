using System;
using UnityEngine;
using UniRx;

public class PlayerHp : MonoBehaviour, IHpRecoveryAble, IDamageAble
{
    [SerializeField]
    private PlayerCore playerCore;

    private bool isProcessExit = false;

    private void Start()
    {
        playerCore.Hp.Value = playerCore.MaxHp.Value;

        // UniRxのUpdate処理 Startで宣言するとUpdateのように使う事が出来る
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                if (isProcessExit == true) return;

                if (playerCore.Hp.Value <= 0)
                {
                    playerCore.HpDecrease = 0;
                    playerCore.Hp.Value = 0;
                    isProcessExit = true;
                }  
            })
            .AddTo(gameObject);

        HpDecreaseExecution();
    }

    private void HpDecreaseExecution()
    {
        // 時間経過で処理を繰り返す
        Observable.Interval(TimeSpan.FromSeconds(playerCore.HpDecreaseTime))
            // 登録した処理を行う
            .Subscribe(_ => playerCore.Hp.Value -= playerCore.HpDecrease)
            // GameObjectが破棄された時に、処理を破棄する
            // （破棄しないと、実行中に処理が動き続けてnullエラーが出る）
            .AddTo(this);
    }

    // HP回復処理
    public void TakeHpRecovery(int HpRecovery)
    {
        playerCore.Hp.Value += HpRecovery;

        if (playerCore.Hp.Value >= playerCore.MaxHp.Value) playerCore.Hp.Value = playerCore.MaxHp.Value;
    }

    // ダメージ処理
    public void TakeDamage(int Attack)
    {
        playerCore.Hp.Value -= Attack;
    }
}
