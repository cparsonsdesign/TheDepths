using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maul_Projectile : MonoBehaviour {

    [SerializeField]
    private float damage = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable));
        if(damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damage);



        }
    }
}
