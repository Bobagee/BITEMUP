using UnityEngine;

public class SortingY : MonoBehaviour
{
    //El sorting ordena las cosas en el eje y

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (sprite != null)
        {
            sprite.sortingOrder = Mathf.RoundToInt(
                transform.position.y * -100
            );
        }
    }
}