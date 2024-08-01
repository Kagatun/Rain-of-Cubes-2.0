using TMPro;
using UnityEngine;

public abstract class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected Spawner<T> spawner;

    private void Start()
    {
        spawner.OnSpawn += DrawText;
    }

    protected virtual void DrawText(int createdCount, int activeCount)
    {
        text.text = $"Количество созданных объектов за всё время: {createdCount}\nКоличество активных объектов на сцене: {activeCount}";
    }
}
