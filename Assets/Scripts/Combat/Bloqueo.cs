using UnityEngine;

public class Bloqueo : MonoBehaviour
{
    public bool bloqueando;
    public float tiempoBloqueoRoto = 0.6f;

    private bool bloqueoRoto;

    public void ActivarBloqueo()
    {
        if (!bloqueoRoto)
        {
            bloqueando = true;
        }
    }

    public void DesactivarBloqueo()
    {
        bloqueando = false;
    }

    public bool EstaBloqueando()
    {
        if (bloqueando && !bloqueoRoto)
        {
            return true;
        }

        return false;
    }

    public void RomperBloqueo()
    {
        bloqueando = false;
        bloqueoRoto = true;

        Debug.Log(gameObject.name + " bloqueo roto");

        CancelInvoke("RecuperarBloqueo");
        Invoke("RecuperarBloqueo", tiempoBloqueoRoto);
    }

    void RecuperarBloqueo()
    {
        bloqueoRoto = false;
    }
}