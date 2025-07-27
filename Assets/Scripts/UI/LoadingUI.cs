namespace Modin
{
    public class LoadingUI : BaseUI
    {
        private LoadingUIModel model;
        
        public override void Open(BaseUIModel model)
        {
            this.model = model as LoadingUIModel;
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}