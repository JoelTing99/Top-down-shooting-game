using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    private float TimerMax = 1f;
    [SerializeField] private Image Bar;
    [SerializeField] private Image DamageBar;
    private float Timer;
    private HealthSystem HealthSystem;

    private void Awake()
    {
    }
    private void Start()
    {
        SetHealth(HealthSystem.GetHealthPercent());
        DamageBar.fillAmount = Bar.fillAmount;
        HealthSystem.OnDamaged += HealthSystem_OnDamaged;
        HealthSystem.OnHealed += HealthSystem_OnHealed;
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0 && Bar.fillAmount <= DamageBar.fillAmount)
        {
            DamageBar.fillAmount -= 0.3f * Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
    }
    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(HealthSystem.GetHealthPercent());
    }
    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        Timer = TimerMax;
        SetHealth(HealthSystem.GetHealthPercent());
    }

    private void SetHealth(float HealthPercentage)
    {
        Bar.fillAmount = HealthPercentage;
        if(Bar.fillAmount >= 0.5f)
        {
            Bar.color = new Color(0, 1, 0);
        }
        else if(Bar.fillAmount <= 0.5f && Bar.fillAmount >= 0.2f)
        {
            Bar.color = new Color(1, 1, 0);
        }
        else
        {
            Bar.color = new Color(1, 0, 0);
        }
    }
    public void SetHealthSystem(HealthSystem HealthSystem)
    {
        this.HealthSystem = HealthSystem;
    }

}
