using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float jumpForce = 30f;
        [SerializeField] float forwardThrust = 30f;
        [SerializeField] float lateralThrust = 30f;

        Rigidbody playerRigidbody;
        bool onTheGround = true;

        private void Start() 
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Input.GetAxis("Jump") == 1 && onTheGround)
            {
                onTheGround = false;
                playerRigidbody.AddForce(Vector3.up * jumpForce);
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                playerRigidbody.AddForce(Vector3.forward * forwardThrust * Input.GetAxis("Vertical"));
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                playerRigidbody.AddForce(Vector3.right * lateralThrust * Input.GetAxis("Horizontal"));
            }
        }

        private void OnCollisionEnter(Collision other) 
        {
            if (other.gameObject.tag == "Terrain")
            {
                onTheGround = true;
            }
        }
    }
}