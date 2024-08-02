using TMPro;
using UnityEngine;

public abstract class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Spawner<T> spawner;
    [SerializeField] private string _name;

    private void Start()
    {
        spawner.Spawned += DrawText;
    }

    private void DrawText(int createdCount, int activeCount)
    {
        text.text = $"Количество созданных {_name} за всё время: {createdCount}\nКоличество активных {_name} на сцене: {activeCount}";
    }
}
