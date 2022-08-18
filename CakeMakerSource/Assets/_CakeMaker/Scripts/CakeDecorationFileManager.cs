using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CakeDecorationFileManager : MonoBehaviour
{
    // Public //
    public List<SelecatableCheezein> m_thingsToSave;
    public string m_fileName;
    public GameObject[] m_prefabs;
    public Transform m_parent;
    // Protected //
    // Private //
    // Access //

    void Start()
    {
        
    }

    public void SaveFile()
    {
        FileStream file = File.Create(Application.persistentDataPath + "\\" + m_fileName);
        BinaryWriter binWriter = new BinaryWriter(file);

        binWriter.Write(m_thingsToSave.Count);
        for(int i=0; i<m_thingsToSave.Count; i++)
        {
            SelecatableCheezein saveMe = m_thingsToSave[i];
            binWriter.Write((int)saveMe.m_type);

            binWriter.Write(saveMe.transform.position.x);
            binWriter.Write(saveMe.transform.position.y);
            binWriter.Write(saveMe.transform.position.z);
            
            binWriter.Write(saveMe.transform.rotation.x);
            binWriter.Write(saveMe.transform.rotation.y);
            binWriter.Write(saveMe.transform.rotation.z);
            binWriter.Write(saveMe.transform.rotation.w);

            binWriter.Write(saveMe.m_mainMaterial.color.r);
            binWriter.Write(saveMe.m_mainMaterial.color.g);
            binWriter.Write(saveMe.m_mainMaterial.color.b);

            binWriter.Write(saveMe.transform.localScale.x);
            binWriter.Write(saveMe.transform.localScale.y);
            binWriter.Write(saveMe.transform.localScale.z);

        }

        file.Close();
    }

    public void LoadFile()
    {
        FileStream file = File.Open(Application.persistentDataPath + "\\" + m_fileName, FileMode.Open);

        for(int i=0; i<m_thingsToSave.Count; i++)
        {
            Destroy(m_thingsToSave[i].gameObject);
        }
        m_thingsToSave.Clear();

        BinaryReader reader = new BinaryReader(file);

        int count = reader.ReadInt32();

        for(int i=0; i<count; i++)
        {
            SelecatableCheezein.Type type = (SelecatableCheezein.Type)reader.ReadInt32();

            Vector3 pos = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Quaternion rotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Color color = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Vector3 scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());


            if (m_prefabs.Length > (int)type)
            {
                GameObject go = Instantiate<GameObject>(m_prefabs[(int)type]);
                go.transform.position = pos;
                go.transform.rotation = rotation;
                go.transform.localScale = scale;

                SelecatableCheezein cheez = go.GetComponent<SelecatableCheezein>();
                if(cheez != null)
                {
                    cheez.m_mainMaterial = cheez.m_mainMesh.material;
                    cheez.m_mainMaterial.color = color;
                    m_thingsToSave.Add(cheez);
                }
            }
        }

        file.Close();
    }
}
