using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode, RequireComponent (typeof (AspectRatioFitter))]
public class UIImageFit : MonoBehaviour
{
    void Start()
    {
        var image = GetComponent<Image>().sprite;
        float imageWidth = image.rect.width;
        float imageHeight = image.rect.height;

        var fitter = GetComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        fitter.aspectRatio = imageWidth / imageHeight;
    }
}
