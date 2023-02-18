using System.ComponentModel;

namespace Components.Stages.Floors{
    public enum FloorType{
        /// <summary>
        /// 空地
        /// </summary>
        [Description("empty")]
        Empty,
        [Description("ana")]
        Ana,
        /// <summary>
        /// 墙壁
        /// </summary>
        [Description("block")]
        Block
        // [Description("pillar")]
        // Pillar,
        // [Description("gate")]
        // Gate,
        // [Description("stair")]
        // Stair,
    }
}