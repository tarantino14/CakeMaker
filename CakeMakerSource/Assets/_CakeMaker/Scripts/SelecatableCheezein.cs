using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecatableCheezein : MonoBehaviour
{
    // Public //
    [Header("Selectable Variables")]
    public Type m_type;
    public GameObject m_selectionMesh;
    public bool m_canChangeColor;
    public bool m_canRotate;
    public MeshRenderer m_mainMesh;
    public Material m_mainMaterial;
    public int m_materialIndex = 0;
    // Protected //
    // Private //
    // Access //
    public enum Type
    {
        Cake,
        Cherry,
        Cream,
        Flower,
        Candle,
        Sprinkles,
        Flower01,
        Flower02,
        Flower03
    }

    public virtual void Start()
    {
        m_mainMaterial = m_mainMesh.materials[m_materialIndex];
    }

    public void Select(bool set)
    {
        m_selectionMesh.SetActive(set);
    }
}
