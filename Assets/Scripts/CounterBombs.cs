public class CounterBombs : Counter<Bomb>
{
    protected override void DrawText(int createdCount, int activeCount)
    {
        text.text = $"���������� ��������� ���� �� �� �����: {createdCount}\n���������� �������� ���� �� �����: {activeCount}";
    }
}
