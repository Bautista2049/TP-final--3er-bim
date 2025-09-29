using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AgentScript : MonoBehaviour
{
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject jugador;
    [SerializeField] private float distanciaCambio = 0.4f;

    private NavMeshAgent navAgent;
    private int indiceActual = 0;
    private bool enModoPatrulla = true;
    private float velocidad;

    // por si despues queres usarlo con raycast
    public bool jugadorDetectado = false;
    public float tiempoSinDetectar = 0f;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (puntosPatrulla != null && puntosPatrulla.Length > 0)
        {
            navAgent.SetDestination(puntosPatrulla[indiceActual].position);
        }
    }

    private void Update()
    {
        if (enModoPatrulla)
        {
            Patrullar();
        }
        else
        {
            PerseguirJugador();
        }

        velocidad = navAgent.velocity.magnitude;
        animator.SetFloat("Speed", velocidad);
    }

    private void Patrullar()
    {
        if (puntosPatrulla.Length == 0) return;

        if (!navAgent.pathPending && navAgent.remainingDistance <= distanciaCambio)
        {
            indiceActual++;
            if (indiceActual >= puntosPatrulla.Length)
                indiceActual = 0;

            navAgent.SetDestination(puntosPatrulla[indiceActual].position);
        }
    }

    private void PerseguirJugador()
    {
        if (jugador != null)
        {
            navAgent.SetDestination(jugador.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Perdiste");
        }
    }
}