using __Game.Resources.Scripts.EventBus;
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

    private int _emptyVariantsCounter;
    private int _overallAnswersCounter;
    private int _correctAnswersCounter;
    private int _incorrectAnswerCounter;

    private EventBinding<EventStructs.CorrectAnswerEvent> _correctAnswerEvent;
    private EventBinding<EventStructs.IncorrectCancelEvent> _incorrectAnswerEvent;

    private void OnEnable()
    {
      _correctAnswerEvent = new EventBinding<EventStructs.CorrectAnswerEvent>(ReceiveCorrectAnswer);
      _incorrectAnswerEvent = new EventBinding<EventStructs.IncorrectCancelEvent>(ReceiveIncorrectAnswer);
    }

    private void OnDisable()
    {
      _correctAnswerEvent.Remove(ReceiveCorrectAnswer);
      _incorrectAnswerEvent.Remove(ReceiveIncorrectAnswer);
    }

    private void Start()
    {
      _emptyVariantsCounter = CountVariantsWithSpriteHidden();

      InitVariants();

      EventBus<EventStructs.ComponentEvent<VariantBoard>>.Raise(new EventStructs.ComponentEvent<VariantBoard> { Data = this });
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

    public int CountVariantsWithSpriteHidden()
    {
      int count = 0;

      foreach (VariantItem item in _variantItems)
      {
        if (item.ShowSprite == false)
        {
          count++;
        }
      }
      return count;
    }

    private void ReceiveCorrectAnswer(EventStructs.CorrectAnswerEvent correctAnswerEvent)
    {
      _overallAnswersCounter++;
      _correctAnswersCounter++;

      if (_correctAnswersCounter == _emptyVariantsCounter)
      {
        Debug.Log("Win");
        EventBus<EventStructs.WinEvent>.Raise(new EventStructs.WinEvent());

        return;
      }

      CheckOverallAnswer();
    }

    private void ReceiveIncorrectAnswer(EventStructs.IncorrectCancelEvent incorrectCancelEvent)
    {
      _overallAnswersCounter++;
      _incorrectAnswerCounter++;

      if (_incorrectAnswerCounter == _emptyVariantsCounter)
      {
        Debug.Log("Lose");
        EventBus<EventStructs.LoseEvent>.Raise(new EventStructs.LoseEvent());

        return;
      }
    }

    private void CheckOverallAnswer()
    {
      if (_overallAnswersCounter == _emptyVariantsCounter)
      {
        if (_correctAnswersCounter < _emptyVariantsCounter)
          EventBus<EventStructs.LoseEvent>.Raise(new EventStructs.LoseEvent());

        else if (_incorrectAnswerCounter < _emptyVariantsCounter) { }
      }
    }

    public Transform GetLastVariantObjectTransform()
    {
      if (_variantObjects != null && _variantObjects.Length > 0)
        return _variantObjects[_variantObjects.Length - 1].transform;

      return null;
    }
  }
}