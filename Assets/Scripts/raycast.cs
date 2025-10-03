using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    private AgentScript agentScript;
    float RaycastDistancia = 5f;

    void Start()
    {
        agentScript = GetComponent<AgentScript>();
    }

    void Update()
    {
        bool JugadorDetectado = false;

        Debug.DrawRay(transform.position, transform.forward * RaycastDistancia, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, RaycastDistancia))
        {
            if (hitInfo.collider.gameObject.tag == "Player")
            {
                agentScript.patrullando = false;
                agentScript.jugadorEnVision = true;
                agentScript.tiempoSinVision = 0;
                JugadorDetectado = true;
            }
        }

        if (!JugadorDetectado)
        {
            agentScript.jugadorEnVision = false;
        }

        if (!agentScript.jugadorEnVision && !agentScript.patrullando)
        {
            agentScript.tiempoSinVision += (int)(Time.deltaTime * 100);
            Debug.Log(agentScript.tiempoSinVision);
            if (agentScript.tiempoSinVision >= 500)
            {
                agentScript.patrullando = true;
                agentScript.tiempoSinVision = 0;
            }
        }
    }
}