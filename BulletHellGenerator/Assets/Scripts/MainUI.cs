using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private PatternGenerator patternGenerator;
    [SerializeField] private Button spawnBulletsButton;
    [SerializeField] private Slider nrBulletsSlider;
    [SerializeField] private TMP_Text nrBulletsText;
    [SerializeField] private Slider angleStepSlider;
    [SerializeField] private TMP_Text angleStepText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBulletsButton.onClick.AddListener(OnSpawnBulletsButtonPressed);
        nrBulletsSlider.onValueChanged.AddListener(delegate {OnNrSliderValueChanged();});
        angleStepSlider.onValueChanged.AddListener(delegate {OnAngleStepValueChanged();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpawnBulletsButtonPressed()
    {
        patternGenerator.SpawnBullets();
    }

    private void OnNrSliderValueChanged()
    {
        patternGenerator.numberOfBullets = (int)nrBulletsSlider.value;
        nrBulletsText.text = "Number of Bullets: " + (int)nrBulletsSlider.value;
    }

    private void OnAngleStepValueChanged()
    {
        patternGenerator.angleStep = angleStepSlider.value;
        angleStepText.text = "Angle step (degrees): " + angleStepSlider.value;
    }

}
