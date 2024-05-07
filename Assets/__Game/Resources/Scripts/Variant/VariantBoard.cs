using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Variant
{
  [ExecuteAlways]
  public class VariantBoard : MonoBehaviour
  {
    [SerializeField] private bool _allowEdit = true;
    [SerializeField] private float _spacing = 1f;
    [SerializeField] private float _fixedChildWidth = 1f;
    [Space]
    [SerializeField] private VariantItem[] _variantItems;
    [Space]
    [SerializeField] private Variant[] _variantObjects;

    private void Start()
    {
      InitVariants();
    }

    private void Update()
    {
      if (Application.isPlaying == false)
      {
        RearrangeChildren();
      }
    }

    private void OnValidate()
    {
      if (Application.isPlaying == false && _allowEdit)
      {
        RearrangeChildren();
      }
    }

    void RearrangeChildren()
    {
      float currentXPosition = 0f;

      foreach (Transform child in transform)
      {
        child.localPosition = new Vector3(currentXPosition, 0f, 0f);
        currentXPosition += _fixedChildWidth + _spacing;
      }
    }

    private void InitVariants()
    {
      Sprite currentSprite = null;

      for (int i = 0; i < _variantItems.Length; i++)
      {
        currentSprite = _variantItems[i].VariantSprite;

        _variantObjects[i].SetSpriteAndImage(currentSprite, _variantItems[i].ShowSprite);
      }
    }
  }
}