using UnityEngine;
using UnityEngine.UI;

namespace Assets.__Game.Resources.Scripts.Train
{
  public class Answer : MonoBehaviour
  {
    [SerializeField] public Image _image;

    [HideInInspector]
    public Sprite AnswerSprite;

    public void SetSpriteAndImage(Sprite sprite)
    {
      AnswerSprite = sprite;
      _image.sprite = AnswerSprite;
    }
  }
}