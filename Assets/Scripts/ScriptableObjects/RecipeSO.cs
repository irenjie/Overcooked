using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private string recipeName;

    public string GetRecipeName() {
        return recipeName;
    }
}
