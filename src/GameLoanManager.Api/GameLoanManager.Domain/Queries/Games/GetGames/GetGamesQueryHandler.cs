﻿using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Games.GetGames.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Games.GetGames
{
    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GetGamesResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _repository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<GetGamesQueryHandler> _logger;

        public GetGamesQueryHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            INotificationContext notificationContext,
            ILogger<GetGamesQueryHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationContext = notificationContext;
            _logger = logger;
        }


        public async Task<IEnumerable<GetGamesResponse>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetGamesQueryHandler was called");
            
            var games = await _repository.FindAsync(cancellationToken);

            if (!games.Any())
            {
                _notificationContext.AddNotification("Nenhum Jogo Encontrado", "Não existe nenhum jogo no Banco de Dados :(");
                return default;
            }

            _logger.LogInformation("GetGamesQueryHandler end of execution");
            
            return _mapper.Map<IEnumerable<GetGamesResponse>>(games);
        }
    }
}
