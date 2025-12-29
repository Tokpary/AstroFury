using DG.Tweening;
using UnityEngine;

namespace Patterns.ObjectPool.Components.MoneyObjectPool
{
    public class MoneyPoolManager : AObjectPoolManager<Money>
    {
        private void OnEnable()
        {
            EventManager.Instance.OnEnemyKilled += CreateMoney;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnEnemyKilled -= CreateMoney;
        }

        private void CreateMoney(Vector2 position)
        {
            int moneyAmount = Random.Range(1, 5);
            for (int i = 0; i < moneyAmount; i++)
            {
                Money money = CreatePoolElement(elementPrototype, initialNumberOfElements, allowAddNewElements);
                if (money)
                {
                    Vector2 randomPosition = new Vector2(position.x + Random.Range(-1f, 1f), position.y + Random.Range(-1f, 1f));
                    money.transform.localPosition = position;
                    money.transform.DOMove(randomPosition, 1f).SetEase(Ease.InOutQuad);
                }
            }
        }
    }
}