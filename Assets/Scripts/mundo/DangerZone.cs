using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public Camarahhh camara;

    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public int cantidadEnemigos = 3;

    private bool activada;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activada)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            activada = true;

            if (camara != null)
            {
                camara.BloquearCamara();
            }

            SpawnEnemigos(other.transform);
        }
    }

    void SpawnEnemigos(Transform player)
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            Transform punto = spawnPoints[i];

            GameObject enemigo =
                Instantiate(
                    enemyPrefab,
                    punto.position,
                    Quaternion.identity
                );

            EnemyMovement movimiento =
                enemigo.GetComponent<EnemyMovement>();

            if (movimiento != null)
            {
                movimiento.player = player;
                movimiento.ActivarEnemigo();
            }
        }
    }
}