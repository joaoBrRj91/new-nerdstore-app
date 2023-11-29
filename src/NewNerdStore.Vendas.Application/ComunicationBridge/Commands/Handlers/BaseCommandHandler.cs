﻿using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Core.Messages.Commons.Notifications;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;

namespace NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers
{
    public abstract class BaseCommandHandler<T> where T : Command
    {
        private readonly INotificationMediatorStrategy _notificationMediatorStrategy;

        protected BaseCommandHandler(INotificationMediatorStrategy notificationMediatorStrategy)
        {
            _notificationMediatorStrategy = notificationMediatorStrategy;
        }

        protected bool CommandIsValid(T message)
        {
            if (message.EhValido()) return true;

            message.ValidationResult
                .Errors
                .ForEach(error =>
                {
                    //Lançar um evento de erro - Domain Notification
                    _notificationMediatorStrategy
                    .PublishNotification(new DomainErrorNotifications(key: message.MessageType, value: error.ErrorMessage));
                });


            return false;
        }

    }
}