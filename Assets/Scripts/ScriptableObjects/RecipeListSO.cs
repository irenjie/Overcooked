using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 该对象只能创建一个，已经创建，注释掉该关键字使其不能再创建
//[CreateAssetMenu]
public class RecipeListSO : ScriptableObject
{
    [SerializeField] public List<RecipeSO> recipeSOList;
}
