using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ö���ֻ�ܴ���һ�����Ѿ�������ע�͵��ùؼ���ʹ�䲻���ٴ���
//[CreateAssetMenu]
public class RecipeListSO : ScriptableObject
{
    [SerializeField] public List<RecipeSO> recipeSOList;
}
