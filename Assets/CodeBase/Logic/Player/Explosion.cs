using System;
using CodeBase.Logic.Enemy;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Player
{
  public class Explosion : MonoBehaviour
  {
    [SerializeField] private float _minScale = 1.5f;
    [SerializeField] private float _maxScale = 1.5f;
    [SerializeField] private float _scaleTime = 1.25f;
    [SerializeField] private float _fadeTime = 0.5f;
    [Space(10)]
    [SerializeField] private Color _finishColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Action OnExplosionEnded;
    public void PerformExplosion()
    {
      transform.localScale = Vector3.zero;
      
      Sequence explosionSequence = DOTween.Sequence();
      explosionSequence.Append(ScaleTween());
      explosionSequence.Insert(0,ChangeColorTween());
      explosionSequence.Append(FadeTween());
      explosionSequence.OnComplete(DestroyExplosion);
    }

    private TweenerCore<Vector3, Vector3, VectorOptions> ScaleTween() => 
      transform.DOScale(GetScale(), _scaleTime).SetEase(Ease.Linear);

    private TweenerCore<Color, Color, ColorOptions> ChangeColorTween() => 
      _spriteRenderer.DOColor(_finishColor, _scaleTime);

    private TweenerCore<Color, Color, ColorOptions> FadeTween() => 
      _spriteRenderer.DOFade(0, _fadeTime);


    private Vector3 GetScale() => 
      Vector3.one * Random.Range(_minScale, _maxScale);

    private void DestroyExplosion()
    {
      OnExplosionEnded?.Invoke();

      _spriteRenderer.DOKill();
      transform.DOKill();
      Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.TryGetComponent(out IHittable hittable)) 
        hittable.Hit();
    }
  }
}
