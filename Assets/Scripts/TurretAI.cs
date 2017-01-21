﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public float range;
    public float fireRate;
    public GameObject projectile;
    public Transform shotOrigin;
    
    private float nextFire;
    private List<Collider2D> hostileList = new List<Collider2D>();
    private Transform target;

	void Start ()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = range;
	}
	
	void FixedUpdate ()
    {
        if (hostileList.Count != 0)
        {
            AquireTarget();

            //Make it follow a target
            if (Time.time >= nextFire)
            {
                Fire();
            }
        }


		
	}

    void AquireTarget()
    {
        Collider2D selected = null;
        float minDist = float.PositiveInfinity;
        foreach (Collider2D enemy in hostileList)
        {
            if (enemy == null)
            {
                hostileList.Remove(enemy);
            }
            if (enemy.GetComponentInParent<WalkerAI>().distToEnd < minDist)
            {
                selected = enemy;
                minDist = enemy.GetComponentInParent<WalkerAI>().distToEnd;
            }
        }
        target = selected.transform;

        float timeToHit = Vector3.Distance(target.transform.position, transform.position) / projectile.GetComponent<Shot>().speed;

        Vector3 targetVelocity = selected.GetComponent<Rigidbody2D>().velocity;

        Vector3 aimPoint = target.position + (targetVelocity * timeToHit);

        Vector3 rotDir = (aimPoint- transform.position);
        float angle = Mathf.Atan2(rotDir.y, rotDir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Fire()
    {
        Instantiate(projectile, shotOrigin.transform.position, this.transform.rotation);
        nextFire = Time.time + fireRate;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Arse");
        if (!hostileList.Contains(other) && other.tag == "Enemy")
        {
            hostileList.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("De-Arse");
        if (hostileList.Contains(other) && other.tag == "Enemy")
        {
            hostileList.Remove(other);
        }
    }
}
