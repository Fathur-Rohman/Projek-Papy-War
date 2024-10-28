using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f; // Kecepatan roket
    public float rotateSpeed = 200f; // Kecepatan rotasi roket
    public GameObject explosionPrefab; // Prefab untuk efek ledakan
    private Transform target;

    void Start()
    {
        // Cari target dengan tag "Enemy"
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        // Arahkan roket menuju target
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        // Rotasi roket
        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

        // Gerakkan roket maju ke arah target
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Tampilkan efek ledakan
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            
            // Hancurkan musuh
            Destroy(collision.gameObject); // Hancurkan objek musuh
            Destroy(gameObject); // Hancurkan roket
        }
    }
}
