using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

    #region(Enemy_Stats)
    [SerializeField]
    private float currentHealth = 100f;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float chaseRadius = 4f;
    [SerializeField]
    private float attackRadius = 2f;

    private bool isAttacking = false;
    #endregion

    #region(Projectile Variables)
    [SerializeField]
    private GameObject projectileToUse;
    [SerializeField]
    private GameObject projectileSocket;
    [SerializeField]
    private float damagePerShot = 7;
    [SerializeField]
    private float fireRate = 0.5f;
    #endregion




    #region (Other Components and Scripts)
    ThirdPersonCharacter thirdPersonCharacter = null;
    AICharacterControl ai = null;
    GameObject player;
    #endregion

    #region (getters)
    public float healthAsPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTarget");
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        ai = GetComponent<AICharacterControl>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= attackRadius && !isAttacking)
            {
                isAttacking = true;
                InvokeRepeating("SpawnProjectile", 0f, fireRate); //TODO Switch to Coroutines ASAP!!!!!
            }
            else if (distanceToPlayer >= attackRadius)
            {
                isAttacking = false;
                CancelInvoke();
            }

            if (distanceToPlayer <= chaseRadius)
            {
                ai.SetTarget(player.transform);
            }
            else
            {
                ai.SetTarget(transform);
            }
        }
        else
        {
            return;
        }
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetDamage(damagePerShot);

        Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        if (currentHealth <= 0)
            Destroy(gameObject);

    }

    private void OnDrawGizmos()
    {
        //Draw attack Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        //Draw movement Gizmos
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
