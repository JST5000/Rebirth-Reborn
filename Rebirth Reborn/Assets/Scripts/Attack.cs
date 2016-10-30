using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public float strikeForce = 3500;
    public float attackTimer = 100.0f;
    float hitTime = .5f;
    public bool isAttacking = false;
    //float attackTime = 0.0f;
    float cooldown = 0;
    float maxCooldown = 1;
    float attackDuration = 0.1f;
    public bool canAttack = true;

    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        cooldown += Time.deltaTime;

        if (cooldown >= attackDuration)
        {
            isAttacking = false;
        }

        if (cooldown >= maxCooldown)
        {
            canAttack = true;
        }

    }

    public void Attack1(float vert, float hori)
    {
        if (canAttack)
        {
            canAttack=false;
            cooldown = 0;
            isAttacking = true;
            Debug.Log("I AM ATTACKING");
            Vector2 direction = new Vector2(hori, vert);
            rb.AddForce(direction * strikeForce);
       //     attackTime = Time.time

        }
    }
}
