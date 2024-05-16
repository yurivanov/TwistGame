using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthDamage : MonoBehaviour
{
    public int health = 50;
    public bool isDead = false;
    public Material hurtMaterial;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderer;

    private List<Material[]> originalMaterials = new List<Material[]>(); // keep track of original materials
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderer)
        {
            originalMaterials.Add(renderer.materials); // store the original materials
        }
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
        Material[] hurtMaterials;

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderer)
        {
            hurtMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < hurtMaterials.Length; ++i)
            {
                hurtMaterials[i] = hurtMaterial;
            }

            renderer.materials = hurtMaterials;
        }

        yield return new WaitForSeconds(0.1f); // changed from 0.1f to 1f

        for (int i = 0; i < skinnedMeshRenderer.Length; ++i)
        {
            skinnedMeshRenderer[i].materials = originalMaterials[i];
        }

        if (!isDead) // check if the enemy is not dead
        {
            animator.Play("WalkEnemy"); // switch back to WalkEnemy animation
        }
    }
}
