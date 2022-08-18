using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationPicker : MonoBehaviour
{
    public CakeDecorationFileManager m_fileManager;

    //public GameObject[] m_decorations;
    public Transform m_decoSpawnPosition;
    public float m_spawnRadius = 5f;


    public void CreateDecoration(int index)
    {
        if(index < 0 || index >= m_fileManager.m_prefabs.Length)
        {
            Debug.LogError("trying to spawn decoration which does not exist " + index);
            return;
        }

        Vector3 spawnPosForThisDecoItem = m_decoSpawnPosition.position;
        Vector3 rndVec = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        spawnPosForThisDecoItem += rndVec.normalized * m_spawnRadius;

        GameObject decoGO = Instantiate(m_fileManager.m_prefabs[index], spawnPosForThisDecoItem, Quaternion.identity);
        decoGO.SetActive(true);

        ChipkneWaleCheezein cheez = decoGO.GetComponent<ChipkneWaleCheezein>();

        if(cheez == null)
        {
            Debug.LogError(decoGO.name + " per ChipkneWaleCheez ka component nahi lagaya gadhi");
            Destroy(decoGO);
            return;
        }

        m_fileManager.m_thingsToSave.Add(cheez);

    }
}

