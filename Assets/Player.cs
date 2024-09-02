using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 25f;
    float jumpForce = 50f;
    float sensitivity = 2f;
    CharacterController characterController;
    Rigidbody rigidbody;

    [SerializeField]
    Transform light;

    public bool win;

    [SerializeField]
    GameObject winUI;

    // Start is called before the first frame update
    void Start()
    {
     characterController = GetComponent<CharacterController>();   
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
            return;
        Move();
        Rotate();
    }

    private void Move()
    {
        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player's body based on the mouse movement (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Calculate vertical rotation (clamping to limit the range of vertical look)
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera (or the player's head, depending on your setup) vertically
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // If you want to rotate the entire player GameObject (including the body and camera), use the following line instead:
        // transform.Rotate(Vector3.up * mouseX);

        // Read WASD input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on player's local forward direction
        Vector3 movementDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
        movementDirection.y = 0f; // Ensure no vertical movement

        // Normalize the movement direction to prevent faster diagonal movement
        movementDirection.Normalize();

        // Move the player using CharacterController to handle collisions
        characterController.Move(movementDirection * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
        }

    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        // Rotate the player's body based on the mouse movement (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);
        light.transform.Rotate(Vector3.up * mouseX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Diamond"))
        {
            Cursor.lockState = CursorLockMode.None;
            winUI.SetActive(true);
        }
    }
}
