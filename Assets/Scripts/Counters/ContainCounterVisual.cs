using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainCounter containCounter;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containCounter.OnPlayerGrabbedObject += ContainCounter_OnPlayerGrabbedObject;
        
    }

    private void ContainCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
