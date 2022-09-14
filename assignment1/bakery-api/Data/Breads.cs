using bakery_data.Models;

namespace bakery_api.Data;

public static class Breads { 

  public static IList<Bread> BreadList { get; }

  static Breads() {
    BreadList = new List<Bread>() {
      new Bread{
        Title = "Arboud",
        Type = "Unleavened bread",
        Country = "Jordan",
        Description = "Unleavened bread made from flour, water and salt, baked in the embers of a fire. Traditional among Arab Bedouin",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/d/de/Arboud_Bread_Jordan.jpg"
      }, 
      new Bread{
        Title = "Bagel",
        Type = "Yeast bread",
        Country = "Germany",
        Description = "Ring-shaped, usually with a dense, chewy interior; usually topped with sesame or poppy seeds baked into the surface. May be boiled in lye.",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/8/88/Plain-Bagel.jpg"
      },
      new Bread{
        Title = "Bing",
        Type = "Flatbread",
        Country = "China",
        Description = "Similar to a Mexican tortilla, only much thicker; usually cooked on a griddle.",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Bing_zi_%28Chinese_pancakes%29.jpg"
      },
    };
  }
}