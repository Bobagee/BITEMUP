using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    public bool activo;

    public float velocidad = 2.3f;
    public float distanciaDetenerse = 1.15f;

    void Start()
    {
        if (player == null)
        {
            GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

            if (objPlayer != null)
            {
                player = objPlayer.transform;
            }
        }
    }

    void Update()
    {
        if (!activo)
        {
            return;
        }

        EnemyController controller = GetComponent<EnemyController>();

        if (controller != null && controller.stuneado)
        {
            return;
        }

        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
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
                player.position - transform.position;

            direccion = direccion.normalized;

            transform.position +=
                (Vector3)(direccion * velocidad * Time.deltaTime);

            if (direccion.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (direccion.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}