using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] UnityEvent OnShoot;

    [SerializeField] private int weaponDamage = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem ShotParticle;
    [SerializeField] GameObject Light;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, Mathf.Infinity, targetLayer))
            ApplyDamage(hit.collider.gameObject);
        animator.SetTrigger("Shot");
        ShotParticle.Play();
        Light.SetActive(true);
        StartCoroutine(SwitckOffLight());
        OnShoot.Invoke();
    }

    public IEnumerator SwitckOffLight()
    {
        yield return new WaitForSeconds(.1f);
        Light.SetActive(false);
    }

    private void ApplyDamage(GameObject hitObject)
    {
        ZombieAi zombieAi = hitObject.GetComponent<ZombieAi>();
        if (zombieAi == null) return;

        HP hp = hitObject.GetComponent<HP>();
        hp?.TakeDamage(weaponDamage);
    }
}
