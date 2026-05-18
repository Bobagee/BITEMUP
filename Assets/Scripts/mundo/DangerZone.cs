using UnityEngine;
using System.Collections.Generic;

public class DangerZone : MonoBehaviour
{
    public Camarahhh camara;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int cantidadEnemigos = 3;

    private bool activada;
    private bool zonaCompletada;

    private List<GameObject> enemigosVivos = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activada)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            activada = true;

            Debug.Log("Player entro a DangerZone");

            if (camara != null)
            {
                camara.BloquearCamara();
                Debug.Log("Camara bloqueada");
            }

            SpawnEnemigos();
        }
    }

    void Update()
    {
        RevisarEnemigos();
    }

    void SpawnEnemigos()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("No asignaste Enemy Prefab");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No asignaste Spawn Points");
            return;
        }

        int cantidadReal = cantidadEnemigos;

        if (cantidadReal > spawnPoints.Length)
        {
            cantidadReal = spawnPoints.Length;
        }

        for (int i = 0; i < cantidadReal; i++)
        {
            GameObject enemigo = Instantiate(
                enemyPrefab,
                spawnPoints[i].position,
                Quaternion.identity
            );

            enemigosVivos.Add(enemigo);

            EnemyMovement mov = enemigo.GetComponent<EnemyMovement>();

            if (mov != null)
            {
                mov.ActivarEnemigo();
            }
        }
    }

    void RevisarEnemigos()
    {
        if (!activada || zonaCompletada)
        {
            return;
        }

        for (int i = enemigosVivos.Count - 1; i >= 0; i--)
        {
            if (enemigosVivos[i] == null)
            {
                enemigosVivos.RemoveAt(i);
            }
        }

        if (enemigosVivos.Count == 0)
        {
            zonaCompletada = true;

            if (camara != null)
            {
                camara.DesbloquearCamara();
                Debug.Log("Zona completada, camara desbloqueada");
            }
        }
    }
}