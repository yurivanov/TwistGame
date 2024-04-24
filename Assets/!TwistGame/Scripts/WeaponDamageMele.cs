using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 25;
    public CharacterHealth characterHealth; // Add this line
   // private Animator weaponAnimator; // Add this line

    private void Start()
    {
        characterHealth = GameObject.Find("Player").GetComponent<CharacterHealth>(); // Add this line
       // weaponAnimator = GetComponent<Animator>(); // Add this line
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && characterHealth.health > 0) // Check if character is not dead
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealthDamage>();
            enemyHealth.TakeDamage(damage);

            // You might want to call your animation here. Remember to replace "MeleeRotate" with your animation's name.
           // weaponAnimator.Play("MeleeRotate");
        }
    }
    
    private void Update()
    {
        if(characterHealth.health <= 0)
        {
            Destroy(gameObject, 2f);
        }
    }
}
