using Microsoft.EntityFrameworkCore;

namespace CoL.DB.Entities
{

    [Owned]
    public class WarAttack
    {
        public string AttackerTag { get; set; }
        public string DefenderTag { get; set; }
        public int Stars { get; set; }
        public int DestructionPercentage { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
    }

}
