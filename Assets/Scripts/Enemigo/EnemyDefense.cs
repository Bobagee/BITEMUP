using UnityEngine;

public class EnemyDefense : MonoBehaviour
{
    public int golpesParaBloquear = 3;
    public float duracionBloqueo = 1.5f;

    private int golpesNormalesRecibidos;
    private Bloqueo bloqueo;

    void Start()
    {
        bloqueo = GetComponent<Bloqueo>();
    }

    public void RecibirGolpeNormal()
    {
        golpesNormalesRecibidos++;

        Debug.Log("Golpes normales recibidos: " + golpesNormalesRecibidos);

        if (golpesNormalesRecibidos >= golpesParaBloquear)
        {
            ActivarBloqueoTemporal();
        }
    }

    void ActivarBloqueoTemporal()
    {
        if (bloqueo == null)
        {
            return;
        }

        bloqueo.ActivarBloqueo();

        Debug.Log("Enemy bloqueando");

        golpesNormalesRecibidos = 0;

        CancelInvoke("QuitarBloqueo");
        Invoke("QuitarBloqueo", duracionBloqueo);
    }

    void QuitarBloqueo()
    {
        if (bloqueo != null)
        {
            bloqueo.DesactivarBloqueo();
            Debug.Log("Enemy dejo de bloquear");
        }
    }
}