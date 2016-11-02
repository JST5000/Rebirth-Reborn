using UnityEngine;
using System.Collections;

public class DashAttack : MonoBehaviour {

    float damage;
    float distance;

	// Use this for initialization
	void Start () {
        damage = 10;
        distance = 1;
	}

    void Dash(Rigidbody rb, Vector2 direction)
    {
        rb.AddForce(direction * distance);
    }
}
