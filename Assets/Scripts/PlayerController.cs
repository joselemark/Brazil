using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedFactor = 0.1f;

    private CharacterController characterController;
    private Vector3 movementDirection = new Vector3();


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speedFactor ;
    }

    private void FixedUpdate()
    {
        characterController.Move(movementDirection);
    }
}
