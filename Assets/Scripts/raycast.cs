using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    private AgentScript agentScript;
    [SerializeField] private float RaycastDistancia = 8f; // un poco más largo para que detecte mejor
    [SerializeField] private Transform RaycastOrigin;

    void Start()
    {
        agentScript = GetComponent<AgentScript>();
    }

    void Update()
    {
        bool JugadorDetectado = false;

        // Dibujar el rayo rojo
        Debug.DrawRay(RaycastOrigin.position, transform.forward * RaycastDistancia, Color.red);

        // Lanzar raycast desde un poco más arriba
        if (Physics.Raycast(RaycastOrigin.position, transform.forward, out RaycastHit hitInfo, RaycastDistancia))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                agentScript.patrullando = false;
                agentScript.jugadorEnVision = true;
                agentScript.tiempoSinVision = 0;
                JugadorDetectado = true;
            }
            else
            {
                JugadorDetectado = false;
            }
        }

        if (!JugadorDetectado)
        {
            agentScript.jugadorEnVision = false;
        }

        if (!agentScript.jugadorEnVision && !agentScript.patrullando)
        {
            agentScript.tiempoSinVision += (int)(Time.deltaTime * 100);

            if (agentScript.tiempoSinVision >= 500)
            {
                agentScript.patrullando = true;
            }
        }
    }
}
