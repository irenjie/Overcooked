using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent IKitchenObjectParent) {
        // 告诉原parent自己要走了，并告诉新parent自己要来了
        if(this.KitchenObjectParent !=null) {
            this.KitchenObjectParent.clearKitchenObject();
        }

        if (IKitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent alreadly has a KitchenObject");
        }

        this.KitchenObjectParent = IKitchenObjectParent;
        IKitchenObjectParent.SetKitchenObject(this);

        // 移动食物模型
        transform.parent = IKitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    
    public IKitchenObjectParent GetKitchenObjectParent() {
        return KitchenObjectParent;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if(this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }

    public void DestroySelf() {
        KitchenObjectParent.clearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        

        return kitchenObject;
    }

}
