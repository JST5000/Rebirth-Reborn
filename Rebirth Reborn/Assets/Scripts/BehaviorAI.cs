using UnityEngine;
using System.Collections;

public class BehaviorAI : MonoBehaviour
{

    [Range(0, 1000)]
    public float speed;

    [Range(1, 10000)]
    public float awarenessRadius;

    [Range(0, 1000)]
    public float power;

    public string typeOfCreature;

    public string[] typesOfFood;

    private GameObject[] nearbyEntities;

    private Vector2 firstHostile;

    private Vector2 firstFleeFrom;

    private GameObject current;

    private string behavior;

    private int hp;


    private Vector3 direction;

    private float timeSinceLastPathing = 0;

    // Use this for initialization
    void Start()
    {
        behavior = "Idle";
        direction = new Vector3();
    }

    void initTypesOfFood()
    {
        typesOfFood = new string[2];
        typesOfFood[0] = "Plant";
        typesOfFood[1] = "Animal";
    }

    Vector2 GetGeneralDirection()
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                return new Vector2(1, 0);
            }
            else
            {
                return new Vector2(-1, 0);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return new Vector2(0, 1);
            }
            else
            {
                return new Vector2(0, -1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool nowFleeing = false;
        bool nowHostile = false;
        nearbyEntities = GetGameObjectsWithin(awarenessRadius);
        for (int i = 0; i < nearbyEntities.GetLength(0); i++)
        {
            current = nearbyEntities[i];
            if (current.tag == "Alive")
            {
                string expected = CheckExpectedBehavior(current);
                if (Equals(expected, "Hostile"))
                {
                    firstHostile = current.transform.position;
                    nowHostile = true;
                }
                else if (Equals(expected, "Flee"))
                {
                    firstFleeFrom = current.transform.position;
                    nowFleeing = true;
                }
            }
        }
        if (nowFleeing)
        {
            behavior = "Flee";
        }
        else if (nowHostile)
        {
            behavior = "Hostile";
        }
        else
        {
            behavior = "Idle";
        }
        Act();

    }

    string CheckExpectedBehavior(GameObject livingEntity)
    {
        if (power >= livingEntity.GetComponent<BehaviorAI>().power)
        {
            if (CanEat(gameObject, livingEntity))
            {
                return "Hostile";
            }
        }
        else if (CanEat(livingEntity, gameObject))
        {
            return "Flee";
        }
        else
        {
            return "Idle";
        }
        return null;
    }

    bool CanEat(GameObject attackingCreature, GameObject other)
    {
        string[] types = attackingCreature.GetComponent<BehaviorAI>().typesOfFood;
        for (int i = 0; i < types.GetLength(0); i++)
        {
            if (Equals(types[i], other.GetComponent<BehaviorAI>().typeOfCreature))
            {
                return true;
            }
        }
        return false;
    }

    void Act()
    {
        timeSinceLastPathing += Time.deltaTime;
        float resetTime = 1;
        if (Equals(behavior, "Idle"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetIdlePath();
            firstFleeFrom = transform.position;
            firstHostile = transform.position;

        }
        else if (Equals(behavior, "Hostile"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetPathTowards(firstHostile);


            firstFleeFrom = transform.position;

            /*Attack skills = GetComponent<Attack>();
            if(!Equals(skills, null) && skills.canAttack) 
            {    
                Vector3 unit = GetPathTowards(firstHostile);
                skills.Attack1(unit.x, unit.y);
            }*/
            
        }
        else if (Equals(behavior, "Flee"))
        {
            if (timeSinceLastPathing > resetTime)
                direction = GetFleePath(firstFleeFrom);
            firstHostile = transform.position;
        }
        gameObject.transform.Translate(direction * speed * Time.deltaTime);
        if (timeSinceLastPathing > 1)
        {
            timeSinceLastPathing = 0;
        }
    }

    Vector3 GetUnitVector(Vector3 start, Vector3 end)
    {
        float deltaX = end.x - start.x;
        float deltaY = end.y - start.y;
        float magnitude = Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2));
        if(magnitude == 0)
        {
            return new Vector3(0, 0);
        }
        return new Vector3(deltaX / magnitude, deltaY / magnitude);
    }

    Vector3 GetIdlePath()
    {
        float xRandom = Random.Range(.00001f, 1);
        float yRandom = Random.Range(.00001f, 1);
        float angle = Mathf.Atan(xRandom / yRandom);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        if (Random.value < .5)
        {
            x *= -1;
        }
        if (Random.value < .5)
        {
            y *= -1;
        }
        Debug.Log("I am currently Idle");
        return new Vector3(x, y);
        
    }

    Vector3 GetPathTowards(Vector2 target)
    {
        float angle = Mathf.Atan(Mathf.Abs(target.y - gameObject.transform.position.y) /
                Mathf.Abs(target.x - gameObject.transform.position.x));
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        return new Vector3(x, y);
    }

    Vector3 GetFleePath(Vector2 target)
    {
        Vector3 temp = GetPathTowards(target);
        Debug.Log("I am currently Fleeing");
        return new Vector3(-temp.x, -temp.y);
    }

    GameObject[] GetGameObjectsWithin(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y), radius);
        GameObject[] entitiesWithin = new GameObject[colliders.GetLength(0)];
        for (int i = 0; i < colliders.GetLength(0); i++)
        {
            entitiesWithin[i] = colliders[i].gameObject;
        }
        return entitiesWithin;
    }
}
