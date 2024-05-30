using UnityEngine;

public class WeaponDamageMeleEnemy : MonoBehaviour
{
    public int damage = 25;

    private EnemyHealthDamage enemyHealthDamage;

    void Start()
    {
        enemyHealthDamage = GetComponent<EnemyHealthDamage>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            var enemyHealth = other.gameObject.GetComponent<CharacterHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }

    private void Update()
    {

        if (enemyHealthDamage.health <= 0)
        {
            Destroy(gameObject, 1f);
        }

    }

}
