using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float minFade = 0.1f;
    public float maxFade = 1.0f;
    public float alpha = 1.0f;

    Material m_EnemyMaterial;
    float m_InterpolatedAlpha;
    // Start is called before the first frame update
    void Start()
    {
        m_EnemyMaterial = GetComponent<SkinnedMeshRenderer>().material;
        m_InterpolatedAlpha = (1.0f - alpha) * minFade + alpha * maxFade;
        SetAlpha(m_InterpolatedAlpha);
    }
    
    void FixedUpdate()
    {
        m_InterpolatedAlpha = (1.0f - alpha) * minFade + alpha * maxFade;
        SetAlpha(m_InterpolatedAlpha);
    }

    void SetAlpha(float alpha)
    {
        Color color = m_EnemyMaterial.color;
        color.a = Mathf.Clamp(alpha, 0, 1);
        m_EnemyMaterial.color = color;
    }
}
