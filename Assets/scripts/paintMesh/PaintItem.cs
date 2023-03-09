using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintItem : MonoBehaviour
{

    //Simple scirpt to paint material model (Albedo color) using MaterialPropetry block on start for defaul Lit/Unlit Material

    [Header("Main")]
    [Tooltip("Mesh to paint")]
    public MeshRenderer MeshToPaint;
    [Tooltip("material number to paint (default 0)")]
    public int materialNoToPaint;
    [Tooltip("Check if want to paint on start, you cal always call funcion by script")]
    public bool paintOnStart;

    [Header("Attributes")]
    [Tooltip("Color of the material")]
    public Color ColorToPaint = Color.white;
    public float Smoothness = 0.2f;
    public float Metallic = 0.2f;
    public float Opacity = 1f;


    // Start is called before the first frame update

    private void OnEnable()    //if no mesh selected, try with parent object
    {
        if(MeshToPaint == null)
        {
            MeshToPaint = GetComponent<MeshRenderer>();
        }
    }

    void Start()
    {

        if (paintOnStart && MeshToPaint != null)                    //color on start if there is a mesh
            Paint();
        else
            Debug.Log("No mesh to paint");
        
    }

    public void Paint()
    {
        MaterialPropertyBlock paintP = new MaterialPropertyBlock();



        if (MeshToPaint != null)
        {
            MeshToPaint.GetPropertyBlock(paintP, materialNoToPaint);


            ColorToPaint.a = Opacity;
            paintP.SetColor("_BaseColor", ColorToPaint);
            paintP.SetFloat("_Metallic", Metallic);
            paintP.SetFloat("_Smoothness", Smoothness);


            MeshToPaint.SetPropertyBlock(paintP, 0);
        }
        else
            Debug.Log("No mesh to paint");
    }
}
