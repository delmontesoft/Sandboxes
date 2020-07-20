using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float jumpForce = 30f;
        [SerializeField] float thrust = 30f;
        [SerializeField] float turnSpeed = 2f;

        Rigidbody playerRigidbody;
        bool onTheGround = true;

        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            if (inputX != 0 || inputZ != 0)
            {
                playerRigidbody.AddForce(transform.forward * thrust);

                Vector3 lookDirection = new Vector3(inputX, 0, inputZ);
                Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                float step = turnSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(lookRotation, Quaternion.identity, step);
            }

            if (Input.GetAxis("Jump") == 1 && onTheGround)
            {
                onTheGround = false;
                playerRigidbody.AddForce(Vector3.up * jumpForce);
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