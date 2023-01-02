using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Enemies{
    public class EnemyController: Controller<Enemy, EnemyView>, IAfterSetMode{

        public void AfterSetModel(){
            var sprite = Resources.Load<Sprite>(Model.GetSpriteResourcePath());
            if (sprite == null) return;
            view.SetSprite(sprite);
        }
    }
}