using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    public bool activo;

    public float velocidad = 2f;
    public float distanciaDetenerse = 1.2f;

    void Update()
    {
        if (!activo)
        {
            return;
        }

        PerseguirJugador();
    }

    public void ActivarEnemigo()
    {
        activo = true;
    }

    public void PerseguirJugador()
    {
        if (player == null)
        {
            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia > distanciaDetenerse)
        {
            Vector2 direccion =
                (
                    player.position -
                    transform.position
                ).normalized;

            transform.position +=
                (Vector3)(direccion * velocidad * Time.deltaTime);

            // mirar al jugador
            if (direccion.x > 0)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, 0);
            }

            if (direccion.x < 0)
            {
                transform.rotation =
                    Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
