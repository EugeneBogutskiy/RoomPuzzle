using UnityEngine;

namespace GameContent.UI.UISystem
{
    public class BaseView<M, C> : MonoBehaviour
        where M : BaseModel
        where C : BaseController<M>, new()
    {
        public M model;
        protected C controller;

        public virtual void Awake()
        {
            controller = new C();
            controller.Init(model);
        }
    }
}