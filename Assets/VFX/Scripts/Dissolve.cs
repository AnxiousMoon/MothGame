using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    MeshRenderer meshRenderer;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Material material;
    Material[] materials = new Material[1];
    float dissolveAmount = 0f;

    void Awake()
    {
        if (gameObject.GetComponent<MeshRenderer>())
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer.materials.Length == 1)
            {
                material = meshRenderer.material;
                if (material.shader != Shader.Find("Custom/Dissolve"))
                {
                    Debug.LogError("Dissolve.cs needs the Dissolve shader attached the to the same game object to run properly");
                }
            }
            else
            {
                materials = meshRenderer.materials;
            }
        }

        else if (gameObject.GetComponent<SkinnedMeshRenderer>())
        {
            skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer.materials.Length == 1)
            {
                material = skinnedMeshRenderer.material;
                if (material.shader != Shader.Find("Custom/Dissolve"))
                {
                    Debug.LogError("Dissolve.cs needs the Dissolve shader attached the to the same game object to run properly");
                }
            }
            else
            {
                materials = skinnedMeshRenderer.materials;
            }
        }

        else
        {
            Debug.LogError("Dissolve.cs needs a Mesh Renderer or a Skinned meshed renderer attached to the same game object");
        }
    }

    public void DissolveMe(float _duration = 10f)
    {
        if (materials.Length == 1)
        {
            LeanTween.value(gameObject, 0f, 1f, _duration).setOnUpdate((float dissolveAmount) =>
            {
                material.SetFloat("_Dissolve", dissolveAmount);
            });
        }
        else
        {
            LeanTween.value(gameObject, 0f, 1f, _duration).setOnUpdate((float dissolveAmount) =>
            {
                for(int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetFloat("_Dissolve", dissolveAmount);
                }
            });
        }
    }

    void Start()
    {
        DissolveMe(3f);
    }
}
