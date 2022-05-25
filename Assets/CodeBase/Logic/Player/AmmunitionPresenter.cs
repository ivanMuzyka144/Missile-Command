using System;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class AmmunitionPresenter : MonoBehaviour
  {
    [SerializeField] private GameObject[] _ammoImages;
    [SerializeField] private TextMeshProUGUI _outText;

    private int _towerId;
    private TowersData _towersData;

    public void Construct(int towerId,TowersData towersData)
    {
      _towerId = towerId;
      _towersData = towersData;
      _towersData.OnTowerAmmoChanged += HandleAmmunitionChanged;
      
      UpdateAmmoImages(towersData.GetTowerAmmo(_towerId));
    }

    private void OnDestroy() => 
      _towersData.OnTowerAmmoChanged -= HandleAmmunitionChanged;

    public void HandleAmmunitionChanged(int towerId, int ammoCount)
    {
      if(towerId == _towerId)
        UpdateAmmoImages(ammoCount);
    }

    public void UpdateAmmoImages(int ammoCount)
    {
      _outText.gameObject.SetActive(ammoCount <= 0);

      for (int i = 0; i < _ammoImages.Length; i++) 
        _ammoImages[i].SetActive(i + 1 <= ammoCount);
    }
  }
}