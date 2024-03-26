using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    [SerializeField] 
    private float turnRate = 20f;

    [SerializeField] 
    private float thrustValue = 10f;
    
    [SerializeField] 
    private float attackDelay = 0.5f;
    
    private Rigidbody rb;

    private bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleRotate();
        HandleAttack();
    }

    private void FixedUpdate()
    {
        var yInput = Input.GetAxisRaw("Vertical");

        if (yInput > 0)
        {
            rb.AddForce(transform.up * (yInput * thrustValue)); 
        }
    }

    private void HandleRotate()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Vector3.forward * -(xInput * turnRate * Time.deltaTime));
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            StartCoroutine(ShootBullet());
        }
    }

    private IEnumerator ShootBullet()
    {
        canAttack = false;
        var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().Direction = transform.up;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
