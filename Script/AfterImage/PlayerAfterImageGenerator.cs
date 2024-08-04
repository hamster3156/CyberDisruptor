using UnityEngine;

public class PlayerAfterImageGenerator : MonoBehaviour
{
    [Header("生成する残像")]
    [SerializeField]
    GameObject PlayerModelObj;

    // 生成する残像のオブジェクト
    private GameObject instantiateObj;
    private PlayerMaterialChange materialChange;

    [Header("生成フラグ")]
    public bool isInst = false;

    [Header("生成スピード")]
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

    // 残像を生成する
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

    // 残像生成開始フラグ
    public void InstantiateStart()
    {
        isInst = true;
    }

    // 残像生成終了フラグ
    public void InstantiateEnd()
    {
        isInst = false;
    }

    // 瞬間的に残像を生成する
    public void InstantiateAfterImage()
    {
        instantiateObj = Instantiate(PlayerModelObj, transform.position, transform.rotation);
        materialChange = instantiateObj.GetComponent<PlayerMaterialChange>();
        materialChange.IsChangeMate = true;
    }
}
