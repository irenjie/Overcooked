using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public static event EventHandler OnAnyCut;

    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public override void Interact(Player player) {
        // ��ҽ����ϵ���Ʒ����counter�ϣ����ߴӸ�counter�����߶���
        if (!HasKitchenObject()) {
            // �ù�̨��û�ж���
            if (player.HasKitchenObject()) {
                // ��������ж���
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // �������Ķ���������,����ڸ�counter��
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        // ����KOSO�Ƿ���cuttingRecipeSOArray��һԱ��input
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    public override void InteractAlternate(Player player) {
        // �в˰����ж��������ҿ�����(��ֹ�й��Ķ�������)������. ɾ����ǰ��Ȼ������Slices
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
            }
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
