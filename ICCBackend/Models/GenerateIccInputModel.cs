// Copyright 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System;
using System.ComponentModel.DataAnnotations;

namespace NL.Rijksoverheid.ExposureNotification.IccBackend.Models
{
    public class GenerateIccInputModel
    {
        [Required]
        public string UserId { get; set; }
    }
}