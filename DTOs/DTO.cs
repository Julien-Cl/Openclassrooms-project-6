namespace Projet_6.DTOs
{

    // Utilisation du type "record": une classe (donc type référence, ce qui évite des copies à chaque passage) mais immutable et avec une comparaison par valeur intégrée
    // Propriétés en lecture seule générées automatiquement. 
    public record TicketDisplayDto(
        int Id,
        string Description,
        string Resolution,
        DateTime DateCreation,
        string Produit,
        string Version,
        string SystemeExploitation,
        string Statut
    );



}
