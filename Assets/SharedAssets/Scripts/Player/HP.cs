using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour
{
    public int HP_Amount;
    public int HP_Max = 10000;

    [SerializeField] bool isPlayer;
    [SerializeField] bool canDebug;
    [SerializeField] UnityEvent OnPlayerDeath;
    [SerializeField] UnityEvent OnTakeDamage;

    bool canHealNow;
    [SerializeField] int HealPercent = 5;

    //Debug
    [SerializeField] TextMeshProUGUI DebugHPAmount;

    [SerializeField] UnityEvent OnLowHP;
    [SerializeField] int LowHpStartPercent;

    public void Awake()
    {
        StartCoroutine(Heal());
        UpdateHP();
    }

    public IEnumerator Heal()
    {
        while (true)
        {
            if (canHealNow)
            {
                yield return new WaitForSeconds(3);
                HP_Amount += HP_Max * HealPercent / 100;
                if (HP_Amount >= HP_Max)
                {
                    HP_Amount = HP_Max;
                }
                UpdateHP();
            }
            else
            {
                yield return new WaitForSeconds(10);
                canHealNow = true;
            }
        }
    }

    public void UpdateHP()
    {
        if (HP_Amount <= 0)
        {
            if (!isPlayer)
            {
                Destroy(gameObject);
            }
            else
            {
                OnPlayerDeath.Invoke();
            }
        }
        else if (canDebug)
        {
            DebugHPAmount.text = HP_Amount.ToString();
        }

        if (HP_Amount < LowHpStartPercent / 100 * 500)
        {
            OnLowHP.Invoke();
        }
    }

    public void TakeDamage(int Damage)
    {
        HP_Amount -= Damage;
        OnTakeDamage.Invoke();
        UpdateHP();
        canHealNow = false;
    }
}
