/*using UnityEngine;

public class EnemyHealthDamage : MonoBehaviour
{
    public int health = 50;
    public bool isDead = false; // add this line
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("WalkEnemy");
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
        isDead = true; // set isDead to true
        animator.Play("DeadEnemy");
        Destroy(gameObject, 2f);
    }
}
*/





using UnityEngine;
using System.Collections;

public class EnemyHealthDamage : MonoBehaviour
{
    public int health = 50;
    public bool isDead = false;
    public Material hurtMaterial;
    public MeshRenderer[] meshRenderers;

    private Material originalMaterial;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        originalMaterial = meshRenderers[0].material;
        animator.Play("WalkEnemy");
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(ChangeMaterialTemporarily());
        animator.Play("EnemyHit"); // Play the hit animation
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.Play("DeadEnemy");
        Destroy(gameObject, 2f);
    }

    private IEnumerator ChangeMaterialTemporarily()
    {
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.material = hurtMaterial;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.material = originalMaterial;
        }
        if (!isDead) // check if the enemy is not dead
        {
            animator.Play("WalkEnemy"); // switch back to WalkEnemy animation
        }
    }
}
