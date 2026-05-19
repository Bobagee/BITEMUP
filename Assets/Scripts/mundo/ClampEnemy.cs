using UnityEngine;

public class ClampEnemy : MonoBehaviour
{
    public BoxCollider2D zonaMovimiento;
    public bool limitarY = true;

    void Start()
    {
        if (zonaMovimiento == null)
        {
            GameObject zona = GameObject.FindGameObjectWithTag("MovementZone");

            if (zona != null)
            {
                zonaMovimiento = zona.GetComponent<BoxCollider2D>();
            }
        }
    }

    void LateUpdate()
    {
        if (zonaMovimiento == null)
        {
            return;
        }

        Bounds limites = zonaMovimiento.bounds;

        float xLimitada = Mathf.Clamp(
            transform.position.x,
            limites.min.x,
            limites.max.x
        );

        float yLimitada = transform.position.y;

        if (limitarY)
        {
            yLimitada = Mathf.Clamp(
                transform.position.y,
                limites.min.y,
                limites.max.y
            );
        }

        transform.position = new Vector3(
            xLimitada,
            yLimitada,
            transform.position.z
        );
    }
}