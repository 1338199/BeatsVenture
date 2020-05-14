using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmout = 30;
    public float maxDistance = 3f;
    public float speed = 10f;

    public GameObject spawnPos;

    private TrailRenderer trailRenderer;
    private Vector3 originPos;

    void Start()
    {
        originPos = this.transform.position;
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        Fly();
    }

    void Fly()
    {
        this.transform.Translate(spawnPos.transform.forward * Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, originPos) > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damageAmout);
            Destroy(this.gameObject);
        }
    }
}
