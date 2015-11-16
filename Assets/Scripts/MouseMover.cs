using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour {
    Rigidbody2D rb;
    public float Speed = 8;
    public float Acceleration = 0.1f;

    private float _speed = 0;
    public float VSpeed = 0.5f;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //foreach (var childRB in GetComponentsInChildren<Rigidbody2D>())
        //{
        //    childRB.velocity = new Vector3(Speed / Acceleration, 0, 0);
        //}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Mathf.Abs(_speed)<Mathf.Abs(Speed))
        {
            _speed += Speed/Acceleration;
        }else if(Mathf.Abs(_speed)> Mathf.Abs(Speed))
        {
            _speed = Speed;
        }

        rb.velocity = new Vector3(_speed, 0, 0);
    }

    //public void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "wall")
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y - VSpeed, transform.position.z);
    //    }
    //}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "wall")
        {
            //Debug.Log("HIT");
            rb.gravityScale = VSpeed;
            //Speed = 0;
            //_speed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //_speed = 0;
            //Speed = -Speed;
            //Acceleration = Acceleration;
        }
    }
}
