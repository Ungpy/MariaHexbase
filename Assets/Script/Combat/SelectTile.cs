using Unity.InferenceEngine.Tokenization;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            Debug.Log("Success");
        }
    }
    private void OnMouseEnter()
    {
        spriteRenderer.color = Color.red;
        Debug.Log("Mouse Hover");
    }
    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
}
