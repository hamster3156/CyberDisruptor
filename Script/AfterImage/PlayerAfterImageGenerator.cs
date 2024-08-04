using UnityEngine;

public class PlayerAfterImageGenerator : MonoBehaviour
{
    [Header("��������c��")]
    [SerializeField]
    GameObject PlayerModelObj;

    // ��������c���̃I�u�W�F�N�g
    private GameObject instantiateObj;
    private PlayerMaterialChange materialChange;

    [Header("�����t���O")]
    public bool isInst = false;

    [Header("�����X�s�[�h")]
    public float instSpd;
    private float instTimer;

    void Start()
    {
        instantiateObj = null;
        materialChange = null;
        instTimer = instSpd;
    }

    private void FixedUpdate()
    {
        AfterImageInst();
    }

    // �c���𐶐�����
    private void AfterImageInst()
    {
        if (isInst == false) return;

        instTimer -= Time.deltaTime;

        if(instTimer < 0)
        {
            instantiateObj = Instantiate(PlayerModelObj, transform.position, transform.rotation);
            materialChange = instantiateObj.GetComponent<PlayerMaterialChange>();
            materialChange.IsChangeMate = true;
            instTimer = instSpd;
        }
    }

    // �c�������J�n�t���O
    public void InstantiateStart()
    {
        isInst = true;
    }

    // �c�������I���t���O
    public void InstantiateEnd()
    {
        isInst = false;
    }

    // �u�ԓI�Ɏc���𐶐�����
    public void InstantiateAfterImage()
    {
        instantiateObj = Instantiate(PlayerModelObj, transform.position, transform.rotation);
        materialChange = instantiateObj.GetComponent<PlayerMaterialChange>();
        materialChange.IsChangeMate = true;
    }
}
