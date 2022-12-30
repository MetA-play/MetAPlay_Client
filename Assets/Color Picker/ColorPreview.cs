
using UnityEngine;
using UnityEngine.UI;

public class ColorPreview : MonoBehaviour
{
    public Material previewGraphic;

    public ColorPicker colorPicker;

    public enum MetType
    {
        Cloak,
        Body
    }

    public MetType MT;

    private void Start()
    {
        previewGraphic.color = colorPicker.color;
        colorPicker.onColorChanged += OnColorChanged;

        switch (MT)
        {
            case MetType.Cloak:
                MainSceneManager.Instance.cloakMet = previewGraphic;
                break;
            case MetType.Body:
                MainSceneManager.Instance.bodyMet = previewGraphic;
                break;
            default:
                break;
        }
    }

    public void OnColorChanged(Color c)
    {
        previewGraphic.color = c;
        switch (MT)
        {
            case MetType.Cloak:
        MainSceneManager.Instance.cloakMet = previewGraphic;
                break;
            case MetType.Body:
        MainSceneManager.Instance.bodyMet = previewGraphic;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (colorPicker != null)
            colorPicker.onColorChanged -= OnColorChanged;
    }
}