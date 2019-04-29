using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInit : MonoBehaviour {

    public GameObject targetGameObject;
    public Color color;

    // Use this for initialization
    
    void Update () {
        targetGameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color);
	}
}
