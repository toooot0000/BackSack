#interactioon
游戏内的交互方式。分为以下几类：

## [[伤害(Damage)]]
发出者：所有[[基础对象(TileObject)]]或者无发出者
接受者：所有[[可受伤对象(Damageable)]]
触发方式：执行[[行动(Action)#攻击(Attack)|攻击]]

## [[对话(Talk)]]
发出者：[[玩家角色(Player)]]
接收者：[[可交谈对象(Talkable)]]
触发方式：玩家与对象相邻且向对象方向[[行动(Action)#移动(Move)]]

## 踩上(Stepover)
发出者：[[基础对象(TileObject)]]
接收者: [[可踩上对象(Stepoverable)]]
触发方式：可移动对象[[行动(Action)#移动(Move)]]到可踩上对象上时

## 推动(Push)
发出者：[[基础对象(TileObject)]]
接收者：[[可强制移动对象(ForceMovable)]]
触发方式：

