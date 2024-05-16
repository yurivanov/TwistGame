using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int health = 100;
    private Animator animator;
    public bool isDead = false; // add this line

    private void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play("WalkCharacter");

     }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
       // animator.Play("DeadCharacter");
        isDead = true; // add this line
        //Destroy(gameObject, 2f);
    }

}
