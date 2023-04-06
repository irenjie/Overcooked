using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private float deliveryUITimer;
    private float deliveryUITimerMax = 2f;
    bool ShowDliveryUI;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;

        Hide();
        deliveryUITimer = deliveryUITimerMax;
        ShowDliveryUI = false;
    }

    private void Update() {
        if (ShowDliveryUI) {
            Debug.Log("show");
            deliveryUITimer -= Time.deltaTime;
            if (deliveryUITimer < 0) {
                deliveryUITimer = deliveryUITimerMax;
                Hide();
                ShowDliveryUI = false;
            }
        }
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e) {
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "Delivery\nFailed";

        Show();
        ShowDliveryUI = true;
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nSuccess";

        Show();
        ShowDliveryUI = true;
    }

    public void Show() {
        ShowDliveryUI = true;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
