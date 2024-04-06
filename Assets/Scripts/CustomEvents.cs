using System;

public class CustomEvents
{
    public static Action<bool> OnToggleDamageCollider;
    public static void FireToggleDamageCollider(bool state)
    {
        OnToggleDamageCollider?.Invoke(state);
    }

    public static Action<int> OnAddScore;
    public static void FireAddScore(int number)
    {
        OnAddScore?.Invoke(number);
    }
}
