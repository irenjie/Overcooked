using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject {
        // ���� KitchenObject �����ڲ����ϵ� visual �Ĺ�ϵ
        public KitchenObjectSO kitchenObjectSo;
        public GameObject gameObject;

    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectsSO_GameObjectList;


    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectsSO_GameObjectList) {
                kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        // �������ʳ��ʱ����
        // չʾ�����ϵ�ʳ��ͼ��
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectsSO_GameObjectList) {
            if(kitchenObjectSO_GameObject.kitchenObjectSo == e.KitchenObjectSO) {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
