using UnityEngine;

public class PlayerThrowWeapon : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // ���킪��������A�j���[�V�������Đ�����
    public void ThrowWeaponStart()
    {
        animator.Play("WeaponMove");
    }
}
