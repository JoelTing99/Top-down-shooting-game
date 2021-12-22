using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HealthBar : MonoBehaviour
{
    private Image Bar;
    private HealthSystem HealthSystem;

    private void Start()
    {
        Bar = transform.Find("Bar").GetComponent<Image>();
        HealthSystem.OnDamaged += HealthSystem_OnDamaged;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SetHealth(HealthSystem.GetHealthPercent());
    }

    private void SetHealth(float HealthPercentage)
    {
        Bar.fillAmount = HealthPercentage;
    }
    public void SetHealthSystem(HealthSystem HealthSystem)
    {
        this.HealthSystem = HealthSystem;
    }
}
