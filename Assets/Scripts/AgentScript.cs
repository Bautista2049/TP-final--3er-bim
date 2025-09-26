using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] waypoints; // puntos de patrulla
    [SerializeField] Animator anim;
    [SerializeField] float velocity;
    [SerializeField] float distanciaMin = 0.5f; // qué tan cerca tiene que estar para considerar que llegó

    int indiceActual = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (waypoints.Length > 0)
        {
            agent.destination = waypoints[indiceActual].position;
        }
    }

    void Update()
    {
        // si no hay waypoints, salir
        if (waypoints.Length == 0) return;

        // mover hacia el destino actual
        agent.destination = waypoints[indiceActual].position;

        // animación
        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed", velocity);

        // chequear si llegó
        if (!agent.pathPending && agent.remainingDistance <= distanciaMin)
        {
            // pasar al siguiente
            indiceActual++;
            if (indiceActual >= waypoints.Length)
            {
                indiceActual = 0; // vuelve al primero
            }

            agent.destination = waypoints[indiceActual].position;
        }
    }
}