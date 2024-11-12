using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Rigidbody2D rb;
    private Alteruna.Avatar _avatar;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if  (!_avatar.IsMe)
            return;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if  (!_avatar.IsMe)
            return;

        float moveDirection = Input.GetAxis("Vertical");
        float rotateDirection = Input.GetAxis("Horizontal");

        rb.velocity = transform.up * moveDirection * moveSpeed;

        rb.angularVelocity = -rotateDirection * rotationSpeed;
    }
}
