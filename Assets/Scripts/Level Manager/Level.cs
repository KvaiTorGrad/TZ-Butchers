using SingleTon;
using System;
using UnityEngine;

namespace ButchersGames
{
    public class Level : SingletonBase<Level>
    {
        [Serializable]
        private struct ItemsLevelLists
        {
            public ItemsList[] itemsList;
        }
        [Serializable]
        private class ItemsList
        {
            public FlagZone flagZone;
        }
        [SerializeField] private ItemsLevelLists _itemsLevelLists;
        [SerializeField] private int _activeCurrentItem;
        [SerializeField] private GameObject[] _uiMenu;
        protected override void Awake()
        {
            base.Awake();
            StartActivePlatform();
        }

        private void Start()
        {
            LevelManager.OnLevelStarted += HiddeElementMenu;
            LevelManager.OnLevelEntedIsLoss += LevelEnd;
        }
        private void HiddeElementMenu()
        {
            _uiMenu[0].SetActive(false);
        }
        private void StartActivePlatform()
        {
            foreach (var itemsList in _itemsLevelLists.itemsList)
            {
                if (_activeCurrentItem == 0 || _activeCurrentItem == 1)
                {
                    itemsList.flagZone.gameObject.SetActive(true);
                    _activeCurrentItem++;
                }
            }
            _activeCurrentItem = 0;
        }
        public void TurnOffItems()
        {

            foreach (var itemsList in _itemsLevelLists.itemsList)
            {
                if (!itemsList.flagZone.gameObject.activeSelf && !itemsList.flagZone.IsPassed && _activeCurrentItem < 1)
                {
                    itemsList.flagZone.gameObject.SetActive(true);
                    _activeCurrentItem++;
                    TurnOffItems();
                    break;
                }
                else if (itemsList.flagZone.gameObject.activeSelf && itemsList.flagZone.IsPassed)
                {
                    itemsList.flagZone.gameObject.SetActive(false);
                    _activeCurrentItem--;
                }
            }
        }

        private void LevelEnd(bool isLoss)
        {
            LevelManager.OnLevelEnted?.Invoke();
            if(isLoss)
                _uiMenu[2].SetActive(true);
            else
                _uiMenu[1].SetActive(true);
        }
        private void OnDestroy()
        {
            LevelManager.OnLevelStarted -= HiddeElementMenu;
            LevelManager.OnLevelEntedIsLoss -= LevelEnd;
        }
    }

}