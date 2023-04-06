using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    // �����Ӳ������������֮ǰ��ÿ��һ��ʱ������һ�����ӣ�����ÿ��counterֻ�ܷ�һ��kitchenObject��
    // ��˽�����plate_visual���ڻ���ʱ�������������ӡ�

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (platesSpawnAmount < platesSpawnAmountMax) {
                platesSpawnAmount++;

                // ���� plate (visual)
                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (platesSpawnAmount > 0) {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this,EventArgs.Empty);

            }
        }
    }


}
