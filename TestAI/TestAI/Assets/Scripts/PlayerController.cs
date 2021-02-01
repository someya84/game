using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 3f;
	float moveX = 0f;
	float moveZ = 0f;
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Debug.Log(GameManager.instance.getFruit);
	}

	void Update()
	{
		moveX = Input.GetAxis("Horizontal") * speed;
		moveZ = Input.GetAxis("Vertical") * speed;
		Vector3 direction = new Vector3(moveX, 0, moveZ);
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector3(moveX, 0, moveZ);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Fruit")
		{
			Debug.Log("Get");
			Destroy(other.gameObject);
			GameManager.instance.getFruit = true;
		}
	}
}
