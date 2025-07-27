namespace Modin
{
    public class IntroUI : BaseUI
    {
        private IntroUIModel model;
        
        public override void Open(BaseUIModel model)
        {
            this.model = model as IntroUIModel;
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            this.model = null;
            gameObject.SetActive(false);
        }
    }
}