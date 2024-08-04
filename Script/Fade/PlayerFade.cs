using UnityEngine;
using DG.Tweening;

public class PlayerFade : MonoBehaviour
{
    public MeshRenderer material;
    public float FadeRate;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            material.materials[0].DOFade(0, FadeRate);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            material.materials[0].DOFade(1, FadeRate);
        }
    }
}
