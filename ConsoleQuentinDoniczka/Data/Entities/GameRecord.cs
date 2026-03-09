using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConsoleQuentinDoniczka.Core;

namespace ConsoleQuentinDoniczka.Data.Entities;

[Table("game_records")]
public class GameRecord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("result")]
    public GameResult Result { get; set; }
}
