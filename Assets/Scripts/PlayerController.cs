using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float Speed = 8;
    //public float Acceleration = 0.1f;
    public float distance = 3f;

    private float _distanceSquared;
    private float _speed = 0;
    private bool _collectiongPower = false;
    private bool _launched = false;
    private Vector2 _direction;
    Rigidbody2D rb;
    private LineRenderer lr;

    void Start()
    {
        _distanceSquared = distance * distance;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesHitTriggers = true;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (_collectiongPower)
        {
            Dragging();
        }
    }
	
	void FixedUpdate () {
        if (_launched)
        {
            //if (Mathf.Abs(_speed) < Mathf.Abs(Speed))
            //{
            //    _speed -= Speed / Acceleration;
            //}
            //else if (Mathf.Abs(_speed) < 10) // Mathf.Abs(Speed))
            //{
            //    _speed = 10;// Speed;
            //}
            //if (_direction.y > -1) _direction.y -= 1/Acceleration;
            //else _direction.y = -1;
            //if (Mathf.Abs(_direction.x) > 0.2) _direction.x += -1 * Mathf.Sign(_direction.x) * (1 / (Acceleration));
            //else _direction.x = Mathf.Sign(_direction.x) * 0.2f;
            //Debug.Log(_direction);

            _direction = _direction + Physics2D.gravity/3;// * rb.mass;


            rb.velocity = _direction;
        }
    }

    public void OnMouseDown()
    {
        //Debug.Log("Launched: " + _launched);
        StartLaunch();
    }
    public void StartLaunch()
    {
        if (!_launched)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position);
            lr.enabled = true;
            _collectiongPower = true;
        }
    }

    void OnMouseUp()
    {
        Launch();
    }

    public void Launch()
    {
        if (_collectiongPower)
        {
            _collectiongPower = false;
            rb.constraints = RigidbodyConstraints2D.None;
            //_direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (Mathf.Abs(_direction.x) > 1) _direction.x = 1 * Mathf.Sign(_direction.x);
            //if (Mathf.Abs(_direction.y) > 1) _direction.y = 1 * Mathf.Sign(_direction.y);
            //Debug.Log("Direction: " + _direction);
            _launched = true;
            _direction *= Speed;
            lr.enabled = false;
        }
    }

    void Dragging()
    {
        Ray ray = new Ray(transform.position, Vector3.zero);
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        var playerToMouse = mouseWorldPosition - transform.position;
 
        if (playerToMouse.sqrMagnitude > _distanceSquared)
        {
            ray.direction = playerToMouse;
            mouseWorldPosition = ray.GetPoint(distance);
        }

        //mouseWorldPosition.z = 0;
        Debug.Log("mouse: " + (playerToMouse));
        Debug.Log("mouse world: " + mouseWorldPosition);

        Debug.Log("distance: " + distance);
        Debug.Log("Player pos: " + transform.position);

        _direction = transform.position - mouseWorldPosition;
        lr.SetPosition(1, mouseWorldPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log ("On trigger enter");
        rb.velocity = Vector3.zero;
		Debug.Log ("Tag: " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Wall"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _launched = false;
            _speed = Speed;
			Debug.Log ("On trigger enter hit wall");
        }
    }
}
