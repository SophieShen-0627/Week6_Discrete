using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float PlayerMovingSpeed = 3f;
    public Vector3 Target;
    public bool IsMoving = false;

    [SerializeField] ParticleSystem StartParticle;
    [SerializeField] AudioClip moveSound;
    private AudioSource audioSource;

    private void Start()
    {
        PlayerMovingSpeed = DataManager.datas.PlayerMovingSpeed;
        Target = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position != Target && !IsMoving)
        {
            StartCoroutine(MoveTowardsTarget(Target));
            IsMoving = true;
            StartParticle.Play();
            PlayMoveSound();
        }
    }

    IEnumerator MoveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) >= 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, PlayerMovingSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        IsMoving = false;
    }

    void PlayMoveSound()
    {
        if (moveSound != null && audioSource != null)
        {
            audioSource.clip = moveSound;
            audioSource.Play();
        }
    }
}
