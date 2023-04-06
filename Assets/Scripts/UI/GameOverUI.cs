using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipesDeliveredText;
    [SerializeField] private Button mainMenuButton;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameover()) {
            Show();
            RecipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeAmount().ToString();
        } else {
            Hide();
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
