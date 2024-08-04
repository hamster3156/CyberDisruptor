using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PlayerFadeMainObject : MonoBehaviour
{
    // ���C������̃��b�V�����擾
    [SerializeField]
    private MeshRenderer WeaponMesh;

    // ���C������̎c���̃��b�V�����擾
    [SerializeField]
    private MeshRenderer WeaponAfterImageMesh;

    // �v���C���[�̃��b�V�����擾
    [SerializeField]
    private SkinnedMeshRenderer[] playerMesh;

    // �t�F�[�h�̑��x
    [SerializeField] 
    private float fadeRate = 10;

    // ����̕\������
    private void FadeOutWeapon()
    {
        WeaponMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                WeaponRenderModeOpaque();
            });
    }

    // ����̔�\������
    private void FadeInWeapon()
    {
        WeaponRenderModeFade();
        WeaponMesh.materials[0].DOFade(0, fadeRate);
    }

    // ����̎c���̕\������
    private void FadeOutWeaponAfterImage()
    {
        WeaponAfterImageMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                WeaponAfterImageRenderModeOpaque();
            });
    }

    // ����̎c���̔�\������
    private void FadeInWeaponAfterImage()
    {
        WeaponAfterImageRenderModeFade();
        WeaponAfterImageMesh.materials[0].DOFade(0, fadeRate);
    }

    // ������u���ɔ�\���ɂ���
    private void FadeActiveWeapon()
    {
        WeaponMesh.materials[0].DOFade(1, 0);
    }

    // ������u���ɕ\������
    private void FadeInActiveWeapon()
    {
        WeaponMesh.materials[0].DOFade(0, 0);
    }

    // �v���C���[�̕\������
    private void FadeOutPlayer()
    {
        foreach (SkinnedMeshRenderer skinMesh in playerMesh)
        {
            skinMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                PlayerRenderModeCutOut();
            });
        }
    }

    // �v���C���[�̔�\������
    private void FadeInPlayer()
    {
        PlayerRenderModeFade();
        foreach (SkinnedMeshRenderer skinMesh in playerMesh)
        {
            skinMesh.materials[0].DOFade(0, fadeRate);
        }
    }

    // �����RenderMode��ύX���ăt�F�[�h�o����悤�ɂ���
    private void WeaponRenderModeFade()
    {
        WeaponMesh.material.SetOverrideTag("RenderType", "Transparent");
        WeaponMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        WeaponMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        WeaponMesh.material.SetInt("_ZWrite", 0);
        WeaponMesh.material.DisableKeyword("_ALPHATEST_ON");
        WeaponMesh.material.EnableKeyword("_ALPHABLEND_ON");
        WeaponMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        WeaponMesh.material.renderQueue = 3000;
    }

    // ����̎c����RenderMode��ύX���Ĕ������ɂȂ�Ȃ��悤�ɂ���
    private void WeaponRenderModeOpaque()
    {
        WeaponMesh.material.SetOverrideTag("RenderType", "");
        WeaponMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        WeaponMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        WeaponMesh.material.SetInt("_ZWrite", 1);
        WeaponMesh.material.DisableKeyword("_ALPHATEST_ON");
        WeaponMesh.material.DisableKeyword("_ALPHABLEND_ON");
        WeaponMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        WeaponMesh.material.renderQueue = -1;
    }

    // ����̎c����RenderMode��ύX���ăt�F�[�h�o����悤�ɂ���
    private void WeaponAfterImageRenderModeFade()
    {
        WeaponAfterImageMesh.material.SetOverrideTag("RenderType", "Transparent");
        WeaponAfterImageMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        WeaponAfterImageMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        WeaponAfterImageMesh.material.SetInt("_ZWrite", 0);
        WeaponAfterImageMesh.material.DisableKeyword("_ALPHATEST_ON");
        WeaponAfterImageMesh.material.EnableKeyword("_ALPHABLEND_ON");
        WeaponAfterImageMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        WeaponAfterImageMesh.material.renderQueue = 3000;
    }

    // ����̎c����RenderMode��ύX���Ĕ������ɂȂ�Ȃ��悤�ɂ���
    private void WeaponAfterImageRenderModeOpaque()
    {
        WeaponAfterImageMesh.material.SetOverrideTag("RenderType", "");
        WeaponAfterImageMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        WeaponAfterImageMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        WeaponAfterImageMesh.material.SetInt("_ZWrite", 1);
        WeaponAfterImageMesh.material.DisableKeyword("_ALPHATEST_ON");
        WeaponAfterImageMesh.material.DisableKeyword("_ALPHABLEND_ON");
        WeaponAfterImageMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        WeaponAfterImageMesh.material.renderQueue = -1;
    }

    // �v���C���[��RenderMode��ύX���ăt�F�[�h�o����悤�ɂ���
    private void PlayerRenderModeFade()
    {
        foreach (SkinnedMeshRenderer skinMesh in playerMesh)
        {
            skinMesh.material.SetOverrideTag("RenderType", "Transparent");
            skinMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            skinMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            skinMesh.material.SetInt("_ZWrite", 0);
            skinMesh.material.DisableKeyword("_ALPHATEST_ON");
            skinMesh.material.EnableKeyword("_ALPHABLEND_ON");
            skinMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            skinMesh.material.renderQueue = 3000;
        }
    }

    // �v���C���[��RenderMode��ύX���Ĕ������ɂȂ�Ȃ��悤�ɂ���
    private void PlayerRenderModeCutOut()
    {
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in playerMesh)
        {
            // Material��RenderMode��CutOut�ɕύX����
            skinnedMeshRenderer.material.SetOverrideTag("RenderType", "TransparentCutout");
            skinnedMeshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            skinnedMeshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            skinnedMeshRenderer.material.SetInt("_ZWrite", 1);
            skinnedMeshRenderer.material.EnableKeyword("_ALPHATEST_ON");
            skinnedMeshRenderer.material.DisableKeyword("_ALPHABLEND_ON");
            skinnedMeshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            skinnedMeshRenderer.material.renderQueue = 2450;
        }
    }

    // AnimatorEvent�ɓo�^���ĕ���̕\���A��\�����s��
    // ����̕\���������s��
    public async void FadeOutWeaponsStart()
    {
        FadeOutWeaponAfterImage();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        FadeOutWeapon();
        FadeInWeaponAfterImage();
    }

    // ����̔�\���������s��
    public async void FadeInWeaponsStart()
    {
        FadeInWeapon();
        FadeOutWeaponAfterImage();

        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        FadeInWeaponAfterImage();
    }

    // ����̔�\���������s��
    public void FadeInWeaponsInit()
    {
        FadeInWeapon();
        FadeInWeaponAfterImage();
    }

    // ����𑦎��ɕ\���ɂ���
    public void FadeActiveWeaponStart()
    {
        FadeActiveWeapon();
    }

    // ����𑦎��ɔ�\���ɂ���
    public void FadeInActiveWeaponStart()
    {
        FadeInActiveWeapon();
    }

    // �v���C���[�̕\��
    public void FadeOutPlayerStart()
    {
        FadeOutPlayer();
    }

    // �v���C���[�̔�\��
    public void FadeInPlayerStart()
    {
        FadeInPlayer();
    }
}
