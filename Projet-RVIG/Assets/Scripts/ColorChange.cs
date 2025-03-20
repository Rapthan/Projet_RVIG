using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color1 = Color.green;
    private Color color2 = Color.white;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            spriteRenderer.color = color1;
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = color2;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
