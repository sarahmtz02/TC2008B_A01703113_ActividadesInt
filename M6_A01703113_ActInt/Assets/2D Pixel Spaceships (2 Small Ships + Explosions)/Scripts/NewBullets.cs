using UnityEngine;
using System.Collections;

public class BulletHellShooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public float shootIntervalCircle = 0.5f;  // Intervalo de disparo en círculo en segundos
    public float shootIntervalSpiral = 0.1f;  // Intervalo de disparo en espiral en segundos
    public float shootIntervalStar = 1f;  // Intervalo de disparo en estrella en segundos
    public int bulletsPerWave = 10;  // Número de balas por ola (para el círculo)
    public float bulletSpeed = 5f;  // Velocidad de las balas
    public float radius = 1f;  // Radio del círculo de disparo
    public float shootingDuration = 10f;  // Duración del disparo en cada patrón en segundos
    public int numberOfSpirals = 5;  // Número de espirales simultáneas

    private Coroutine shootingCoroutine;

    private void Start()
    {
        shootingCoroutine = StartCoroutine(ShootBulletsInCircle(shootingDuration));
    }

    private IEnumerator ShootBulletsInCircle(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            ShootCircle();
            yield return new WaitForSeconds(shootIntervalCircle);
        }

        shootingCoroutine = StartCoroutine(ShootBulletsInSpiral(shootingDuration));
    }

    private void ShootCircle()
    {
        float angleStep = 360f / bulletsPerWave;
        float angle = 0f;

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletDir.x, bulletDir.y) * bulletSpeed;

            angle += angleStep;
        }
    }

    private IEnumerator ShootBulletsInSpiral(float duration)
    {
        float endTime = Time.time + duration;
        float[] angles = new float[numberOfSpirals];
        float angleStep = 360f / numberOfSpirals;

        for (int i = 0; i < numberOfSpirals; i++)
        {
            angles[i] = i * angleStep;
        }

        while (Time.time < endTime)
        {
            for (int i = 0; i < numberOfSpirals; i++)
            {
                ShootSpiral(angles[i]);
                angles[i] += 10f;  // Incrementa el ángulo para crear el efecto de espiral
            }
            yield return new WaitForSeconds(shootIntervalSpiral);
        }

        shootingCoroutine = StartCoroutine(ShootBulletsInStar(shootingDuration));
    }

    private void ShootSpiral(float angle)
    {
        float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
        float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
        Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletDir.x, bulletDir.y) * bulletSpeed;
    }

    private IEnumerator ShootBulletsInStar(float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            ShootStar();
            yield return new WaitForSeconds(shootIntervalStar);
        }
    }

    private void ShootStar()
    {
        float angleStep = 360f / 5;  // Una estrella de 5 puntos
        float angle = 0f;

        for (int i = 0; i < 5; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletDir.x, bulletDir.y) * bulletSpeed;

            angle += angleStep;
            angle += angleStep;  // Salta un punto para crear la forma de estrella
        }
    }
}
