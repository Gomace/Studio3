using UnityEngine;
using System.Collections;

public class FireSpreadTest : MonoBehaviour
{
    void Update()
    {
        // Grow the fire in the X & Y directions
        transform.localScale += new Vector3(Time.deltaTime, 0f, Time.deltaTime);
    }

}