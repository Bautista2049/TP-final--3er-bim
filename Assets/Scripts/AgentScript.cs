using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AgentScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float velocity;

    [Header("Patrulla")]
    [SerializeField] private Transform[] Puntos;
    private int currentPatrolPoint = 0;
    public bool patrullando = true;

    [Header("Jugador")]
    public GameObject jugador;
    public bool jugadorEnVision = false;
    public int tiempoSinVision = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Ir al primer punto de patrulla al empezar
        if (Puntos.Length > 0)
        {
            agent.SetDestination(Puntos[0].position);
        }
    }

    private void Update()
    {
        if (patrullando)
        {
            Patrullar();
        }
        else
        {
            PerseguirJugador();
        }

        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed", velocity);
    }

    private void Patrullar()
    {
        if (Puntos.Length == 0) return;

        // Si llegó al punto, pasa al siguiente
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % Puntos.Length;
            agent.SetDestination(Puntos[currentPatrolPoint].position);
        }
    }

    private void PerseguirJugador()
    {
        if (jugador != null)
        {
            agent.SetDestination(jugador.transform.position);
        }
    }

    public void ReiniciarPatrulla()
    {
        patrullando = true;
        tiempoSinVision = 0;
        if (Puntos.Length > 0)
        {
            agent.SetDestination(Puntos[currentPatrolPoint].position);
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
