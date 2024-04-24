using UnityEngine;

public class WeaponDamageMeleEnemy : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            var enemyHealth = other.gameObject.GetComponent<CharacterHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
