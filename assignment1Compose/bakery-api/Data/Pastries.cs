using bakery_data.Models;

namespace bakery_api.Data;

public static class Pastries {
public static IList<Pastry> PastriesList { get; }
  static Pastries() {
    PastriesList = new List<Pastry>() {
      new Pastry {
        Title = "Alexandertorte",
        Country = "Latvia",
        Description = "Pastry strips filled with berries.",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/5/50/Aleksanterinleivos.jpg"
      },
      new Pastry {
        Title = "Baklava",
        Country = "Ottoman Empire",
        Description = "An Ottoman pastry that is rich and sweet, made of layers of filo pastry filled with chopped nuts and sweetened with syrup or honey.",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/c/c7/Baklava%281%29.png"
      },
      new Pastry {
        Title = "Bear claw",
        Country = "United States",
        Description = "Sweet breakfast pastry",
        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/c/cc/Bear_claw_pastry.JPG"
      },
    };
  }
}
