using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PhotoController : MonoBehaviour
{
    [SerializeField] private Transform SpawnParent;
    [SerializeField] private GameObject PhotoCanvas;
    [SerializeField] private GameObject[] PhotoCanvass;
    [SerializeField] private Sprite[] Photos;

    private int _photoCanvasIndex = 0;
    public void TakeAPhoto(OptionType photoIndex)
    {
        //   var photo = Instantiate(PhotoCanvas, Vector3.zero, Quaternion.identity);
        var photo = GetPhotoCanvas();
        var rectTransform = photo.GetComponent<RectTransform>();
        var rotation = rectTransform.rotation;
        rotation.z = UnityEngine.Random.rotation.z;
        rectTransform.rotation = rotation;

        var image = photo.GetComponent<Image>();
        image.sprite = Photos[(int)photoIndex];

        photo.transform.SetParent(SpawnParent);
        photo.transform.localPosition = Vector3.zero;

        rectTransform.DOScale(Vector3.one, .25f);
    }


    private GameObject GetPhotoCanvas()
    {
        return PhotoCanvass[_photoCanvasIndex++];
    }
}
