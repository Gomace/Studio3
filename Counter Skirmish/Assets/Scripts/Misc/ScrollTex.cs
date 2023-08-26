using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScrollTex : MonoBehaviour
{
    // by Jimmy Vegas on YouTube
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    
    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}