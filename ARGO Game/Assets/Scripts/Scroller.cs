using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float scrollSpeed;
    private float offset;

    private Renderer[] renderers;
    /// the objects whos texture will be scrolled
    public GameObject[] toScroll;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<gameManager>())
        {
            scrollSpeed = FindObjectOfType<gameManager>().getSpeed();
        }
        renderers = new Renderer[toScroll.Length];
        int i = 0;
        foreach(GameObject wall in toScroll)
        {
            renderers[i] = wall.GetComponent<Renderer>();
            i++;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += scrollSpeed / 10.0f ;
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
