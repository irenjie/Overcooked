using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private State currentState;     // StoveCounter ��ǰ״̬
    private float fryingTimer;
    private float burningTimer;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private FryingRecipeSO fryingRecipeSo;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        currentState = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (currentState) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSo.fryingTimeMax
                    });

                    if (fryingTimer > fryingRecipeSo.fryingTimeMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);

                        currentState = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burnryingTimeMax
                    });

                    if (burningTimer > burningRecipeSO.burnryingTimeMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        currentState = State.Burned;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });

                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        // ��ҽ����ϵ���Ʒ����counter�ϣ����ߴӸ�counter�����߶���
        if (!HasKitchenObject()) {
            // �ù�̨��û�ж���
            if (player.HasKitchenObject()) {
                // ��������ж���
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    fryingRecipeSo = GetFryingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO());
                    // �������Ķ����������,����ڸ�counter��
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    currentState = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = currentState
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSo.fryingTimeMax
                    });
                }
            }
        } else {
            // ��̨���ж���
            if (!player.HasKitchenObject()) {
                // �������û����
                GetKitchenObject().SetKitchenObjectParent(player);
                currentState = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = currentState
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            } else {
                // ��������ж���
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // ������ϵĶ���������
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();

                        currentState = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = currentState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        // ����KOSO�Ƿ���cuttingRecipeSOArray��һԱ��input
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool IsFried() {
        return currentState == State.Fried;
    }

}
