using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int weaponDamage = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] Animator animator;

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
    }

    private void ApplyDamage(GameObject hitObject)
    {
        ZombieAi zombieAi = hitObject.GetComponent<ZombieAi>();
        if (zombieAi == null) return;

        HP hp = hitObject.GetComponent<HP>();
        hp?.TakeDamage(weaponDamage);
    }
}
