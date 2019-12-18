using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    [SerializeField] AudioClip m_se;

    // Start is called before the first frame update
    void Start()
    {
        test();
    }

    // Update is called once per frame
    void Update()
    {
        //AudioManager.Instance.FadeBGM();
    }

    void test()
    {
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //
        //Vector3[] vectores = mesh.vertices;
        //Vector3[] 

        //Debug.Log(vectores.ToString());
    }
}
