using UnityEngine;

// By Claude. I understand what it does.
public static class MenuTransitionLock
{
    private static float s_unlockTime;
    public static bool IsLocked => Time.realtimeSinceStartup < s_unlockTime;
    public static void LockFor(float seconds) => s_unlockTime = Time.realtimeSinceStartup + seconds;
}
//