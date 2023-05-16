using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health Hearts;
    [SerializeField] private Image fullHeartBar;
    [SerializeField] private Image currHeartBar;

    private void Start()
    {
        fullHeartBar.fillAmount = Hearts.currHearts / 10;
    }
    private void Update()
    {
        currHeartBar.fillAmount = Hearts.currHearts / 10;
    }
}
