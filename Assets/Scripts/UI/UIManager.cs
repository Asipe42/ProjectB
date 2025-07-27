using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modin
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private List<BaseUI> uiList;
        
        private Dictionary<Type, BaseUI> managedUIMap;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(mainCanvas.gameObject);

            managedUIMap = new Dictionary<Type, BaseUI>();
            foreach (var ui in uiList)
            {
                var type = ui.GetType();
                if (!managedUIMap.ContainsKey(type))
                {
                    managedUIMap.Add(type, ui);
                }
                else
                {
                    Debug.LogWarning($"중복된 UI 타입 발견: {type.Name}");
                }
            }
        }

        public T GetUI<T>() where T : BaseUI
        {
            var type = typeof(T);
            if (managedUIMap != null && managedUIMap.TryGetValue(type, out var ui))
            {
                return ui as T;
            }
            
            Debug.LogWarning($"{type.Name} UI를 찾을 수 없습니다");
            return null;
        }
    }
}