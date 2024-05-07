using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Resources.Scripts.Variant
{
  public class Variant : MonoBehaviour
  {
    [SerializeField] private Image _variantImage;

    [HideInInspector]
    public Sprite VariantSprite;

    public void SetSpriteAndImage(Sprite variantSprite, bool showSprite)
    {
      VariantSprite = variantSprite;

      if (showSprite == true)
        _variantImage.sprite = VariantSprite;
    }
  }
}