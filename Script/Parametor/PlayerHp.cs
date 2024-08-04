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

        // UniRx��Update���� Start�Ő錾�����Update�̂悤�Ɏg�������o����
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
        // ���Ԍo�߂ŏ������J��Ԃ�
        Observable.Interval(TimeSpan.FromSeconds(playerCore.HpDecreaseTime))
            // �o�^�����������s��
            .Subscribe(_ => playerCore.Hp.Value -= playerCore.HpDecrease)
            // GameObject���j�����ꂽ���ɁA������j������
            // �i�j�����Ȃ��ƁA���s���ɏ���������������null�G���[���o��j
            .AddTo(this);
    }

    // HP�񕜏���
    public void TakeHpRecovery(int HpRecovery)
    {
        playerCore.Hp.Value += HpRecovery;

        if (playerCore.Hp.Value >= playerCore.MaxHp.Value) playerCore.Hp.Value = playerCore.MaxHp.Value;
    }

    // �_���[�W����
    public void TakeDamage(int Attack)
    {
        playerCore.Hp.Value -= Attack;
    }
}
