using BookWise.Customer.Infrastructure.Notifications.Abstraction;
using BookWise.Customer.Infrastructure.Notifications;
using Microsoft.Extensions.Logging;

namespace BookWise.Customer.Application.Helpers;

public sealed class NotificationHelper
{
    public static void Notificar(
        Exception ex,
        string mensagem,
        INotificationService notificacaoService,
        ILogger? logger = null)
    {
        var notificacao = CriarNotificacao(mensagem);
        notificacaoService.Adicionar(notificacao);

        logger?.LogCritical(ex, $"Mensagem: {mensagem}. Detalhes: {ex.Message}");
    }

    private static ErroResponse CriarNotificacao(string mensagem) => new() { Mesangem = mensagem };
}
