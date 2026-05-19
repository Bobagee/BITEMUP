using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public Transform cameraRig;

    public Transform[] piezas;

    public float anchoPieza = 10f;

    void Start()
    {
        if (cameraRig == null)
        {
            GameObject rig = GameObject.Find("CameraRig");

            if (rig != null)
            {
                cameraRig = rig.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (cameraRig == null)
        {
            return;
        }

        for (int i = 0; i < piezas.Length; i++)
        {
            if (piezas[i] == null)
            {
                continue;
            }

            float distancia = cameraRig.position.x - piezas[i].position.x;

            if (distancia > anchoPieza)
            {
                piezas[i].position = new Vector3(
                    piezas[i].position.x + anchoPieza * piezas.Length,
                    piezas[i].position.y,
                    piezas[i].position.z
                );
            }

            if (distancia < -anchoPieza)
            {
                piezas[i].position = new Vector3(
                    piezas[i].position.x - anchoPieza * piezas.Length,
                    piezas[i].position.y,
                    piezas[i].position.z
                );
            }
        }
    }
}