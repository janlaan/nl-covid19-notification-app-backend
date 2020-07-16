﻿// Copyright 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.IO;
using System.Linq;
using System.Text;
using Google.Protobuf;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySetsEngine.FormatV1;
using NL.Rijksoverheid.ExposureNotification.BackEnd.GeneratedGaenFormat;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySetsEngine.ContentFormatters
{
    public class GeneratedProtobufContentFormatter : IContentFormatter
    {
        public byte[] GetBytes(ExposureKeySetContentArgs content)
        {
            var result = new TemporaryExposureKeyExport
            {
                Region = content.Region,
                BatchNum = content.BatchNum,
                BatchSize = content.BatchSize,
                EndTimestamp = content.EndTimestamp,
                StartTimestamp = content.StartTimestamp,
                SignatureInfos = { content.SignatureInfos.Select(Map).ToArray() },
                Keys = { content.Keys.Select(Map).ToArray() },
            };
            var buffer = result.ToByteArray();

            var headerBytes = Encoding.UTF8.GetBytes(content.Header);
            var stream = new MemoryStream();
            stream.Write(headerBytes);
            stream.Write(buffer);
            return stream.ToArray();
        }

        private static TemporaryExposureKey Map(TemporaryExposureKeyArgs arg)
            => new TemporaryExposureKey
            {
                KeyData = ByteString.CopyFrom(arg.KeyData),
                TransmissionRiskLevel = arg.TransmissionRiskLevel, //todo: fix
                RollingPeriod = arg.RollingPeriod,
                RollingStartIntervalNumber = arg.RollingStartNumber,
                //DaysSinceOnsetOfSymptoms = 0
                //ReportType = TemporaryExposureKey.Types.ReportType.ConfirmedClinicalDiagnosis
            };

        private static SignatureInfo Map(SignatureInfoArgs arg)
            => new SignatureInfo
            {
                SignatureAlgorithm = arg.SignatureAlgorithm,
                VerificationKeyId = arg.VerificationKeyId,
                VerificationKeyVersion = arg.VerificationKeyVersion
            };

        public byte[] GetBytes(ExposureKeySetSignaturesContentArgs arg)
        {
            var result = new TEKSignatureList
            {
                Signatures = { arg.Items.Select(Map).ToArray() }
            };
            return result.ToByteArray();
        }

        private static TEKSignature Map(ExposureKeySetSignatureContentArgs args)
        {
            var s = ByteString.CopyFrom(args.Signature);
            return new TEKSignature
            {
                BatchNum = args.BatchNum,
                BatchSize = args.BatchSize,
                Signature = s,
                SignatureInfo = Map(args.SignatureInfo)
            };
        }
    }
}