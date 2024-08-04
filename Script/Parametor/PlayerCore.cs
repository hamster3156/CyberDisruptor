using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerCore : MonoBehaviour
{
    // �ړ����x
    public int RunSpd = 10;

    // �����]�����x
    public float TurnSpd = 0.12f;

    // �󒆂̕����]�����x
    public float AirTurnSpd = 1;

    // �v���C���[�̗̑�
    public ReactiveProperty<int> Hp = new ReactiveProperty<int>(0);

    // �̗͂̍ő�l
    public ReactiveProperty<int> MaxHp = new ReactiveProperty<int>(100);

    // �̗͂̌�������
    public float HpDecreaseTime =  2;

    // �̗͂̌����l
    public int HpDecrease = 1;

    // �v���C���[�̍U����
    public int Offense = 10;

    // �v���C���[�̖h���
    public int Defense = 5;

    // �n��U���̍U���{��
    public float[] GroundAtkPow = { 0, 1, 1, 1.1f, 1.2f, 1.3f, 1.5f, 2 };

    // �n��U���̈ړ�����
    public int[] GroundMovDis = { 0, 10, 10, 10, 12, 13, 14, 15 };

    // �n��U���ړ��̏I������
    public float[] GroundMovExitTime = { 0, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };

    // �󒆍U���̍U���{��
    public float[] AirAtkPow = { 0, 1, 1, 1, 1, 1.2f, 1.5f };

    // �󒆍U���̈ړ�����
    public int[] AirAtkMovDis = { 0, 10, 10, 10, 10, 10, 15 };

    // �󒆍U���ړ��̏I������
    public float[] AirMovExitTime = { 0, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};

    // �W�����v��
    public int JumpPow = 200;

    // �������
    public int DodgeDis = 300;

    // �V�t�g�̗̑͌����l
    public int ShiftHpDecrease = 10;

    // �V�t�g�̈ړ�����
    public int ShiftMovDis = 500;

    //�ړ��J�n����
    public float ShiftMovStartTime = 0.1f;

    // �V�t�g�̈ړ�����
    public float ShiftMovTime = 0.1f;

    // �G����U�����󂯂����̕���
    public ReactiveProperty<float> HitAngle;
}
