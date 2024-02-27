/*using UnityEngine;

public class ScrollTex : MonoBehaviour
{
    // by Jimmy Vegas on YouTube
    private Renderer _renderer;
    
    private float _scrollX = 0.5f;
    private float _scrollY = 0.5f;

    public float ScrollX => _scrollX;
    public float ScrollY => _scrollY;

    private void Awake() => _renderer = GetComponent<Renderer>();
        

    private void Update()
    {
        float offsetX = Time.time * _scrollX;
        float offsetY = Time.time * _scrollY;
        _renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}*/