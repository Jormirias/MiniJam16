using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform target;
    public Transform stinger;
    public GameObject bullet;
    public float fireRate;
    public float shootingPower;
    public Animator animator;

    private float shootingTime;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(Time.time > shootingTime)
        {
            animator.SetTrigger("Attack");
            shootingTime = Time.time + fireRate / 1000;
            Vector2 myPos = new Vector2(stinger.position.x, stinger.position.y);
            GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity);
            Vector2 direction = (Vector2)target.position - myPos;
            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * shootingPower;
        }
    }
}
