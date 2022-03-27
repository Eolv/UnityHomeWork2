using UnityEngine;

namespace HomeWork2
{
    public class HealPot : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealable target))
            {
                target.Heal();
                Destroy(gameObject);
            }
        }
    }
}


