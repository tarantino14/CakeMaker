using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChipkneWaleCheezein : SelecatableCheezein
{
    // Public //
    [Header("Chipkne Wale Cheezein")]
    public Collider m_collider;
    public float m_radius = 0.1f;

    public UnityEvent m_celebrationEvents;
    // Protected //
    // Private //
    // Access //

    public override void ActivateCelebration()
    {
        base.ActivateCelebration();
        m_celebrationEvents.Invoke();
    }

}
