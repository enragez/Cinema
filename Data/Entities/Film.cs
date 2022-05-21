namespace Cinema.Data.Entities;

public class Film
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Genre { get; set; }
    
    public string Director { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Duration { get; set; }
    
    public byte[] Poster { get; set; }
}