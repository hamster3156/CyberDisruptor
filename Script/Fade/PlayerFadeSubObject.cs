using UnityEngine;
using DG.Tweening;

public class PlayerFadeSubObject : MonoBehaviour
{
    // シフトの投げる武器のメッシュを取得
    [SerializeField]
    private MeshRenderer ShiftWeaponMesh;

    // シフトの投げる武器の残像のメッシュを取得
    [SerializeField]
    private MeshRenderer ShiftWeaponAfterImageMesh;

    // フェードの足し引き値
    [SerializeField] 
    private float fadeRate = 10;

    // シフトの投げる武器の表示処理
    public void FadeOutShiftWeapon()
    {
        ShiftWeaponMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                ShiftWeaponRenderModeOpaque();
            });
    }

    // シフトの投げる武器の非表示処理
    public void FadeInShiftWeapon()
    {
        ShiftWeaponRenderModeFade();
        ShiftWeaponMesh.materials[0].DOFade(0, fadeRate);
    }

    // シフトの投げる武器の残像の表示処理
    public void FadeOutShiftWeaponAfterImage()
    {
        ShiftWeaponAfterImageMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                ShiftWeaponAfterImageRenderModeOpaque();
            });
    }

    // シフトの投げる武器の残像の非表示処理
    public void FadeInShiftWeaponAfterImage()
    {
        ShiftWeaponAfterImageRenderModeFade();
        ShiftWeaponAfterImageMesh.materials[0].DOFade(0, fadeRate);
    }

    // シフトの武器を透明に出来るようにする
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

    // シフトの武器を半透明にしないようにする
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

    // シフトの武器の残像を透明に出来るようにする
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

    // シフトの武器の残像を半透明にしないようにする
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

    // シフト武器を瞬時に透明にする
    public void FadeActiveShiftWeapon()
    {
        ShiftWeaponMesh.materials[0].DOFade(1, 0)
            .OnComplete(() =>
            {
                ShiftWeaponRenderModeOpaque();
            });
    }
}
