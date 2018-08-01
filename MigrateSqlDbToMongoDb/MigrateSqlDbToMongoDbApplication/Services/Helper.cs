﻿using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using JobDomainModel = MongoDatabase.Domain.Job.AggregatesModel;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using JobMatchingDomainModel = MongoDatabase.Domain.JobMatching.AggregatesModel;
using System.Text;
using System.Security.Cryptography;
using System;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public static class Helper
    {
        public static JobDomainModel.JobStatus JobStatusToJobService(HrToolDomainModel.JobStatus jobStatus, int jobExternalId)
        {
            if (jobStatus != null)
            {
                switch (jobStatus.Status)
                {
                    case "0":
                        return JobDomainModel.JobStatus.Draft;
                    case "3":
                        return JobDomainModel.JobStatus.Published;
                    case "4":
                        return JobDomainModel.JobStatus.Closed;
                }
            }
            return JobDomainModel.JobStatus.Closed;
        }

        public static CandidateDomainModel.JobStatus JobStatusToCandidateService(HrToolDomainModel.JobStatus jobStatus, int jobExternalId)
        {
            if (jobStatus != null)
            {
                switch (jobStatus.Status)
                {
                    case "0":
                        return CandidateDomainModel.JobStatus.Draft;
                    case "3":
                        return CandidateDomainModel.JobStatus.Published;
                    case "4":
                        return CandidateDomainModel.JobStatus.Closed;
                }
            }
            return CandidateDomainModel.JobStatus.Closed;
        }

        public static InterviewDomainModel.JobStatus JobStatusToInterviewService(HrToolDomainModel.JobStatus jobStatus, int jobExternalId)
        {
            if (jobStatus != null)
            {
                switch (jobStatus.Status)
                {
                    case "0":
                        return InterviewDomainModel.JobStatus.Draft;
                    case "3":
                        return InterviewDomainModel.JobStatus.Published;
                    case "4":
                        return InterviewDomainModel.JobStatus.Closed;
                }
            }
            return InterviewDomainModel.JobStatus.Closed;
        }

        public static OfferDomainModel.JobStatus JobStatusToOfferService(HrToolDomainModel.JobStatus jobStatus, int jobExternalId)
        {
            if (jobStatus != null)
            {
                switch (jobStatus.Status)
                {
                    case "0":
                        return OfferDomainModel.JobStatus.Draft;
                    case "3":
                        return OfferDomainModel.JobStatus.Published;
                    case "4":
                        return OfferDomainModel.JobStatus.Closed;
                }
            }
            return OfferDomainModel.JobStatus.Closed;
        }

        public static JobMatchingDomainModel.JobStatus JobStatusToJobMatchingService(HrToolDomainModel.JobStatus jobStatus, int jobExternalId)
        {
            if (jobStatus != null)
            {
                switch (jobStatus.Status)
                {
                    case "0":
                        return JobMatchingDomainModel.JobStatus.Draft;
                    case "3":
                        return JobMatchingDomainModel.JobStatus.Published;
                    case "4":
                        return JobMatchingDomainModel.JobStatus.Closed;
                }
            }
            return JobMatchingDomainModel.JobStatus.Closed;
        }

        public static string Decrypt(string toDecrypt, bool useHashing)
        {
            const string KeyEncrypt = "HRTool";
            if (string.IsNullOrEmpty(toDecrypt)) return toDecrypt;
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(KeyEncrypt));
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(KeyEncrypt);
            }
            var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
