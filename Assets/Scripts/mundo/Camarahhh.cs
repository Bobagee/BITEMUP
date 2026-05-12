using UnityEngine;

public class Camarahhh : MonoBehaviour
{
    public Transform target;
    public float suavizado = 5f;
    public bool camaraBloqueada = false;

    private float y_fija;
    private float z_fija;

    private void Start()
    {
        y_fija = transform.position.y;
        z_fija = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (camaraBloqueada)
        {
            return;
        }

        Vector3 destino = new Vector3(
            target.position.x,
            y_fija,
            z_fija
        );

        transform.position = Vector3.Lerp(
            transform.position,
            destino,
            suavizado * Time.deltaTime
        );
    }

    public void BloquearCamara()
    {
        camaraBloqueada = true;
        Debug.Log("Se bloqueo");
    }

    public void DesbloquearCamara()
    {
        camaraBloqueada = false;
        Debug.Log("Se desbloqueo");
    }

}