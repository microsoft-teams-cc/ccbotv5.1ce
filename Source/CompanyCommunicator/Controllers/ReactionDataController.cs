// <copyright file="ReactionDataController.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// </copyright>

namespace Microsoft.Reactions.Apps.CompanyCommunicator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Teams.Apps.CompanyCommunicator.Authentication;
    using Microsoft.Teams.Apps.CompanyCommunicator.Common.Repositories.ReactionData;
    using Microsoft.Teams.Apps.CompanyCommunicator.Models;

    /// <summary>
    /// Controller for the reactions data.
    /// </summary>
    [Route("api/reactiondata")]
    [Authorize(PolicyNames.MustBeValidUpnPolicy)]
    public class ReactionDataController : ControllerBase
    {
        private readonly IReactionDataRepository reactionDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactionDataController"/> class.
        /// </summary>
        /// <param name="reactionDataRepository">Reaction data repository instance.</param>
        public ReactionDataController(IReactionDataRepository reactionDataRepository)
        {
            this.reactionDataRepository = reactionDataRepository ?? throw new ArgumentNullException(nameof(reactionDataRepository));
        }

        /// <summary>
        /// Get data for all reactions.
        /// </summary>
        /// <returns>A list of reaction data.</returns>
        [HttpGet]
        public async Task<IEnumerable<ReactionData>> GetAllReactionDataAsync()
        {
            var entities = await this.reactionDataRepository.GetAllSortedAlphabeticallyByNameAsync();
            var result = new List<ReactionData>();
            foreach (var entity in entities)
            {
                var reaction = new ReactionData
                {
                    Id = entity.ReactionId,
                    Name = entity.Name,
                };
                result.Add(reaction);
            }

            return result;
        }
    }
}
