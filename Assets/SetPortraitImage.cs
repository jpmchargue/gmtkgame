using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPortraitImage : MonoBehaviour
{
    // Start is called before the first frame update


    public RawImage portrait;


    public void setPortraitImage(Texture image)
    {
        portrait.texture = image;
    }
}
