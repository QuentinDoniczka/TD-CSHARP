using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleQuentinDoniczka.Data.Entities;

[Table("game_saves")]
public class GameSave
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;

    [Column("grid_state")]
    [MaxLength(9)]
    public string GridState { get; set; } = string.Empty;

    [Column("current_symbol")]
    public char CurrentSymbol { get; set; }

    [Column("saved_at")]
    public DateTime SavedAt { get; set; }
}
