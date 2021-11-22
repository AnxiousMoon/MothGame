using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Collider collider;
    public float initialTimeOut = 2f;
    private float timeOut;

    Material[] childMaterials;
    [Header("Gravel Effects")]
    [SerializeField][ColorUsage(true,true)] Color activeColor = Color.white;

    [Header("Gravel Particle Effect")]
    [SerializeField] [ColorUsage(true, true)] Color idleColor = Color.white;
    [SerializeField] [ColorUsage(true, true)] Color particleActiveColor = Color.white;

    [SerializeField] GameObject gravelParticleSystemPrefab;
    ParticleSystem gravelParticleSystem;
    ParticleSystem.MainModule gravelParticleSystemMain;

    
    [SerializeField] GameObject gravelSoundRadiusPrefab;
    SoundPadRadius gravelSoundRadius;

    [Header("Sound")]
    bool soundPadActive = false;

    public AK.Wwise.Event Sound;

    private void Awake()
    {

        
    }

    // Start is called before the first frame update
    void Start()
    {
        GetGravelMaterials();
        SetUpParticleSystem();

        gravelSoundRadiusPrefab = Instantiate(gravelSoundRadiusPrefab);
        gravelSoundRadius = gravelSoundRadiusPrefab.GetComponent<SoundPadRadius>();

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Player on soundpad");
            if (!col.isTrigger)
            {
                collider.enabled = true;
                if (!soundPadActive)
                {
                    Sound.Post(gameObject);
                    gravelSoundRadius.Activate();
                    GravelGlow();
                }
            }

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            collider.enabled = false;
            
        }
        if (col.tag == "Player")
        {
            if (!soundPadActive)
            {
                gravelSoundRadius.Deactivate();
                EndGravelGlow();
            }
            

        }
    }

    void GetGravelMaterials()
    {
        childMaterials = new Material[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childMaterials[i] = transform.GetChild(i).GetComponent<MeshRenderer>().material;
            childMaterials[i].EnableKeyword("_EMISSION");
        }
    }

    void SetUpParticleSystem()
    {
        gravelParticleSystemPrefab = Instantiate(gravelParticleSystemPrefab, transform);
        gravelParticleSystemPrefab.transform.localPosition = new Vector3(-4, 0, 4);
        gravelParticleSystem = gravelParticleSystemPrefab.GetComponent<ParticleSystem>();
        gravelParticleSystemMain = gravelParticleSystem.main;
        gravelParticleSystemMain.startColor = Color.black;
    }
    void GravelGlow()
    {
        for (int i = 0; i < childMaterials.Length; i++)
        {
            childMaterials[i].SetColor("_EmissionColor", activeColor);
        }
        gravelParticleSystemMain.startColor = particleActiveColor;
    }

    void EndGravelGlow()
    {
        for (int i = 0; i < childMaterials.Length; i++)
        {
            childMaterials[i].SetColor("_EmissionColor", Color.black);
        }
        gravelParticleSystemMain.startColor = idleColor;
    }

    // Update is called once per frame
    void Update()
    {
        timeOut -= Time.deltaTime;
        if (timeOut <= 0f)
        {
            timeOut = initialTimeOut;
            collider.enabled = false;
        }
    }
}
