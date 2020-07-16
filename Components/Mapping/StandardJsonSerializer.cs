﻿// Copyright 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Text.Json;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Mapping
{
    public class StandardJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _SerializerOptions;

        public StandardJsonSerializer()
        {
            _SerializerOptions = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        }

        public string Serialize<TContent>(TContent input)
        {
            return JsonSerializer.Serialize(input, _SerializerOptions);
        }

        public TContent Deserialize<TContent>(string input)
        {
            return JsonSerializer.Deserialize<TContent>(input, _SerializerOptions);
        }
    }
}