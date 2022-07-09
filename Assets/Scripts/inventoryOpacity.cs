using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryOpacity : MonoBehaviour
{

    SpriteRenderer inventoryBackground;
    // Start is called before the first frame update
    void Start()
    {
        inventoryBackground = GetComponent<SpriteRenderer>();
        inventoryBackground.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
