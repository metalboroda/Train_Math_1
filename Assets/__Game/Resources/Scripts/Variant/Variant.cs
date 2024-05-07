using __Game.Resources.Scripts.EventBus;
using Assets.__Game.Resources.Scripts.Train;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Resources.Scripts.Variant
{
  public class Variant : MonoBehaviour
  {
    [SerializeField] private Image _variantImage;

    public bool ShowSprite { get; private set; }
    public Sprite VariantSprite { get; private set; }
    public Sprite ReceivedAnswer { get; private set; }

    public void SetSpriteAndImage(Sprite variantSprite, bool showSprite)
    {
      VariantSprite = variantSprite;
      ShowSprite = showSprite;

      if (showSprite == true)
        _variantImage.sprite = VariantSprite;
    }

    public void Place(Answer answerToPlace)
    {
      answerToPlace.transform.DOMove(transform.position, 0.1f)
        .OnComplete(() =>
        {
          answerToPlace.transform.SetParent(transform);

          CheckForCorrectAnswer();
        });

      ShowSprite = true;
      ReceivedAnswer = answerToPlace.AnswerSprite;
    }

    private void CheckForCorrectAnswer()
    {
      if (VariantSprite == ReceivedAnswer)
      {
        EventBus<EventStructs.CorrectAnswerEvent>.Raise(new EventStructs.CorrectAnswerEvent());
      }
      else
      {
        EventBus<EventStructs.IncorrectCancelEvent>.Raise(new EventStructs.IncorrectCancelEvent());
      }
    }
  }
}