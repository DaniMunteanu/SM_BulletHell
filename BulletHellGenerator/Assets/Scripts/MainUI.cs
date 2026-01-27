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
    [SerializeField] private Slider horizontalAngleStepSlider;
    [SerializeField] private TMP_Text horizontalAngleStepText;
    [SerializeField] private Slider radiusSlider;
    [SerializeField] private TMP_Text radiusText;
    [SerializeField] private Slider firingRateSlider;
    [SerializeField] private TMP_Text firingRateText;
    [SerializeField] private Slider wiggleSpeedSlider;
    [SerializeField] private TMP_Text wiggleSpeedText;
    [SerializeField] private Slider horizontalWiggleSizeSlider;
    [SerializeField] private TMP_Text horizontalWiggleSizeText;
    [SerializeField] private Slider verticalWiggleSizeSlider;
    [SerializeField] private TMP_Text verticalWiggleSizeText;
    [SerializeField] private Toggle sphereModeToggle;
    [SerializeField] private Slider numberOfSpherePartsSlider;
    [SerializeField] private TMP_Text numberOfSpherePartsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBulletsButton.onClick.AddListener(OnSpawnBulletsButtonPressed);
        nrBulletsSlider.onValueChanged.AddListener(delegate {OnNrSliderValueChanged();});
        horizontalAngleStepSlider.onValueChanged.AddListener(delegate {OnHorizontalAngleStepValueChanged();});
        radiusSlider.onValueChanged.AddListener(delegate {OnRadiusValueChanged();});
        numberOfSpherePartsSlider.onValueChanged.AddListener(delegate {OnNumberOfSpherePartsValueChanged();});
        firingRateSlider.onValueChanged.AddListener(delegate {OnFiringRateValueChanged();});
        wiggleSpeedSlider.onValueChanged.AddListener(delegate {OnWiggleSpeedValueChanged();});
        horizontalWiggleSizeSlider.onValueChanged.AddListener(delegate {OnHorizontalWiggleValueChanged();});
        verticalWiggleSizeSlider.onValueChanged.AddListener(delegate {OnVerticalWiggleValueChanged();});
        sphereModeToggle.onValueChanged.AddListener(delegate {OnSphereModeValueChanged();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpawnBulletsButtonPressed()
    {
        patternGenerator.Fire();
    }

    private void OnNrSliderValueChanged()
    {
        patternGenerator.numberOfBullets = (int)nrBulletsSlider.value;
        nrBulletsText.text = "Number of Bullets: " + (int)nrBulletsSlider.value;
    }

    private void OnHorizontalAngleStepValueChanged()
    {
        patternGenerator.horizontalAngleStep = horizontalAngleStepSlider.value;
        horizontalAngleStepText.text = "Horizontal angle step (degrees): " + horizontalAngleStepSlider.value;
    }

    private void OnRadiusValueChanged()
    {
        patternGenerator.radius = radiusSlider.value;
        radiusText.text = "Radius: " + radiusSlider.value;
    }

    private void OnFiringRateValueChanged()
    {
        patternGenerator.firingRate = firingRateSlider.value;
        firingRateText.text = "Firing rate (seconds): " + firingRateSlider.value;
        patternGenerator.OnFiringSpeedValueChanged();
    }
    private void OnWiggleSpeedValueChanged()
    {
        patternGenerator.wiggleSpeed = wiggleSpeedSlider.value;
        wiggleSpeedText.text = "Wiggle speed: " + wiggleSpeedSlider.value;
    }
    private void OnHorizontalWiggleValueChanged()
    {
        patternGenerator.horizontalWiggleSize = horizontalWiggleSizeSlider.value;
        horizontalWiggleSizeText.text = "Horizontal wiggle size: " + horizontalWiggleSizeSlider.value;
    }
    private void OnVerticalWiggleValueChanged()
    {
        patternGenerator.verticalWiggleSize = verticalWiggleSizeSlider.value;
        verticalWiggleSizeText.text = "Vertical wiggle size: " + verticalWiggleSizeSlider.value;
    }

    private void OnSphereModeValueChanged()
    {
        patternGenerator.sphereMode = sphereModeToggle.isOn;   
    }

    private void OnNumberOfSpherePartsValueChanged()
    {
        patternGenerator.numberOfSphereParts = (int)numberOfSpherePartsSlider.value;
        numberOfSpherePartsText.text = "Number of sphere parts: " + (int)numberOfSpherePartsSlider.value;
    }
}
