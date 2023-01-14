# Development Log

----

## 1.12

1. Done:
   1. Toaster
   2. Effect:
      1. ITileObject作为效果执行以及传播的主体，可以通过stage来索敌
      2. 其他东西（Ground比如）的方法都返回IEffectTemplate，由caller来填补所需要的字段（比如索敌）
2. Todo:
   1. 攻击动画
      1. 当前解决方案：每个动画保存为prefab，由view动态加载
   2. 连锁效果结算动画的间隔
      1. 考虑优先播放动画然后在动画过程中consume效果？
         1. Model层更新滞后？
      2. 
