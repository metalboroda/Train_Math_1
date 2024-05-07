using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Train
{
  public class TrainHandler : MonoBehaviour
  {
    [SerializeField] private Transform _cartJoint;
    [Space]
    [SerializeField] private CartHandler _cartPrefab;
    [Space]
    [SerializeField] private Answer _answerObject;
    [Space]
    [SerializeField] private CartItem[] _answers;

    private void Start()
    {
      SpawnCarts();
    }

    private void SpawnCarts()
    {
      Transform lastCartJoint = _cartJoint;
      CartHandler spawnedCart = null;
      Answer spawnedAnswer = null;

      for (int i = 0; i < _answers.Length; i++)
      {
        spawnedCart = Instantiate(_cartPrefab, lastCartJoint.position, Quaternion.Euler(0, 90, 0), transform);

        lastCartJoint = spawnedCart.CartJoint;

        spawnedAnswer =  Instantiate(
          _answerObject, spawnedCart.AnswerPlacePoint.position, spawnedCart.AnswerPlacePoint.rotation, spawnedCart.AnswerPlacePoint);

        spawnedAnswer.SetSpriteAndImage(_answers[i].AnswerSprite);
      }
    }
  }
}