using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
  public class WindowBase : MonoBehaviour
  {
    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    private void OnDestroy() => 
      Cleanup();

    protected virtual void OnAwake()
    {
    }

    private void OnCloseButtonClicked()
    {
      Destroy(gameObject);
    }

    protected virtual void Initialize()
    {
      
    }

    protected virtual void SubscribeUpdates()
    {
      
    }

    protected virtual void Cleanup()
    {
      
    }

  }
}