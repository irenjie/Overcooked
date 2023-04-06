using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        // 玩家将手上的物品放在clearcounter上，或者从该柜台上拿走东西
        if (!HasKitchenObject()) {
            // 该柜台上没有东西
            if (player.HasKitchenObject()) {
                // 玩家手上有东西
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // 柜台上有东西
            if (!player.HasKitchenObject()) {
                // 玩家手上没东西
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                // 玩家手上有东西
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // 玩家手上的东西是盘子
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // 玩家手上的东西不是盘子
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter 上是盘子
                        KitchenObject kitchenObject = player.GetKitchenObject();
                        if (plateKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO())) {
                            kitchenObject?.DestroySelf();
                        }
                    }
                }
            }
        }

    }



}
