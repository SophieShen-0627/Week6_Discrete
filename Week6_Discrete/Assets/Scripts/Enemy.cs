using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject DashPoint;
    [SerializeField] bool CarryDashPoint = false;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] AudioClip deathSoundClip; // Serialized field for the death sound clip

    private float HitStopTime = 0.1f;
    private float MovingSpeed = 5f;

    private float DistanceToTarget = 0;
    private Rigidbody2D rb;
    private EnemySpawner spawner;
    private AudioSource audioSource; // Audio source for death sound

    // Start is called before the first frame update
    void Start()
    {
        HitStopTime = DataManager.datas.HitStopTime;
        MovingSpeed = DataManager.datas.MovingSpeed;
        GetComponent<Collider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource component
    }

    private void OnEnable()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        Vector3 targetPos = DetectNearestDashPoint().position;
        MoveTowardsDashPoints(targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            StartCoroutine(DoHitStop());
            DoDeath();
        }
    }


    private void MoveTowardsDashPoints(Vector3 targetPos)
    {
        rb.velocity = (targetPos - transform.position).normalized * MovingSpeed;

        if (rb.velocity != Vector2.zero) transform.right = rb.velocity.normalized;

        if (Vector2.Distance(targetPos, transform.position) < 0.05) transform.position = targetPos;
    }

    private Transform DetectNearestDashPoint()
    {

        DashPoint[] points = FindObjectsOfType<DashPoint>();

        DistanceToTarget = Vector2.Distance(points[0].transform.position, transform.position);
        Transform target = points[0].transform;

        foreach (var point in points)
        {
            if (Vector2.Distance(point.transform.position, transform.position) <= DistanceToTarget)
            {
                DistanceToTarget = Vector2.Distance(point.transform.position, transform.position);
                target = point.transform;
            }
        }

        return target;
    }
    private void DoDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;

        FindObjectOfType<CameraShake>().StartShake(0.1f, 1f, 1f);
        DeathParticle.Play();

        // Play death sound
        if (deathSoundClip != null && audioSource != null)
        {
            audioSource.clip = deathSoundClip;
            audioSource.Play();
        }

        if (CarryDashPoint) Instantiate(DashPoint, transform.position, Quaternion.identity);
        spawner.OneEnemyDie = true;
        spawner.CurrentEnemies.Remove(this.gameObject);
    }
    IEnumerator DoHitStop()
    {
        Time.timeScale = DataManager.datas.HitStopTimeScale;
        yield return new WaitForSecondsRealtime(HitStopTime);

        Time.timeScale = 1;
    }
}
