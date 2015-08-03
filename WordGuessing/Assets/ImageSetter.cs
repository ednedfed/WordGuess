using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
	public Sprite imageFile;
	public SpriteRenderer destinationImage;

	void Awake()
	{
		SetImage(imageFile);
	}

	public void SetImage(Sprite _image)
	{
		destinationImage.sprite = _image;
	}
}
