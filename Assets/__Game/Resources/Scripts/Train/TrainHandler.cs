using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Train
{
  public class TrainHandler : MonoBehaviour
  {
    [SerializeField] private Transform _cartJoint;
    [Space]
    [SerializeField] private CartHandler _cartPrefab;
    [Space]
    [SerializeField] private int _cartAmount;

    private void Start()
    {
      SpawnCarts();
    }

    private void SpawnCarts()
    {
      Transform lastCartJoint = _cartJoint;
      CartHandler newCart = null;

      for (int i = 0; i < _cartAmount; i++)
      {
        newCart = Instantiate(_cartPrefab, lastCartJoint.position, Quaternion.Euler(0, 90, 0), transform);

        lastCartJoint = newCart.CartJoint;
      }
    }
  }
}