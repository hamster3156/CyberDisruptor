using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PlayerFadeMainObject : MonoBehaviour
{
    // メイン武器のメッシュを取得
    [SerializeField]
    private MeshRenderer WeaponMesh;

    // メイン武器の残像のメッシュを取得
    [SerializeField]
    private MeshRenderer WeaponAfterImageMesh;

    // プレイヤーのメッシュを取得
    [SerializeField]
    private SkinnedMeshRenderer[] playerMesh;

    // フェードの速度
    [SerializeField] 
    private float fadeRate = 10;

    // 武器の表示処理
    private void FadeOutWeapon()
    {
        WeaponMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                WeaponRenderModeOpaque();
            });
    }

    // 武器の非表示処理
    private void FadeInWeapon()
    {
        WeaponRenderModeFade();
        WeaponMesh.materials[0].DOFade(0, fadeRate);
    }

    // 武器の残像の表示処理
    private void FadeOutWeaponAfterImage()
    {
        WeaponAfterImageMesh.materials[0].DOFade(1, fadeRate)
            .OnComplete(() =>
            {
                WeaponAfterImageRenderModeOpaque();
            });
    }

    // 武器の残像の非表示処理
    private void FadeInWeaponAfterImage()
    {
        WeaponAfterImageRenderModeFade();
        WeaponAfterImageMesh.materials[0].DOFade(0, fadeRate);
    }

    // 武器を瞬時に非表示にする
    private void FadeActiveWeapon()
    {
        WeaponMesh.materials[0].DOFade(1, 0);
    }

    // 武器を瞬時に表示する
    private void FadeInActiveWeapon()
    {
        WeaponMesh.materials[0].DOFade(0, 0);
    }

    // プレイヤーの表示処理
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

    // プレイヤーの非表示処理
    private void FadeInPlayer()
    {
        PlayerRenderModeFade();
        foreach (SkinnedMeshRenderer skinMesh in playerMesh)
        {
            skinMesh.materials[0].DOFade(0, fadeRate);
        }
    }

    // 武器のRenderModeを変更してフェード出来るようにする
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

    // 武器の残像のRenderModeを変更して半透明にならないようにする
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

    // 武器の残像のRenderModeを変更してフェード出来るようにする
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

    // 武器の残像のRenderModeを変更して半透明にならないようにする
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

    // プレイヤーのRenderModeを変更してフェード出来るようにする
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

    // プレイヤーのRenderModeを変更して半透明にならないようにする
    private void PlayerRenderModeCutOut()
    {
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in playerMesh)
        {
            // MaterialのRenderModeをCutOutに変更する
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

    // AnimatorEventに登録して武器の表示、非表示を行う
    // 武器の表示処理を行う
    public async void FadeOutWeaponsStart()
    {
        FadeOutWeaponAfterImage();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        FadeOutWeapon();
        FadeInWeaponAfterImage();
    }

    // 武器の非表示処理を行う
    public async void FadeInWeaponsStart()
    {
        FadeInWeapon();
        FadeOutWeaponAfterImage();

        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        FadeInWeaponAfterImage();
    }

    // 武器の非表示処理を行う
    public void FadeInWeaponsInit()
    {
        FadeInWeapon();
        FadeInWeaponAfterImage();
    }

    // 武器を即時に表示にする
    public void FadeActiveWeaponStart()
    {
        FadeActiveWeapon();
    }

    // 武器を即時に非表示にする
    public void FadeInActiveWeaponStart()
    {
        FadeInActiveWeapon();
    }

    // プレイヤーの表示
    public void FadeOutPlayerStart()
    {
        FadeOutPlayer();
    }

    // プレイヤーの非表示
    public void FadeInPlayerStart()
    {
        FadeInPlayer();
    }
}
