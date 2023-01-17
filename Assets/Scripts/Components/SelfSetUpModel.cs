using MVC;
using Utility.Loader.Attributes;

namespace Components{
    public abstract class SelfSetUpModel : Model{
        [Key("name")]
        public string Name;
        [Key("desc")]
        public string Desc;
        public new int? ID{
            set{
                base.ID = value;
                this.SetUp(GetType());
            }
            get => base.ID;
        }
    }
}