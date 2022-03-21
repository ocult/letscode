using LetsCode.Controllers;
using LetsCode.Models;

namespace LetsCode.Customs;

public static class CardDtoLoggingInformationExtension
{
    public static void LogCardChanged(this ILogger<CardsController> logger, Card card) => LogCard(logger, card, "Alterado");

    public static void LogCardDeleted(this ILogger<CardsController> logger, Card card) => LogCard(logger, card, "Removido");

    private static void LogCard(ILogger<CardsController> logger, Card card, String action)
    {
        logger.LogInformation($"Card {card.Id} - {card.Title} - {action}");
    }
}
