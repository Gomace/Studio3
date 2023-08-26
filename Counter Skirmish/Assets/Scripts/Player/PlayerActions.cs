using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Global global;
    //[SerializeField] private Transform useTexts;
    private LayerMask useLayer = 1 << 6;
    
    private float maxUseDistance = 2f;
    private RaycastHit hit;

    private Transform curText;
    private Coroutine textMouse;

    private void Awake()
    {
        if (!global/* || !useTexts*/)
            Debug.Log("Missing inspector drag & drop reference. Please help :[");
    }
    
    private void Update()
    {
        if (global.disabled)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Check unity input stuff
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if ((Physics.Raycast(ray, out hit, maxUseDistance, useLayer)))
        {
            switch (hit.collider.tag)
            {
                case "Milk":
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        
                    }
                    break;
                default:
                    
                    break;

            }
        }
        /*else
            TextOff();*/
    }

    /*private void TextOn(int child)
    {
        if (useTexts)
        {
            if (useTexts.GetChild(child))
            {
                if (curText != useTexts.GetChild(child))
                {
                    curText = useTexts.GetChild(child);
                    if (textMouse != null)
                        StopCoroutine(textMouse);
                    textMouse = StartCoroutine(ChangeMouse());
                }

                if (!curText.gameObject.activeSelf)
                    curText.gameObject.SetActive(true);
            }
        }
    }

    private void TextOff()
    {
        if (curText)
        {
            if (curText.gameObject.activeSelf)
            {
                curText.gameObject.SetActive(false);
                curText = null;
            }
        }
    }
    
    private IEnumerator ChangeMouse()
    {
        bool click = false;
        while (true)
        {
            if (curText)
            {
                if (curText.gameObject.activeSelf)
                {
                    curText.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(click ? "Images/MouseRed" : "Images/MouseBlack");
                    click = !click;
                }
            }
            yield return new WaitForSeconds((float)0.5);
        }
    }*/
}