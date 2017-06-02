using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public float projectileSpeed = 10f;
    float damageCaused = 10;
    //[SerializeField]
    //private GameObject player;

        public void SetDamage(float damage)
    {
        damageCaused = damage;
    }
	// Use this for initialization
	void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, projectileSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {

        Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable));
        if(damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);



        }
    }
}
