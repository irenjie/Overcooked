using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectPlaceHere;

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public static void ResetStaticData() {
        OnAnyObjectPlaceHere = null;
    }


    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter::Interact");
    }
    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("BaseCounter::InteractAlternate");
    }


    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void clearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return (kitchenObject != null);
    }

}
