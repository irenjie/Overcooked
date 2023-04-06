using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour {
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindKeyTrandform;

    private void Awake() {
        Instance = this;

        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            updateViusal();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            updateViusal();
        });

        closeButton.onClick.AddListener(() => {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.InteractAlt); });
        pauseButton.onClick.AddListener(() => { RebindBingding(GameInput.Binding.Pause); });
    }

    private void Start() {
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        updateViusal();

        HidePressToRebindKey();
        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void updateViusal() {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

        moveUpText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBingdingText(GameInput.Binding.Pause);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTrandform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey() {
        pressToRebindKeyTrandform.gameObject.SetActive(false);
    }

    private void RebindBingding(GameInput.Binding bingding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBingding(bingding, () => {
            HidePressToRebindKey();
            updateViusal();
        });
    }
}
