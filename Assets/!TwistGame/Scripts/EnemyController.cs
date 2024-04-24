using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Use this for initialization
    public Transform Player;
    private NavMeshAgent MyAgent;
    private EnemyHealthDamage enemyHealth; // add this line

    void Start()
    {
        MyAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform; // find the player gameobject
        enemyHealth = GetComponent<EnemyHealthDamage>(); // add this line
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null && !enemyHealth.isDead) // check if the enemy is not dead
        {
            MyAgent.SetDestination(Player.position); // set the destination of the NavMeshAgent to the player.
        }
    }
}
