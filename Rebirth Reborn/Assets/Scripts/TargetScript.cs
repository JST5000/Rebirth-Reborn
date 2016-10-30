using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
        if (gameObject.GetComponent<BehaviorAI>().typeOfCreature == "Plant")
        {
            health = 10;
        }
        else if (gameObject.GetComponent<BehaviorAI>().typeOfCreature == "Animal"
            && gameObject.GetComponent<BehaviorAI>().typesOfFood.Length == 2)
        {
            health = 50;
        }
        else
        {
            health = 60;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && coll.gameObject.GetComponent<PlayerController>().isAttacking==true)
        {
            Debug.Log("Ive been hit");
            health -= 10;
            Debug.Log(health);
            if (health<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
