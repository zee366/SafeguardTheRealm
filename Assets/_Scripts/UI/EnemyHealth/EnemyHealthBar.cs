using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private float positionOffset;

    private Enemy health;

    public void SetHealth(Enemy health)
    {
        this.health = health;
        health.OnHealthChangedPercentage += TakeCareOfHealthChange;
    }

    private void TakeCareOfHealthChange(float percentage)
    {
        StartCoroutine(ChangeToPercentage(percentage));
    }

    private IEnumerator ChangeToPercentage(float percentage)
    {
        float initialChangePercentage = foregroundImage.fillAmount;
        float elapsedTime = 0.0f;

        while (elapsedTime < updateSpeedSeconds)
        {
            elapsedTime += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(initialChangePercentage, percentage, elapsedTime / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = percentage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(health.transform.position + Vector3.up * positionOffset);
    }

    private void OnDestroy()
    {
        health.OnHealthChangedPercentage -= TakeCareOfHealthChange;
    }
}
