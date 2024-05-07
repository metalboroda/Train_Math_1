using UnityEngine;

namespace Assets.__Game.Resources.Scripts.SOs
{
  [CreateAssetMenu(fileName = "CorrectObjectsContainer", menuName = "SOs/Containers/CorrectObjectsContainer")]
  public class CorrectImagesContainerSo : MonoBehaviour
  {
    [SerializeField] private GameObject[] _correctObjects;

    public GameObject[] CorrectObjects
    {
      get => _correctObjects;
      private set => _correctObjects = value;
    }
  }
}