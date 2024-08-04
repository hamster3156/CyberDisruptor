using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerCore : MonoBehaviour
{
    // 移動速度
    public int RunSpd = 10;

    // 方向転換速度
    public float TurnSpd = 0.12f;

    // 空中の方向転換速度
    public float AirTurnSpd = 1;

    // プレイヤーの体力
    public ReactiveProperty<int> Hp = new ReactiveProperty<int>(0);

    // 体力の最大値
    public ReactiveProperty<int> MaxHp = new ReactiveProperty<int>(100);

    // 体力の減速時間
    public float HpDecreaseTime =  2;

    // 体力の減少値
    public int HpDecrease = 1;

    // プレイヤーの攻撃力
    public int Offense = 10;

    // プレイヤーの防御力
    public int Defense = 5;

    // 地上攻撃の攻撃倍率
    public float[] GroundAtkPow = { 0, 1, 1, 1.1f, 1.2f, 1.3f, 1.5f, 2 };

    // 地上攻撃の移動距離
    public int[] GroundMovDis = { 0, 10, 10, 10, 12, 13, 14, 15 };

    // 地上攻撃移動の終了時間
    public float[] GroundMovExitTime = { 0, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };

    // 空中攻撃の攻撃倍率
    public float[] AirAtkPow = { 0, 1, 1, 1, 1, 1.2f, 1.5f };

    // 空中攻撃の移動距離
    public int[] AirAtkMovDis = { 0, 10, 10, 10, 10, 10, 15 };

    // 空中攻撃移動の終了時間
    public float[] AirMovExitTime = { 0, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f};

    // ジャンプ力
    public int JumpPow = 200;

    // 回避距離
    public int DodgeDis = 300;

    // シフトの体力減少値
    public int ShiftHpDecrease = 10;

    // シフトの移動距離
    public int ShiftMovDis = 500;

    //移動開始時間
    public float ShiftMovStartTime = 0.1f;

    // シフトの移動時間
    public float ShiftMovTime = 0.1f;

    // 敵から攻撃を受けた時の方向
    public ReactiveProperty<float> HitAngle;
}
