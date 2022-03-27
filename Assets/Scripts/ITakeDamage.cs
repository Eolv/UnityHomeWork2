using UnityEngine;

namespace HomeWork2
{
    public interface ITakeDamage
    {
        public void Hit(float damage);
    }

    public interface IHealable
    {
        public void Heal();
    }

    public interface IInterractWithButton
    {
        public void DoOnButtonPressedActions();
    } 
}




