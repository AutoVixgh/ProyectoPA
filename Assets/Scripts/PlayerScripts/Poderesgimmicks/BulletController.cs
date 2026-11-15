using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;
    private float direction = 1f;

    public void SetDirection(float dir)
    {
        direction = dir;
        if (dir < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca con un objeto frágil
        if (collision.CompareTag("Fragil"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }

        // Si choca con un enemigo
        if (collision.CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }

        // Si choca con un Boss
        if (collision.CompareTag("Boss"))
        {
            // collision.GetComponent<BossController>()?.RecibirDanio(damage);
            Destroy(gameObject);
            return;
        }

        // Si choca con el suelo (Capa Ground)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}