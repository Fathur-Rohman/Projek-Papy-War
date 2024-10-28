using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kecepatan gerak tank
    public float rotationSpeed = 100f;  // Kecepatan rotasi tank
    public Rigidbody2D rb;  // Rigidbody dari tank
    private Alteruna.Avatar _avatar;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if  (_avatar.IsMe)
            return;
        // Memastikan Rigidbody2D diambil dari objek tank
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if  (_avatar.IsMe)
            return;
        // Ambil input dari keyboard untuk pergerakan
        float moveDirection = Input.GetAxis("Vertical");  // W dan S untuk maju/mundur
        float rotateDirection = Input.GetAxis("Horizontal");  // A dan D untuk rotasi kiri/kanan

        // Menggerakkan tank maju dan mundur
        rb.velocity = transform.up * moveDirection * moveSpeed;

        // Memutar tank
        rb.angularVelocity = -rotateDirection * rotationSpeed;
    }
}
