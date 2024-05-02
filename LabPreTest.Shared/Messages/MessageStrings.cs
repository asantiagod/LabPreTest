﻿namespace LabPreTest.Shared.Messages
{
    public static class MessageStrings
    {
        public const string DbUpdateExceptionMessage = "The record you are trying to create already exists.";
        public const string DbRecordNotFoundMessage = "Record not found.";
        public const string DbDeleteErrorMessage = "It cannot be deleted because it has related records.";
        
        public const string DbCountryNotFoundMessage = "The Country doesn't exist.";
        public const string DbStateNotFoundMessage = "The State doesn't exist.";
        public const string DbCityNotFoundMessage = "The City doesn't exist.";
        public const string DbTestNotFoundMessage = "The test doesn't exist.";
        public const string DbTestTubeNotFoundMessage = "The test tube doesn't exist.";
    }
}