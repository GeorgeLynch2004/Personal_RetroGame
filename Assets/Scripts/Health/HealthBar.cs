using System;
using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthBar : MonoBehaviour
    {
        private HealthComponent healthComponent;
        [SerializeField] private Transform currentBar;
        [SerializeField] private Transform capacityBar;
        [SerializeField] private float visiblityDuration;
        private MeshRenderer currentRenderer;
        private MeshRenderer capacityRenderer;
        private Coroutine hideCoroutine;
        
        
        public void Initialise(HealthComponent health)
        {
            healthComponent = health;
            currentRenderer = currentBar.gameObject.GetComponentInChildren<MeshRenderer>();
            capacityRenderer = capacityBar.gameObject.GetComponentInChildren<MeshRenderer>();
            BarVisible(false);
        }

        public void UpdateBar()
        {
            BarVisible(true);

            Vector3 currScale = currentBar.transform.localScale;
            currScale.x = healthComponent.getCurrentHealth() / healthComponent.getMaxHealth();
            currentBar.transform.localScale = currScale;

            // Reset the hide timer
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(visiblityDuration);
            BarVisible(false);
            hideCoroutine = null;
        }

        private void BarVisible(bool state)
        {
            currentRenderer.enabled = state;
            capacityRenderer.enabled = state;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}