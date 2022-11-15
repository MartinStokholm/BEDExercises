using MessagePack;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffetApp.Models;


public class CheckedIn
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    [Required] public int Adults { get; set; } = 0;
    [Required] public int Children { get; set; } = 0;
    public KitchenReport KitchenReport { get; set; } = default!;
}