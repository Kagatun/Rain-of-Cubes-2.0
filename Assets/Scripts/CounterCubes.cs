public class CounterCubes : Counter<Cube>
{
    protected override void DrawText(int createdCount, int activeCount)
    {
        text.text = $"Количество созданных кубов за всё время: {createdCount}\nКоличество активных кубов на сцене: {activeCount}";
    }
}
