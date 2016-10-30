using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
        if (gameObject.GetComponent<BehaviorAI>().typeOfCreature == "Plant")
        {
            //plant
            health = 10;
        }
        else if (gameObject.GetComponent<BehaviorAI>().typesOfFood.Length == 2)
        {
            //omnivore
            health = 50;
        }
        else if (gameObject.GetComponent<BehaviorAI>().typesOfFood[0] == "Plant")
        {
            //herbivore
            health = 80;
        }
        else 
        {
            //carnivore
            health = 60;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Ive collided");
        Attack atk = coll.gameObject.GetComponentInParent<Attack>();
        Debug.Log("is null? " + Equals(atk, null) +"\n is attacking? " + atk.isAttacking);
        //coll.gameObject.name == "Player"
        if (!Equals(atk, null) && atk.isAttacking)
        {
            Debug.Log("Ive been hit");
            health -= 10;
            Debug.Log(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
         }

        
    }
}
