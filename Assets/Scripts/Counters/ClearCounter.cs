using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        // ��ҽ����ϵ���Ʒ����clearcounter�ϣ����ߴӸù�̨�����߶���
        if (!HasKitchenObject()) {
            // �ù�̨��û�ж���
            if (player.HasKitchenObject()) {
                // ��������ж���
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // ��̨���ж���
            if (!player.HasKitchenObject()) {
                // �������û����
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                // ��������ж���
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // ������ϵĶ���������
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // ������ϵĶ�����������
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter ��������
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
