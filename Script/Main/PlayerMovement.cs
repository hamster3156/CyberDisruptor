
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

    [Header("�ړ�������")]
    public bool IsMov;

    [Header("���݂̈ړ����x")]
    public float CurrentMovSpd = 0;

    [Header("���݂̉�]���x")]
    public float CurrentTurnSpd = 0;

    // ���胂�[�V�������x�̏I�����x
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

    // ���͈ړ�����
    private void InputMovement()
    {
        // �J�������ʂ��ړ����͂Ɋ|����
        mainCameraQuat = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        movDir = new Vector3(inputMovDir.x, 0, inputMovDir.y).normalized;
        var cameraDir = mainCameraQuat * movDir;

        // ���͂��ꂽ��������p�x���擾
        inputAngle = (float)(Math.Atan2(inputMovDir.x, inputMovDir.y) * Mathf.Rad2Deg);
        inputAngle = Mathf.Round((float)(inputAngle / 45)) * 45;

        // �ړ��������Ȃ����͈ړ��̃p�����[�^�[��S�ď�����
        if (IsMov == false)
        {
            isAnimeMov = false;
            CurrentMovSpd = 0;
            return;
        }

        // �ړ����͂�0�ł͂Ȃ���
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

    // ���͉�]����
    private void InputRotation()
    {
        var cameraDir = mainCameraQuat * movDir;

        if (IsMov == false)
        {
            // �u���ɓ��͕����ɉ�]
            if (isActRot == true)
            {
                // ���͂�����Α���]
                if (movDir.sqrMagnitude != 0.0f)
                {
                    targetRotation = Mathf.Atan2(cameraDir.x, cameraDir.z) * Mathf.Rad2Deg;
                    float inputRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, 0);
                    transform.rotation = Quaternion.Euler(0.0f, inputRotation, 0.0f);
                }
            }
            return;
        }

        // �ړ��\���ɓ��͕����ɉ�]
        if (movDir.sqrMagnitude != 0.0f)
        {
            // �v���C���[�̉�]����
            targetRotation = Mathf.Atan2(cameraDir.x, cameraDir.z) * Mathf.Rad2Deg;
            float inputRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, CurrentTurnSpd);
            transform.rotation = Quaternion.Euler(0.0f, inputRotation, 0.0f);
        }

        // �ʏ펞��true�ɂ���
        isActRot = true;
    }

    private void AnimatorParameterMatch()
    {
        // Animator�̃p�����[�^�[�Ɠ���
        animator.SetFloat(MOV_SPD, CurrentMovSpd, runExitTime, Time.deltaTime);
        animator.SetBool(IS_MOV, isAnimeMov);

        if (movDir.sqrMagnitude != 0.0) isAnimeMov = true;
        else isAnimeMov = false;
    }

    private void SpdParameter()
    {
        // �v���C���[���n�ʂ̏�ɋ��鎞�̈ړ��A��]���x
        if (groundCheck.IsGround == true)
        {
            CurrentMovSpd = playerCore.RunSpd;
            CurrentTurnSpd = playerCore.TurnSpd;
        }
        else
        {
            // �v���C���[���󒆂ɋ��鎞�͈ړ��A��]���x��x������
            if (lastInputAngle != inputAngle) CurrentMovSpd = playerCore.RunSpd / 2;
            else if (lastInputAngle == inputAngle) CurrentMovSpd = playerCore.RunSpd / 1.2f;
            CurrentTurnSpd = playerCore.AirTurnSpd;
        }
    }

    // �Ō�ɓ��͂��ꂽ�p�x���擾����
    private void LastInputAngle()
    {
        // ���͊p�x�����킹��
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

    // InputSystem��Vector2���󂯎��
    public void OnMov(InputAction.CallbackContext context)
    {
        inputMovDir = context.ReadValue<Vector2>();
    }

    // �v���C���[������Ȃ��悤��Vector3��������
    public void MovVelocityXZInit()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    // �W�����v����Vector3��������
    public void MovVelocityYInit()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }


    // �A�N�V�����J�n�������f������]�����鎖���o����
    public void ActRotStart()
    {
        isActRot = true;
        Observable
            .Timer(TimeSpan.FromSeconds(0.01f))
            .Subscribe(_ => isActRot = false)
            .AddTo(this);
    }

    // �A�N�V�����I�����ɉ�]�����ɖ߂�
    public void ActRotInit()
    {
        isActRot = false;
    }

    // �X�e�[�g���؂�ւ�������ɁA�n�ʏ�Ԃɍ��킹�ď������������s��
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

    // rb��Y�����Œ肵�ăv���C���[���������Ȃ��悤�ɂ���
    public void Air()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // rb��Y�����Œ���������ăv���C���[����������悤�ɂ���
    public void DropDown()
    {
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }
}