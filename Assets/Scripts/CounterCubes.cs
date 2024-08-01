public class CounterCubes : Counter<Cube>
{
    protected override void DrawText(int createdCount, int activeCount)
    {
        text.text = $"���������� ��������� ����� �� �� �����: {createdCount}\n���������� �������� ����� �� �����: {activeCount}";
    }
}
