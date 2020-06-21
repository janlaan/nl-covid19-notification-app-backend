﻿// Copyright 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflow.BackgroundJobs;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.WorkflowStateEngineApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        [HttpGet]
        [Route("Purge")]
        public async Task<IActionResult> Purge([FromServices] PurgeExpiredSecretsDbCommand command)
        {
            return await command.Execute();
        }
    }
}