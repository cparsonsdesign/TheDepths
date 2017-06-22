using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageable {

    #region(Player Stats)
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private int enemyLayer = 9;
    [SerializeField]
    Weapon weaponInUse;
    #endregion


    #region (For Attacking)
    [SerializeField]
    private int damagePerHit = 10;
    [SerializeField]
    private float minTimeBetweenHits = .5f;
    [SerializeField]
    private float maxAttackRange = 2f;
    private float lastHitTime = 0f;
    #endregion

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
        RegisterForMouseClick();
        currentHealth = maxHealth;
        PutWeaponInHand();
    }

    private void PutWeaponInHand()
    {
        var weaponPrefab = weaponInUse.GetWeaponPrefab();
        GameObject dominantHand = RequestDominantHand();
        var weapon = Instantiate(weaponPrefab, dominantHand.transform);
        weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
        weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
    }

    private GameObject RequestDominantHand()
    {
        var dominantHands = GetComponentsInChildren<DominantHand>();
        int numberOfDominantHands = dominantHands.Length;
        Assert.IsFalse(numberOfDominantHands <= 0, "No DomiantHand found, Please add one.");
        Assert.IsFalse(numberOfDominantHands > 1, "Multiple DominantHand scripts on Player, please remove extras.");
        return dominantHands[0].gameObject;
        
    }

    private void RegisterForMouseClick()
    {
        camRay = FindObjectOfType<CameraRaycaster>();
        camRay.notifyMouseClickObservers += OnMouseClicked;
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
