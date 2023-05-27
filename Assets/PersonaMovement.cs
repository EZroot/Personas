using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonaMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementForce;
    public float jumpForce;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;
    public LayerMask whatIsGround;

    [Header("Camera Follow")]
    public Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    public BoxCollider2D[] _collidersToIgnore;

    private Rigidbody2D rb;

    // void Awake()
    // {
    //     for (var i = 0; i < _collidersToIgnore.Length; i++)
    //     {
    //         for (var j = 0; j < _collidersToIgnore.Length; j++)
    //         {
    //             Physics2D.IgnoreCollision(_collidersToIgnore[i], _collidersToIgnore[j], true);
    //         }
    //     }
    // }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Jump();
        CameraFollow();
    }

    public void SetMovement(float xDir)
    {
        rb.velocity = new Vector2(xDir * (movementForce * Time.deltaTime), rb.velocity.y);
    }

    public void Jump(bool shouldJump)
    {
        if(!shouldJump)
            return;
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, whatIsGround);
        return hit.collider != null;
    }

    private void CameraFollow()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + offset, interpolation);
    }
}