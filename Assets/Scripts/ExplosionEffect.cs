using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public AudioClip explosionSound;

    void Start()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                float animationLength = clipInfo[0].clip.length;
                Destroy(gameObject, animationLength);
            }
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}