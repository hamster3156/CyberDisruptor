using UnityEngine;

public class PlayerMaterialChange : MonoBehaviour
{
    // material�̕ύX�t���O
    public bool IsChangeMate = false;

    [Header("�c���̃}�e���A��")]
    [SerializeField] private Material afterImageMate;

    [Header("�}�e���A����ύX����SkinnedMeshRenderer")]
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header("�c���𐶐�����I�u�W�F�N�g")]
    [SerializeField] private GameObject WeaponObj;
    [SerializeField] private GameObject AfterImageObj;

    [Header("�c���̏��ő��x")]
    public float rate = 30;


    private void Start()
    {
        // SkinnedMeshRenderer���ݒ肳��Ă���ꍇ�S�Ẵ}�e���A����ύX����
        if (skinnedMeshRenderers != null && skinnedMeshRenderers.Length > 0 && IsChangeMate == true)
        {
            WeaponObj.SetActive(false);
            AfterImageObj.SetActive(false);

            Material[] newMaterials = new Material[skinnedMeshRenderers[0].sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = afterImageMate;
            }

            // ���ׂĂ� SkinnedMeshRenderer �ɐV�����}�e���A���̔z���ݒ�
            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                skinnedMeshRenderer.materials = newMaterials;
            }
        }
    }

    private void Update()
    {
        if (IsChangeMate == false) return;

        // �c���̓����x��ύX
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
