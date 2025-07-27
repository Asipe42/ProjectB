using UnityEngine;

namespace Modin
{
    public abstract class BaseUI : MonoBehaviour
    {
        public abstract void Open(BaseUIModel model);
        public abstract void Close();
    }
}