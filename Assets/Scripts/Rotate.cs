using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] Vector3 _direction;

	private Transform _transform;

	private void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	private void Update()
	{
		_transform.Rotate( _direction * Time.deltaTime );
	}
}
