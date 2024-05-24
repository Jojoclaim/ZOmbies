using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    private Transform Player;
    [SerializeField] NavMeshAgent Zombie;

    [SerializeField] int Damage;
    [SerializeField] float AttackDelay;
    [SerializeField] float AttackRange;

    [SerializeField] Animator animator;
    [SerializeField] string AttackAnimationName;

    HP PlayerHP;

    bool canAttack;

    public void Awake()
    {
        Player = FindObjectOfType<CharacterController>().transform;
        PlayerHP = Player.gameObject.GetComponent<HP>();
        StartCoroutine(Attack());
    }

    public void FixedUpdate()
    {
        Zombie.SetDestination(Player.position);
        if (Vector3.Distance(Player.position, Zombie.transform.position) <= AttackRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    public IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackDelay);
            if (canAttack)
            {
                PlayerHP.TakeDamage(Damage);
                animator.SetTrigger(AttackAnimationName);
            }
        }
    }

}
