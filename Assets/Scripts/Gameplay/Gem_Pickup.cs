using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Pickup : MonoBehaviour
{
    private Collider g_collider;

    public GameObject Gem;

    public Gem_manager GemManager;


    Material material;
    float initialAlpha;
    [SerializeField] float pickUpFadeOutTime = 1f;
    private void Awake()
    {
        g_collider = gameObject.GetComponent<Collider>();
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        initialAlpha = material.GetFloat("_Alpha");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Dashing")
        {
            if(!col.isTrigger)
            {
                g_collider.enabled = false;
                GemManager.Count += 1;
                
                PickUpAnimation();
            }
        }
    }

    void PickUpAnimation()
    {
        material.SetFloat("_FresnelStrength", 0f);
        LeanTween.value(gameObject, initialAlpha, 0f, pickUpFadeOutTime).setOnUpdate((float _alpha) =>
        {
            material.SetFloat("_Alpha", _alpha);
        }).setOnComplete(Deactivation);
    }

    void Deactivation()
    {
        Destroy(gameObject);
    }
}
