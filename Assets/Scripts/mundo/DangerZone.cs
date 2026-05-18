using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public Camarahhh camara;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entro algo " + other.name);
        Debug.Log("Tag detectado: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Entro el player");

            if (camara != null)
            {
                camara.BloquearCamara();
                Debug.Log("Se bloqueo la camara");
            }
            else
            {
                Debug.LogWarning("No asignaste la camara en DangerZone");
            }
        }
    }
}