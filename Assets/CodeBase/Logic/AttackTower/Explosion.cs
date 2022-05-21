using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.AttackTower
{
  public class Explosion : MonoBehaviour
  {
    [SerializeField] private float _explosionTime = 1;
    [SerializeField] private Color _finishColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void PerformExplosion()
    {
      ScaleExplosion();
      AnimExplosionColor();
      StartCoroutine(CO_DestroyTimer());
    }

    private void ScaleExplosion()
    {
      transform.localScale = Vector3.zero;
      transform.DOScale(Vector3.one * Random.Range(1, 1.75f), _explosionTime)
               .SetEase(Ease.Linear);
    }

    private void AnimExplosionColor() => 
      _spriteRenderer.DOColor(_finishColor, _explosionTime);

    private IEnumerator CO_DestroyTimer()
    {
      yield return new WaitForSeconds(_explosionTime);
      _spriteRenderer.DOKill();
      transform.DOKill();
      Destroy(gameObject);
    }
  }
}
