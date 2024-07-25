namespace GameContent.UI.UISystem
{
    public class BaseController<M> where M : BaseModel
    {
        protected M model;

        public virtual void Init(M model)
        {
            this.model = model;
        }
    }
}