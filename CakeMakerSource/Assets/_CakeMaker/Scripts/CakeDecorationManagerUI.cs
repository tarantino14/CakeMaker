using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeDecorationManagerUI : MonoBehaviour
{
    // Public //
    public CakeDecorationManager m_decoManager;
    public GameObject m_rotationPanel;
    public GameObject m_colorPanel;
    public FlexibleColorPicker m_colorPicker;
    // Protected //
    // Private //
    // Access //

    void Start()
    {
        
    }

    public void RotationX(float by)
    {
        if (m_decoManager.m_selectedCheez == null)
        {
            return;
        }

        if(m_decoManager.m_selectedCheez.m_canRotate == false)
        {
            return;
        }

        m_decoManager.m_selectedCheez.transform.rotation = Quaternion.AngleAxis(by*360f, Vector3.right);
    }

    public void RotationY(float by)
    {
        if (m_decoManager.m_selectedCheez == null)
        {
            return;
        }

        if(m_decoManager.m_selectedCheez.m_canRotate == false)
        {
            return;
        }

        m_decoManager.m_selectedCheez.transform.rotation = Quaternion.AngleAxis(by*360f, Vector3.up);
    }

    public void ColorChanged(Color changedTo)
    {
        if (m_decoManager.m_selectedCheez == null)
        {
            return;
        }

        if(m_decoManager.m_selectedCheez.m_canChangeColor == false)
        {
            return;
        }

        m_decoManager.m_selectedCheez.m_mainMaterial.color = changedTo;
    }

    void Update()
    {
        if(m_decoManager.m_selectedCheez == null)
        {
            m_rotationPanel.SetActive(false);
            m_colorPanel.SetActive(false);
        }
        else
        {
            if(m_decoManager.m_selectedCheez.m_canRotate)
            {
                m_rotationPanel.SetActive(true);
            }
            else
            {
                m_rotationPanel.SetActive(false);
            }
            
            if(m_decoManager.m_selectedCheez.m_canChangeColor)
            {
                if(m_colorPanel.activeSelf == false)
                {
                    m_colorPicker.SetColor(m_decoManager.m_selectedCheez.m_mainMaterial.color);
                }
                m_colorPanel.SetActive(true);
            }
            else
            {
                m_colorPanel.SetActive(false);
            }
        }
    }
}
