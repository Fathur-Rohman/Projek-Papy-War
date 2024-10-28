using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kecepatan gerak tank
    public float rotationSpeed = 100f;  // Kecepatan rotasi tank
    public Rigidbody2D rb;  // Rigidbody dari tank

    void Start()
    {
        // Memastikan Rigidbody2D diambil dari objek tank
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ambil input dari keyboard untuk pergerakan
        float moveDirection = Input.GetAxis("Vertical");  // W dan S untuk maju/mundur
        float rotateDirection = Input.GetAxis("Horizontal");  // A dan D untuk rotasi kiri/kanan

        // Menggerakkan tank maju dan mundur
        rb.velocity = transform.up * moveDirection * moveSpeed;

        // Memutar tank
        rb.angularVelocity = -rotateDirection * rotationSpeed;
    }
}
