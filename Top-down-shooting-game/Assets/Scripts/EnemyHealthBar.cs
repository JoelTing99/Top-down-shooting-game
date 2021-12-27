using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private float TimerMax = 0.5f;
    private Image Bar;
    private Image DamageBar;
    private float Timer;
    private HealthSystem HealthSystem;

    private void Start()
    {
        Bar = transform.Find("Bar").GetComponent<Image>();
        DamageBar = transform.Find("DamageBar").GetComponent<Image>();
        DamageBar.fillAmount = Bar.fillAmount;
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

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        Timer = TimerMax;
        SetHealth(HealthSystem.GetHealthPercent());
    }

    private void SetHealth(float HealthPercentage)
    {
        Bar.fillAmount = HealthPercentage;
    }
    public void SetHealthSystem(HealthSystem HealthSystem)
    {
        this.HealthSystem = HealthSystem;
        this.HealthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
}
