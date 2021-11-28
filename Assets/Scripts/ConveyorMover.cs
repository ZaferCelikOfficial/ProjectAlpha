using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMover : MonoBehaviour
{

    [SerializeField] Material ConveyorMaterial;
    float offset;
    public static float OffsetSpeed=0.002f;

    // Start is called before the first frame update
    void Start()
    {
        ConveyorMaterial = gameObject.GetComponent<Renderer>().material;
 
        
    }
    
    // Update is called once per frame
    void Update()
    {
        offset += OffsetSpeed;
        ConveyorMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
