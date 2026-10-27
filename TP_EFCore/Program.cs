
using Microsoft.EntityFrameworkCore;
Console.OutputEncoding = System.Text.Encoding.UTF8;

using var db = new AppDbContext();

// Créer la base de données si elle n'existe pas
db.Database.EnsureCreated();

// Ajouter des produits si la table est vide
if (!db.Produits.Any())
{
    db.Produits.AddRange(
        new Produit
        {
            Nom = "Clavier",
            Categorie = "Informatique",
            Prix = 45
        },
        new Produit
        {
            Nom = "Souris",
            Categorie = "Informatique",
            Prix = 25
        },
        new Produit
        {
            Nom = "Ecran",
            Categorie = "Informatique",
            Prix = 350
        },
        new Produit
        {
            Nom = "Chaise",
            Categorie = "Mobilier",
            Prix = 120
        },
        new Produit
        {
            Nom = "Bureau",
            Categorie = "Mobilier",
            Prix = 200
        },
        new Produit
        {
            Nom = "Casque",
            Categorie = "Informatique",
            Prix = 80
        }
    );

    db.SaveChanges();
}
Console.WriteLine("Tous les produits :");
foreach (var p in db.Produits)
    Console.WriteLine($"{p.Id} - {p.Nom} ({p.Categorie}) : {p.Prix} €");

Console.WriteLine("\nProduits à plus de 100 € :");
var chers = db.Produits.Where(p => p.Prix > 100).OrderBy(p => p.Prix);
foreach (var p in chers)
    Console.WriteLine($"{p.Nom} : {p.Prix} €");

Console.WriteLine("\nProduits Informatique :");
foreach (var p in db.Produits.Where(p => p.Categorie == "Informatique"))
    Console.WriteLine(p.Nom);

Console.WriteLine($"\nPrix moyen : {db.Produits.Average(p => p.Prix):F2} €");

Console.WriteLine("\nNombre de produits par catégorie :");
var parCategorie = db.Produits
    .GroupBy(p => p.Categorie)
    .Select(g => new { Categorie = g.Key, Nombre = g.Count() });
foreach (var g in parCategorie)
    Console.WriteLine($"{g.Categorie} : {g.Nombre}");