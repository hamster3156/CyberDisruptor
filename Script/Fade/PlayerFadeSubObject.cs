using UnityEngine;
using DG.Tweening;

public class PlayerFadeSubObject : MonoBehaviour
{
    // �V�t�g�̓����镐��̃��b�V�����擾
    [SerializeField]
    private MeshRenderer ShiftWeaponMesh;

    // �V�t�g�̓����镐��̎c���̃��b�V�����擾
    [SerializeField]
    private MeshRenderer ShiftWeaponAfterImageMesh;

    // �t�F�[�h�̑��������l
    [SerializeField] 
    private float fadeRate = 10;

    // �V�t�g�̓����镐��̕\������
    public void FadeOutShiftWeapon()
    {
        ShiftWeaponMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                ShiftWeaponRenderModeOpaque();
            });
    }

    // �V�t�g�̓����镐��̔�\������
    public void FadeInShiftWeapon()
    {
        ShiftWeaponRenderModeFade();
        ShiftWeaponMesh.materials[0].DOFade(0, fadeRate);
    }

    // �V�t�g�̓����镐��̎c���̕\������
    public void FadeOutShiftWeaponAfterImage()
    {
        ShiftWeaponAfterImageMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                ShiftWeaponAfterImageRenderModeOpaque();
            });
    }

    // �V�t�g�̓����镐��̎c���̔�\������
    public void FadeInShiftWeaponAfterImage()
    {
        ShiftWeaponAfterImageRenderModeFade();
        ShiftWeaponAfterImageMesh.materials[0].DOFade(0, fadeRate);
    }

    // �V�t�g�̕���𓧖��ɏo����悤�ɂ���
    private void ShiftWeaponRenderModeFade()
    {
        ShiftWeaponMesh.material.SetOverrideTag("RenderType", "Transparent");
        ShiftWeaponMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        ShiftWeaponMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        ShiftWeaponMesh.material.SetInt("_ZWrite", 0);
        ShiftWeaponMesh.material.DisableKeyword("_ALPHATEST_ON");
        ShiftWeaponMesh.material.EnableKeyword("_ALPHABLEND_ON");
        ShiftWeaponMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ShiftWeaponMesh.material.renderQueue = 3000;
    }

    // �V�t�g�̕���𔼓����ɂ��Ȃ��悤�ɂ���
    private void ShiftWeaponRenderModeOpaque()
    {
        ShiftWeaponMesh.material.SetOverrideTag("RenderType", "");
        ShiftWeaponMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        ShiftWeaponMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        ShiftWeaponMesh.material.SetInt("_ZWrite", 1);
        ShiftWeaponMesh.material.DisableKeyword("_ALPHATEST_ON");
        ShiftWeaponMesh.material.DisableKeyword("_ALPHABLEND_ON");
        ShiftWeaponMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ShiftWeaponMesh.material.renderQueue = -1;
    }

    // �V�t�g�̕���̎c���𓧖��ɏo����悤�ɂ���
    private void ShiftWeaponAfterImageRenderModeFade()
    {
        ShiftWeaponAfterImageMesh.material.SetOverrideTag("RenderType", "Transparent");
        ShiftWeaponAfterImageMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        ShiftWeaponAfterImageMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        ShiftWeaponAfterImageMesh.material.SetInt("_ZWrite", 0);
        ShiftWeaponAfterImageMesh.material.DisableKeyword("_ALPHATEST_ON");
        ShiftWeaponAfterImageMesh.material.EnableKeyword("_ALPHABLEND_ON");
        ShiftWeaponAfterImageMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ShiftWeaponAfterImageMesh.material.renderQueue = 3000;
    }

    // �V�t�g�̕���̎c���𔼓����ɂ��Ȃ��悤�ɂ���
    private void ShiftWeaponAfterImageRenderModeOpaque()
    {
        ShiftWeaponAfterImageMesh.material.SetOverrideTag("RenderType", "");
        ShiftWeaponAfterImageMesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        ShiftWeaponAfterImageMesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        ShiftWeaponAfterImageMesh.material.SetInt("_ZWrite", 1);
        ShiftWeaponAfterImageMesh.material.DisableKeyword("_ALPHATEST_ON");
        ShiftWeaponAfterImageMesh.material.DisableKeyword("_ALPHABLEND_ON");
        ShiftWeaponAfterImageMesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ShiftWeaponAfterImageMesh.material.renderQueue = -1;
    }

    // �V�t�g������u���ɓ����ɂ���
    public void FadeActiveShiftWeapon()
    {
        ShiftWeaponMesh.materials[0].DOFade(1, 0)
            .OnComplete(() =>
            {
                ShiftWeaponRenderModeOpaque();
            });
    }
}
