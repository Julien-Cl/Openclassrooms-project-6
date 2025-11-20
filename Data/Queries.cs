using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NexaWorksTickets.Data;
using NexaWorksTickets.Models;
using Projet_6.DTOs;

namespace NexaWorksTickets.Data
{
  public static class Queries
  {

    // #######################
    // -----------------------
    // -----------------------
    // -- Méthodes communes --
    // -----------------------
    // -----------------------
    // #######################


    // Conversion de IQueryable<Ticket> en IQueryable<TicketDisplayDto> pour affichage
    public static IQueryable<TicketDisplayDto> ToTicketDisplayDto(this IQueryable<Ticket> query)
    {
      return query.Select(t => new TicketDisplayDto(
          t.Id,
          t.Description,
          t.Resolution,
          t.CreationDate,
          t.Configuration.Product.ProductName,
          t.Configuration.VersionNumber.VersionLabel,
          t.Configuration.OpSystem.SystemName,
          t.Status.StatusName
      ));
    }



    // Recherche de mots clés
    private static IQueryable<Ticket> ApplyKeywordFilter(IQueryable<Ticket> query, IEnumerable<string> keywords)
    {
      if (keywords == null || !keywords.Any())
        return query;

      foreach (var keyword in keywords)
      {

        query = query.Where(t => t.Description.ToLower().Contains(keyword.ToLower())); // Ajout d'un filtre SQL selon lequel la requête doit contenir ce mot-clé.

      }

      return query;
    }




    // Affichage console
    public static void PrintTickets(IEnumerable<TicketDisplayDto> tickets, string titre)
    {
      Console.WriteLine($"{titre} : {tickets.Count()} résultat(s)");
      Console.WriteLine(new string('=', 80));

      foreach (var t in tickets)
      {
        //Console.WriteLine($"ID: {t.Id}");
        Console.WriteLine($"Produit: {t.Produit} ({t.Version})");
        Console.WriteLine($"Système: {t.SystemeExploitation}");
        Console.WriteLine($"Statut: {t.Statut}");
        Console.WriteLine($"Date création: {t.DateCreation:yyyy-MM-dd}");
        Console.WriteLine($"Description: {t.Description}");
        Console.WriteLine($"Résolution: {t.Resolution}");
        Console.WriteLine(new string('-', 80));
      }

      Console.WriteLine("Fin des résultats.\n");
    }









    // #################################
    // ---------------------------------
    // ---------------------------------
    // -- Méthode de recherche unique --
    // ---------------------------------
    // ---------------------------------
    // #################################

    // 1 - Obtenir tous les problèmes en cours(tous les produits)
    // 2 - Obtenir tous les problèmes en cours pour un produit (toutes les versions)
    // 3 - Obtenir tous les problèmes en cours pour un produit (une seule version)
    // 4 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (toutes les versions)
    // 5 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit(une seule version)

    // 6 - Obtenir tous les problèmes en cours contenant une liste de mots-clés (tous les produits)
    // 7 - Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (toutes les versions)
    // 8 - Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés(une seule version)
    // 9 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés(toutes les versions)
    // 10 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version)

    // 11 - Obtenir tous les problèmes résolus (tous les produits)
    // 12 - Obtenir tous les problèmes résolus pour un produit (toutes les versions)
    // 13 - Obtenir tous les problèmes résolus pour un produit (une seule version)
    // 14 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (toutes les versions)
    // 15 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (une seule version)

    // 16 - Obtenir tous les problèmes résolus contenant une liste de mots-clés (tous les produits)
    // 17 - Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (toutes les versions)
    // 18 - Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (une seule version)
    // 19 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots-clés (toutes les versions)
    // 20 - Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit contenant une liste de mots-clés (une seule version)


    public static List<TicketDisplayDto> GetTickets(AppDbContext context, string? statusName = null, IEnumerable<string>? keywords = null, string? productName = null, string? versionLabel = null, DateTime? startDate = null, DateTime? endDate = null)
    {
      // Base: all tickets
      var query = context.Tickets.AsQueryable();
      // Statut
      if (!string.IsNullOrEmpty(statusName))
        query = query.Where(t => t.Status.StatusName == statusName);
      // Produit
      if (productName != null)
        query = query.Where(t => t.Configuration.Product.ProductName == productName);
      // Version
      if (versionLabel != null)
        query = query.Where(t => t.Configuration.VersionNumber.VersionLabel == versionLabel);
      // Période
      if (startDate.HasValue && endDate.HasValue)
        query = query.Where(t => t.CreationDate >= startDate && t.CreationDate <= endDate);
      // Mots-clés
      if (keywords != null && keywords.Any())
        query = ApplyKeywordFilter(query, keywords);

      return query
          .OrderByDescending(t => t.CreationDate)
          .ToTicketDisplayDto()
          .ToList();
    }


    // #############
    // -------------
    // -------------
    // -- Helpers --
    // -------------
    // -------------
    // #############
    // Tentative de création d'une API moins paramétrique, mais ne sert à rien. 


    // 1 - Obtenir tous les problèmes en cours (tous les produits)
    public static List<TicketDisplayDto> GetCurrentTickets(AppDbContext context)
    {
      return GetTickets(context, statusName: "En cours");
    }

    // 2 - Obtenir tous les problèmes en cours pour un produit (toutes les versions)
    public static List<TicketDisplayDto> GetCurrentTicketsByProduct(AppDbContext context, string productName)
    {
      return GetTickets(context, statusName: "En cours", productName: productName);
    }

    // 3 - Obtenir tous les problèmes en cours pour un produit (une seule version)
    public static List<TicketDisplayDto> GetCurrentTicketsByProductAndVersion(AppDbContext context, string productName, string versionLabel)
    {
      return GetTickets(context, statusName: "En cours", productName: productName, versionLabel: versionLabel);
    }

    // 4 - Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (toutes les versions)
    public static List<TicketDisplayDto> GetAllTicketsByProductAndPeriod(AppDbContext context, string productName, DateTime startDate, DateTime endDate)
    {
      return GetTickets(context, productName: productName, startDate: startDate, endDate: endDate);
    }


































































































  }
}
