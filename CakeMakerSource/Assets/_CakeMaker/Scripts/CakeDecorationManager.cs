using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DigitalRubyShared;

public class CakeDecorationManager : MonoBehaviour
{
    // Public //
    public LayerMask m_chipakneWaleLayer;
    public LayerMask m_dragLayers;
    public float m_spherecastRadius = 0.1f;
    public Camera m_mainCamera;
    public FingersPanOrbitComponentScript m_orbit;
    public ChipkneWaleCheezein m_chipkuCheezUnderMouse;
    public SelecatableCheezein m_selectedCheez;
    public float m_initialDisWhenChipkuCheezCameUnderMouse;
    public CakeDecorationFileManager m_fileManager;
    public Light m_cakeLight;
    // Protected //
    // Private //
    // Access //

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnTakeMeToAR()
    {

        m_cakeLight.intensity = m_cakeLight.intensity * 0.1f;
        for(int i=0; i<m_fileManager.m_thingsToSave.Count; i++)
        {
            m_fileManager.m_thingsToSave[i].transform.parent = transform;
        }

        gameObject.SetActive(false);
        SceneManager.LoadScene("ARScene");
    }
    public static bool IsPointerOverUIObject()
    {
        bool touchDown = Input.GetMouseButtonDown(0);
        Vector3 touchPos = Input.mousePosition;
        bool touchUp = Input.GetMouseButtonUp(0);

        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                touchDown = true;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                touchUp = true;
            }
            touchPos = touch.position;
        }
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touchPos.x, touchPos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        string log = "";
        for (int index = 0; index < results.Count; index++)
        {
            RaycastResult curRaysastResult = results[index];
            log += curRaysastResult.gameObject.layer;
            if (curRaysastResult.gameObject.layer == 5)
            {
                return true;
            }
        }
        //Debug.Log(log + results.Count);

        return false;
    }

    void Update()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }

        bool touchDown = Input.GetMouseButtonDown(0);
        Vector3 touchPos = Input.mousePosition;
        bool touchUp = Input.GetMouseButtonUp(0);

        if(Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == UnityEngine.TouchPhase.Began)
            {
                touchDown = true;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                touchUp = true;
            }
            touchPos = touch.position;
        }
        if (touchDown)
        {
            RaycastHit hit;
            Ray ray = m_mainCamera.ScreenPointToRay(touchPos);
            bool kyaAbhiHeSelectKeyaHaiKya = false;
            // Chipkne wale cheez
            if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f, m_chipakneWaleLayer.value))
            {
                ChipkneWaleCheezein cheez = hit.collider.GetComponent<ChipkneWaleCheezein>();

                if (cheez == null)
                {
                    Debug.LogError(hit.collider.gameObject.name + " has not script called ChipkneWaleCheezein on it");
                }
                else
                {
                    m_chipkuCheezUnderMouse = cheez;
                    m_chipkuCheezUnderMouse.m_collider.enabled = false;
                    m_initialDisWhenChipkuCheezCameUnderMouse = hit.distance;

                    SelecatableCheezein selectMePls = hit.collider.GetComponent<SelecatableCheezein>();
                    if (selectMePls != null)
                    {
                        if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                        {
                            m_selectedCheez.Select(false);
                        }
                        kyaAbhiHeSelectKeyaHaiKya = true;
                        selectMePls.Select(true);
                        m_selectedCheez = selectMePls;
                    }
                }
            }

            // selectatble wale cheez 
            if (kyaAbhiHeSelectKeyaHaiKya == false)
            {
                if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f))
                {
                    SelecatableCheezein selectMePls = hit.collider.GetComponent<SelecatableCheezein>();
                    if (selectMePls != null)
                    {
                        if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                        {
                            m_selectedCheez.Select(false);
                        }
                        selectMePls.Select(true);
                        m_selectedCheez = selectMePls;
                    }
                    else
                    {
                        selectMePls = hit.collider.transform.parent.GetComponent<SelecatableCheezein>();

                        if (selectMePls != null)
                        {
                            if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                            {
                                m_selectedCheez.Select(false);
                            }
                            selectMePls.Select(true);
                            m_selectedCheez = selectMePls;
                        }
                        else
                        {
                            if (m_selectedCheez != null)
                            {
                                m_selectedCheez.Select(false);
                                m_selectedCheez = null;
                            }
                        }
                    }
                }
                else
                {
                    if (m_selectedCheez != null)
                    {
                        m_selectedCheez.Select(false);
                        m_selectedCheez = null;
                    }
                }
            }
        }
        else if (touchUp)
        {
            
            if (m_chipkuCheezUnderMouse != null)
            {
                m_chipkuCheezUnderMouse.m_collider.enabled = true;
            }
            m_chipkuCheezUnderMouse = null;
        }

        /*
        m_selectedCheez = m_chipkuCheezUnderMouse;
            if(m_selectedCheez != null)
            {

            }
        if(Input.ge)
        */
        if (m_chipkuCheezUnderMouse == null)
        {
            m_orbit.enabled = true;
        }
        else 
        {
            m_orbit.enabled = false;
            UpdateCheezUnderMouseDrag();
        }
    }

    void UpdateCheezUnderMouseDrag()
    {
        bool touchDown = Input.GetMouseButtonDown(0);
        Vector3 touchPos = Input.mousePosition;
        bool touchUp = Input.GetMouseButtonUp(0);

        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                touchDown = true;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                touchUp = true;
            }
            touchPos = touch.position;
        }

        RaycastHit hit;
        Ray ray = m_mainCamera.ScreenPointToRay(touchPos);
        if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f, m_dragLayers.value))
        {
            m_chipkuCheezUnderMouse.transform.position = hit.point - ray.direction*m_chipkuCheezUnderMouse.m_radius;// ray.origin + ray.direction * hit.distance;
        }
        else
        {
            m_chipkuCheezUnderMouse.transform.position = ray.origin + ray.direction * m_initialDisWhenChipkuCheezCameUnderMouse;
        }
    }
}
