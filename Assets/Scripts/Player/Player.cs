using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private int enemyLayer = 9;
    [SerializeField]
    private int damagePerHit = 10;

    [SerializeField]
    private float minTimeBetweenHits = .5f;
    [SerializeField]
    private float maxAttackRange = 2f;
    private float lastHitTime = 0f;

    GameObject currentTarget;
    CameraRaycaster camRay;


    public float healthAsPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    private void Start()
    {
        camRay = FindObjectOfType<CameraRaycaster>();
        camRay.notifyMouseClickObservers += OnMouseClicked;
        currentHealth = maxHealth;
    }
    public void TakeDamage (float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        //if(currentHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}

    }

    void OnMouseClicked(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == 9)
        {
            var enemy = raycastHit.collider.gameObject;
           
            if ((enemy.transform.position - transform.position).magnitude > maxAttackRange)
            {
                return;
            }
            currentTarget = enemy;
            var enemyComponent = enemy.GetComponent<Enemy>();
            if (Time.time - lastHitTime > minTimeBetweenHits)
            {
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }
        }
    }
}
