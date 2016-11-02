using UnityEngine;
using System.Collections;

public class DisplayAndRemove : MonoBehaviour {

    float time = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 3)
        {
            Destroy(gameObject);
        }
	}
}
