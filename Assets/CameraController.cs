using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject _player;
	public EdgeCollider2D _cameraBounds;
	private double _xLeft, _xRight, _yDown,_yUp; //bounds of a map
	private double _minX,_maxX,_minY,_maxY; //bounds of camera
	private Bounds _bounds;
	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public Vector3 offset;
	Vector3 targetPos;
	// Use this for initialization

	void Awake() {


		_player = GameObject.FindGameObjectWithTag ("Player");
	//	_cameraBounds = GetComponentInChildren<EdgeCollider2D> ();
		SetMapBounds ();
		SetCameraBounds ();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void FixedUpdate () {
		if (_player)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = _player.transform.position.z;
			
			Vector3 targetDirection = (_player.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 10f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			if (targetPos.x + _bounds.extents.x > _xRight) {
				targetPos = new Vector3((float)_xRight-_bounds.extents.x,targetPos.y,targetPos.z);
			}
			if ((targetPos.x-_bounds.extents.x < _xLeft)){
				targetPos = new Vector3((float)_xLeft+_bounds.extents.x,targetPos.y,targetPos.z);
			}
			if ((targetPos.y+_bounds.extents.y > _yUp)){
				targetPos = new Vector3(targetPos.x,(float)_yUp-_bounds.extents.y,targetPos.z);
			}
			if ((targetPos.y-_bounds.extents.y < _yDown)){
				targetPos = new Vector3(targetPos.x,(float)_yDown+_bounds.extents.y,targetPos.z);
			}
			
			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.4f);
			
		}
	}


	void SetMapBounds() {

		_xLeft = _cameraBounds.bounds.center.x - _cameraBounds.bounds.extents.x;
		_xRight = _cameraBounds.bounds.center.x + _cameraBounds.bounds.extents.x;
		_yUp= _cameraBounds.bounds.center.y + _cameraBounds.bounds.extents.y;
		_yDown = _cameraBounds.bounds.center.y - _cameraBounds.bounds.extents.y ;
		//Debug.Log ("Left: " + _xLeft + " Right: " + _xRight + " Up: " + _yUp + " Down: " + _yDown);

	}

	void SetCameraBounds() {

		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.orthographicSize * 2;
		_bounds = new Bounds(
			GetComponent<Camera>().transform.position,
			new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

		_minX = _bounds.center.x - _bounds.extents.x;
		_maxX = _bounds.center.x + _bounds.extents.x;
		_maxY= _bounds.center.y + _bounds.extents.y;
		_minY = _bounds.center.y - _bounds.extents.y;

		//Debug.Log ("C_Left: " + _minX + "C_Right: " + _maxX + "C_Up: " + _maxY + "C_Down: " + _minY);

	}


	void UpdateCameraPosition() {



	}

}
