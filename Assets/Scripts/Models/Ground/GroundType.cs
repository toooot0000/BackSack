using System.ComponentModel;

namespace Models.Ground{
    public enum GroundType{
        [Description("null")]
        Null,
        [Description("fire")]
        Fire,
        [Description("water")]
        Water,
        [Description("poison")]
        Poison,
        [Description("oil")]
        Oil,
        [Description("ice")]
        Ice,
        [Description("grass")]
        Grass,
        [Description("explosion")]
        Explosion,
        [Description("steam")]
        Steam
    }
}