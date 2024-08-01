public class CounterBombs : Counter<Bomb>
{
    protected override void DrawText(int createdCount, int activeCount)
    {
        text.text = $"Количество созданных бомб за всё время: {createdCount}\nКоличество активных бомб на сцене: {activeCount}";
    }
}
