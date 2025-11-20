using System;
using Microsoft.EntityFrameworkCore;
using NexaWorksTickets.Data;
using Projet_6.DTOs;



namespace Projet_6
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);


      builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




      // Add services to the container.
      builder.Services.AddControllersWithViews();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");




      // TEST




      using (var scope = app.Services.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


        // 1 - Obtenir tous les problèmes en cours(tous les produits)
        var tickets1 = Queries.GetTickets(context, statusName: "En cours"); 

        // 2 - Obtenir tous les problèmes en cours pour un produit (toutes les versions)
        var tickets2 = Queries.GetTickets(context, statusName: "En cours", productName: "Planificateur d'Anxiété Sociale");

        // 3 - Obtenir tous les problèmes en cours pour un produit (une seule version)
        var tickets3 = Queries.GetTickets(context, statusName: "En cours", productName: "Trader en herbe", versionLabel: "1.1");

        // 4 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (toutes les versions) 
        var tickets4 = Queries.GetTickets(context, productName: "Maître des investissements", startDate: new DateTime(2025, 8, 1), endDate: new DateTime(2025, 9, 30));

        // 5 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit(une seule version)
        var tickets5 = Queries.GetTickets(context, productName: "Planificateur d'entraînement", versionLabel: "1.4", startDate: new DateTime(2023, 2, 1), endDate: new DateTime(2025, 4, 30));

        // 6 - Obtenir tous les problèmes en cours contenant une liste de mots-clés (tous les produits)
        var tickets6 = Queries.GetTickets(context, statusName: "En cours", keywords:  ["intelligence", "artificielle" ]);

        // 7 - Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (toutes les versions)
        var tickets7 = Queries.GetTickets(context, statusName: "En cours", productName: "Planificateur d'entraînement", keywords: ["chocolat"]);

        // 8 - Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (une seule version)
        var tickets8 = Queries.GetTickets(context, statusName: "En cours", productName: "Trader en Herbe", versionLabel: "1.5", keywords: ["banque", "commande de trombones"]);

        // 9 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés(toutes les versions)
        var tickets9 = Queries.GetTickets(context, productName: "Planificateur d'Anxiété Sociale", startDate: new DateTime(2025, 9, 1), endDate: new DateTime(2025, 12, 31), keywords: ["journal", "personnel"]);

        // 10 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version)
        var tickets10 = Queries.GetTickets(context, productName: "Planificateur d'Anxiété Sociale", versionLabel: "1.3", startDate: new DateTime(2025, 9, 1), endDate: new DateTime(2025, 9, 30), keywords: ["journal", "personnel"]);

        // 11 - Obtenir tous les problèmes résolus (tous les produits)
        var tickets11 = Queries.GetTickets(context, statusName: "Résolu");

        // 12 - Obtenir tous les problèmes résolus pour un produit (toutes les versions)
        var tickets12 = Queries.GetTickets(context, statusName: "Résolu", productName: "Maître des investissements");

        // 13 - Obtenir tous les problèmes résolus pour un produit (une seule version)
        var tickets13 = Queries.GetTickets(context, statusName: "Résolu", productName: "Maître des investissements", versionLabel: "1.3");

        // 14 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (toutes les versions)
        var tickets14 = Queries.GetTickets(context, statusName: "Résolu", productName: "Maître des investissements", startDate: new DateTime(2020, 6, 1), endDate: new DateTime(2025, 1, 1));

        // 15 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (une seule version)
        var tickets15 = Queries.GetTickets(context, statusName: "Résolu", productName: "Maître des investissements", versionLabel: "1.0", startDate: new DateTime(2020, 6, 1), endDate: new DateTime(2025, 1, 1));

        // 16 - Obtenir tous les problèmes résolus contenant une liste de mots-clés (tous les produits)
        var tickets16 = Queries.GetTickets(context, statusName: "Résolu", keywords: ["appareil", "différent"]);

        // 17 - Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (toutes les versions)
        var tickets17 = Queries.GetTickets(context, statusName: "Résolu", productName: "Trader en Herbe", keywords: ["action", "appareil"]);

        // 18 - Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (une seule version)
        var tickets18 = Queries.GetTickets(context, statusName: "résolu", productName: "Planificateur d'Entraînement", versionLabel: "1.0", keywords: ["Apple", "Watch"]);

        // 19 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots - clés(toutes les versions)
        var tickets19 = Queries.GetTickets(context, statusName: "résolu", productName: "Planificateur d'Entraînement", startDate: new DateTime(2025, 8, 1), endDate: new DateTime(2025, 8, 31), keywords: ["entraînement", "fractionné"]);

        // 20 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version)
        var tickets20 = Queries.GetTickets(context, statusName: "résolu", productName: "Planificateur d'Anxiété Sociale", versionLabel: "1.5", startDate: new DateTime(2025, 10, 1), endDate: new DateTime(2025, 12, 31), keywords: ["intensives"]);



        Queries.PrintTickets(tickets20, "Tickets filtrés");
      }

















      app.Run();
    }
  }
}
