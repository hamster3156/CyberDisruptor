using UnityEngine;

public class PlayerThrowWeapon : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 武器が投げられるアニメーションを再生する
    public void ThrowWeaponStart()
    {
        animator.Play("WeaponMove");
    }
}
