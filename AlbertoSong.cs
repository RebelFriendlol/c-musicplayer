using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AlbertoPlayer;
public class AlbertoSong
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IDsong { get; set; }

    public string? Wykonawca { get; set; }
    public string? Tytul { get; set; }
    public string? Album { get; set; }
    public decimal DlugoscMinuty { get; set; }
    public string? Jezyk { get; set; }
    public byte[]? Okladka { get; set; }
    public decimal Cena { get; set; }

    // Nowe pola do przechowywania plików piosenek
    public string? FileName { get; set; }
    public byte[]? FileData { get; set; }
}