using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Sprite sprite;
    public Vector3 position;
    public Vector2Int size;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprite;

        transform.position = new Vector3(position.x, position.y);
        var diagonal = Mathf.Sqrt(size.x * size.x + size.y * size.y);
        transform.localScale = new Vector3(size.x, diagonal * 0.5f);
    }
}
