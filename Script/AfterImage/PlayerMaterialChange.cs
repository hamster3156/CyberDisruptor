using UnityEngine;

public class PlayerMaterialChange : MonoBehaviour
{
    // materialの変更フラグ
    public bool IsChangeMate = false;

    [Header("残像のマテリアル")]
    [SerializeField] private Material afterImageMate;

    [Header("マテリアルを変更するSkinnedMeshRenderer")]
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header("残像を生成するオブジェクト")]
    [SerializeField] private GameObject WeaponObj;
    [SerializeField] private GameObject AfterImageObj;

    [Header("残像の消滅速度")]
    public float rate = 30;


    private void Start()
    {
        // SkinnedMeshRendererが設定されている場合全てのマテリアルを変更する
        if (skinnedMeshRenderers != null && skinnedMeshRenderers.Length > 0 && IsChangeMate == true)
        {
            WeaponObj.SetActive(false);
            AfterImageObj.SetActive(false);

            Material[] newMaterials = new Material[skinnedMeshRenderers[0].sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = afterImageMate;
            }

            // すべての SkinnedMeshRenderer に新しいマテリアルの配列を設定
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                skinnedMeshRenderer.materials = newMaterials;
            }
        }
    }

    private void Update()
    {
        if (IsChangeMate == false) return;

        // 残像の透明度を変更
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            skinnedMeshRenderer.materials[0].color -= new Color(0, 0, 0, rate * 0.001f);

            if (skinnedMeshRenderer.materials[0].color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
