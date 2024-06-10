using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float moveSpeed = 5f;
    private Rigidbody characterRigidbody;

    public float jumpSpeed = 10f;
    public float gravity = -20f;
    public float yVelocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputZ);
        velocity *= moveSpeed;

        velocity = cameraTransform.TransformDirection(velocity);
        velocity *= moveSpeed;

        if(characterController.isGrounded)
        {
            yVelocity = 0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += (gravity * Time.deltaTime);

        velocity.y = yVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }
}
