using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class FloatingJoystickS : MonoBehaviour
{
    [HideInInspector]
    public RectTransform RectTransform;
    public RectTransform Knob;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}