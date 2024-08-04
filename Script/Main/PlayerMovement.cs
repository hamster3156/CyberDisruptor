
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerGroundCheck groundCheck;
    [SerializeField] private PlayerCore playerCore;

    private Vector2 inputMovDir;
    private Vector3 movDir;
    private Quaternion mainCameraQuat;
    private float targetRotation = 0;
    private float rotationVelocity;
    private readonly int MOV_SPD = Animator.StringToHash("MovSpd");
    private readonly int IS_MOV = Animator.StringToHash("isMov");
    private bool isAnimeMov;
    private bool isActRot;
    private float inputAngle;

    public int lastInputAngle;

    [Header("移動を許可")]
    public bool IsMov;

    [Header("現在の移動速度")]
    public float CurrentMovSpd = 0;

    [Header("現在の回転速度")]
    public float CurrentTurnSpd = 0;

    // 走りモーション速度の終了速度
    private float runExitTime = 0.35f;

    void Update()
    {
        if (playerCore.Hp.Value <= 0 && IsMov == true)
        {
            IsMov = false;
            ActRotInit();
        }

        InputMovement();
        AnimatorParameterMatch();
        LastInputAngle();
    }

    private void FixedUpdate()
    {
        InputRotation();
    }

    // 入力移動処理
    private void InputMovement()
    {
        // カメラ正面を移動入力に掛ける
        mainCameraQuat = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        movDir = new Vector3(inputMovDir.x, 0, inputMovDir.y).normalized;
        var cameraDir = mainCameraQuat * movDir;

        // 入力された方向から角度を取得
        inputAngle = (float)(Math.Atan2(inputMovDir.x, inputMovDir.y) * Mathf.Rad2Deg);
        inputAngle = Mathf.Round((float)(inputAngle / 45)) * 45;

        // 移動を許可しない時は移動のパラメーターを全て初期化
        if (IsMov == false)
        {
            isAnimeMov = false;
            CurrentMovSpd = 0;
            return;
        }

        // 移動入力が0ではない時
        if (movDir.sqrMagnitude != 0.0f)
        {
            rb.velocity = new Vector3(cameraDir.x * CurrentMovSpd, rb.velocity.y, cameraDir.z * CurrentMovSpd);
            SpdParameter();
        }
        else
        {
            CurrentMovSpd = 0;
            MovVelocityXZInit();
        }
    }

    // 入力回転処理
    private void InputRotation()
    {
        var cameraDir = mainCameraQuat * movDir;

        if (IsMov == false)
        {
            // 瞬時に入力方向に回転
            if (isActRot == true)
            {
                // 入力があれば即回転
                if (movDir.sqrMagnitude != 0.0f)
                {
                    targetRotation = Mathf.Atan2(cameraDir.x, cameraDir.z) * Mathf.Rad2Deg;
                    float inputRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, 0);
                    transform.rotation = Quaternion.Euler(0.0f, inputRotation, 0.0f);
                }
            }
            return;
        }

        // 移動可能時に入力方向に回転
        if (movDir.sqrMagnitude != 0.0f)
        {
            // プレイヤーの回転処理
            targetRotation = Mathf.Atan2(cameraDir.x, cameraDir.z) * Mathf.Rad2Deg;
            float inputRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, CurrentTurnSpd);
            transform.rotation = Quaternion.Euler(0.0f, inputRotation, 0.0f);
        }

        // 通常時はtrueにする
        isActRot = true;
    }

    private void AnimatorParameterMatch()
    {
        // Animatorのパラメーターと同期
        animator.SetFloat(MOV_SPD, CurrentMovSpd, runExitTime, Time.deltaTime);
        animator.SetBool(IS_MOV, isAnimeMov);

        if (movDir.sqrMagnitude != 0.0) isAnimeMov = true;
        else isAnimeMov = false;
    }

    private void SpdParameter()
    {
        // プレイヤーが地面の上に居る時の移動、回転速度
        if (groundCheck.IsGround == true)
        {
            CurrentMovSpd = playerCore.RunSpd;
            CurrentTurnSpd = playerCore.TurnSpd;
        }
        else
        {
            // プレイヤーが空中に居る時は移動、回転速度を遅くする
            if (lastInputAngle != inputAngle) CurrentMovSpd = playerCore.RunSpd / 2;
            else if (lastInputAngle == inputAngle) CurrentMovSpd = playerCore.RunSpd / 1.2f;
            CurrentTurnSpd = playerCore.AirTurnSpd;
        }
    }

    // 最後に入力された角度を取得する
    private void LastInputAngle()
    {
        // 入力角度を合わせる
        if (groundCheck.IsGround == true && movDir.sqrMagnitude != 0.0)
        {
            switch (inputAngle)
            {
                case 0:
                    lastInputAngle = 0;
                    break;

                case 45:
                    lastInputAngle = 45;
                    break;

                case 90:
                    lastInputAngle = 90;
                    break;

                case 135:
                    lastInputAngle = 135;
                    break;

                case 180:
                    lastInputAngle = 180;
                    break;

                case -45:
                    lastInputAngle = -45;
                    break;

                case -90:
                    lastInputAngle = -90;
                    break;

                case -135:
                    lastInputAngle = -135;
                    break;
            }
        }
    }

    // InputSystemのVector2を受け取る
    public void OnMov(InputAction.CallbackContext context)
    {
        inputMovDir = context.ReadValue<Vector2>();
    }

    // プレイヤーが滑らないようにVector3を初期化
    public void MovVelocityXZInit()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    // ジャンプ時のVector3を初期化
    public void MovVelocityYInit()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }


    // アクション開始時だけ素早く回転させる事が出来る
    public void ActRotStart()
    {
        isActRot = true;
        Observable
            .Timer(TimeSpan.FromSeconds(0.01f))
            .Subscribe(_ => isActRot = false)
            .AddTo(this);
    }

    // アクション終了時に回転を元に戻す
    public void ActRotInit()
    {
        isActRot = false;
    }

    // ステートが切り替わった時に、地面状態に合わせて初期化処理を行う
    public void StateChangeInit()
    {
        if (groundCheck.IsGround == true)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isActRot = true;
            Observable
                .Timer(TimeSpan.FromSeconds(0.01f))
                .Subscribe(_ => isActRot = false)
                .AddTo(this);
        }
        else
        {
            rb.velocity = Vector3.zero;
            isActRot = true;
            Observable
                .Timer(TimeSpan.FromSeconds(0.01f))
                .Subscribe(_ => isActRot = false)
                .AddTo(this);
        }
    }

    // rbのY軸を固定してプレイヤーが落下しないようにする
    public void Air()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // rbのY軸を固定を解除してプレイヤーが落下するようにする
    public void DropDown()
    {
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }
}