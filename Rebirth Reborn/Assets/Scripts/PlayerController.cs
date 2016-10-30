using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Sprite right;
    public Sprite left;
    public Sprite down;
    public Sprite up;

    Rigidbody2D rb;

    public float speed = 300.0f;
    public float strikeForce = 3500.0f;
    
    
    public int health = 80;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
        {
            // If the user has pressed either right or D, face right
            gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.A)))
        {
            // If the user has pressed either left or A, face left
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))
        {
            // If the user has pressed either up or W, face up
            gameObject.GetComponent<SpriteRenderer>().sprite = up;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S)))
        {
            // If the user has pressed either down or S, face down
            gameObject.GetComponent<SpriteRenderer>().sprite = down;
        }

        MoveHorizontal(Input.GetAxis("Horizontal"));
        MoveVertical(Input.GetAxis("Vertical"));

      

        if (Input.GetButtonDown("Jump") && gameObject.GetComponent<Attack>().canAttack)
        {
            gameObject.GetComponent<Attack>().Attack1(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")); // takes keyboard input and calls attack
        }
        

    }


    void MoveHorizontal(float input)
    {
        Vector2 moveVe1 = rb.velocity;
        moveVe1.x = input * speed * Time.deltaTime;
        rb.velocity = moveVe1;
    }

    void MoveVertical(float input)
    {
        Vector2 moveVe1 = rb.velocity;
        moveVe1.y = input * speed * Time.deltaTime;
        rb.velocity = moveVe1;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.GetComponent<Attack>().isAttacking)
        {
            if (coll.gameObject.GetComponent<TargetScript>().health <= 10)
            {
                health += 20;
            }
        }
    }
}
