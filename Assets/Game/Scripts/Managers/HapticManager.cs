using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticManager : MonoBehaviour
{
    public static HapticManager instance { get; set; }

    private void Awake()
    {
        Instance();
    }
    private void Instance()
    {
        if (instance != null) return;
        instance = this;
    }
    public void SoftHaptic()
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
    }
    public void HeavyHaptic()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }
    public void SelectHaptic()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
    }
    public void WarningHaptic()
    {
        MMVibrationManager.Haptic(HapticTypes.Warning);
    }
}

